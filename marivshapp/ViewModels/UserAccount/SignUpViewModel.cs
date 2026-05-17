using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MariVshApp.Database;
using MariVshApp.Models;

namespace MariVshApp.ViewModels.UserAccount;

/// <summary>
/// ViewModel for the Sign-Up Page.
/// Handles user registration fields, validation, and saving to SQLite.
/// </summary>
public class SignUpViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _databaseService;

    // Backing fields for bound properties
    private string _phoneNumber = string.Empty;
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _email = string.Empty;
    private DateTime _dateOfBirth = DateTime.Today;
    private string _selectedGender = string.Empty;
    private string _userId = string.Empty;
    private string _password = string.Empty;
    private string _confirmPassword = string.Empty;
    private string _message = string.Empty;
    private Color _messageColor = Colors.Red;
    private UserType? _selectedUserType;

    /// <summary>
    /// Phone number entered by the user.
    /// </summary>
    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// First name entered by the user.
    /// </summary>
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Last name entered by the user.
    /// </summary>
    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Email ID entered by the user.
    /// </summary>
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Date of birth selected by the user.
    /// </summary>
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gender selected by the user (Male or Female).
    /// </summary>
    public string SelectedGender
    {
        get => _selectedGender;
        set
        {
            _selectedGender = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// User ID (username) for login.
    /// </summary>
    public string UserId
    {
        get => _userId;
        set
        {
            _userId = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Password for login.
    /// </summary>
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Confirm password to match with password.
    /// </summary>
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            _confirmPassword = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// List of gender options for the picker.
    /// </summary>
    public List<string> GenderOptions { get; } = new() { "Male", "Female" };

    /// <summary>
    /// List of active user types loaded from the database.
    /// </summary>
    public ObservableCollection<UserType> UserTypeOptions { get; } = new();

    /// <summary>
    /// The user type selected by the user.
    /// </summary>
    public UserType? SelectedUserType
    {
        get => _selectedUserType;
        set
        {
            _selectedUserType = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Message displayed to the user (success or error).
    /// </summary>
    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Color of the message text (Red for errors, Green for success).
    /// </summary>
    public Color MessageColor
    {
        get => _messageColor;
        set
        {
            _messageColor = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Command bound to the Sign Up button.
    /// </summary>
    public ICommand SignUpCommand { get; }

    public SignUpViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        SignUpCommand = new Command(async () => await OnSignUpAsync());
        _ = LoadUserTypesAsync();
    }

    /// <summary>
    /// Loads active user types from the database into the picker.
    /// </summary>
    private async Task LoadUserTypesAsync()
    {
        var types = await _databaseService.GetActiveUserTypesAsync();
        foreach (var type in types)
        {
            UserTypeOptions.Add(type);
        }
    }

    /// <summary>
    /// Handles the sign-up button click.
    /// Validates all fields, saves to database, and shows a prompt to the user.
    /// </summary>
    private async Task OnSignUpAsync()
    {
        // Validate Phone Number
        if (string.IsNullOrWhiteSpace(PhoneNumber))
        {
            MessageColor = Colors.Red;
            Message = "Please enter a phone number.";
            return;
        }

        // Validate First Name
        if (string.IsNullOrWhiteSpace(FirstName))
        {
            MessageColor = Colors.Red;
            Message = "Please enter your first name.";
            return;
        }

        // Validate Last Name
        if (string.IsNullOrWhiteSpace(LastName))
        {
            MessageColor = Colors.Red;
            Message = "Please enter your last name.";
            return;
        }

        // Validate Email
        if (string.IsNullOrWhiteSpace(Email))
        {
            MessageColor = Colors.Red;
            Message = "Please enter your email ID.";
            return;
        }

        // Validate Gender
        if (string.IsNullOrWhiteSpace(SelectedGender))
        {
            MessageColor = Colors.Red;
            Message = "Please select your gender.";
            return;
        }

        // Validate Account Type
        if (SelectedUserType is null)
        {
            MessageColor = Colors.Red;
            Message = "Please select an account type.";
            return;
        }

        // Validate User ID
        if (string.IsNullOrWhiteSpace(UserId))
        {
            MessageColor = Colors.Red;
            Message = "Please enter a User ID.";
            return;
        }

        // Validate Password
        if (string.IsNullOrWhiteSpace(Password))
        {
            MessageColor = Colors.Red;
            Message = "Please enter a password.";
            return;
        }

        // Validate Confirm Password
        if (Password != ConfirmPassword)
        {
            MessageColor = Colors.Red;
            Message = "Passwords do not match.";
            return;
        }

        // Check if User ID already exists
        var db = await _databaseService.GetDatabaseAsync();
        var existingUser = await db.Table<Models.UserAccount>()
            .Where(u => u.UserId == UserId)
            .FirstOrDefaultAsync();

        if (existingUser is not null)
        {
            MessageColor = Colors.Red;
            Message = "User ID already exists. Please choose a different one.";
            return;
        }

        // Create a new UserAccount object from the form data
        var user = new Models.UserAccount
        {
            PhoneNumber = PhoneNumber,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = SelectedGender,
            UserId = UserId,
            Password = Password,
            TypeId = SelectedUserType!.TypeId,
            CreatedDateTime = DateTime.Now,
            UpdatedDateTime = DateTime.Now
        };

        // Save to SQLite database
        await db.InsertAsync(user);

        // Show success message
        MessageColor = Colors.Green;
        Message = "Sign-up successful!";

        // Show an alert prompt to the user
        if (Application.Current?.Windows.Count > 0)
        {
            var page = Application.Current.Windows[0].Page;
            if (page is not null)
            {
                await page.DisplayAlert("Success", "Your account has been created successfully!", "OK");
            }
        }
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
