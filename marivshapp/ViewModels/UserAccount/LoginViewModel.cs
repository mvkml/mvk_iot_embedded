using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MariVshApp.Database;

namespace MariVshApp.ViewModels.UserAccount;

/// <summary>
/// ViewModel for the Login Page.
/// Implements INotifyPropertyChanged to support data binding with the UI.
/// </summary>
public class LoginViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _databaseService;

    // Backing fields for bound properties
    private string _userId = string.Empty;
    private string _password = string.Empty;
    private string _message = string.Empty;
    private Color _messageColor = Colors.Red;

    /// <summary>
    /// User ID entered by the user. Bound to the User ID Entry field.
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
    /// Password entered by the user. Bound to the Password Entry field.
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
    /// Message displayed to the user (success or error). Bound to the Message Label.
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
    /// Command bound to the Login button.
    /// </summary>
    public ICommand LoginCommand { get; }

    public LoginViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        LoginCommand = new Command(async () => await OnLoginAsync());
    }

    /// <summary>
    /// Handles the login button click.
    /// Validates User ID and Password against the SQLite database.
    /// </summary>
    private async Task OnLoginAsync()
    {
        // Check if User ID is empty
        if (string.IsNullOrWhiteSpace(UserId))
        {
            MessageColor = Colors.Red;
            Message = "Please enter a User ID.";
            return;
        }

        // Check if password is empty
        if (string.IsNullOrWhiteSpace(Password))
        {
            MessageColor = Colors.Red;
            Message = "Please enter a password.";
            return;
        }

        // Look up user in the database
        var db = await _databaseService.GetDatabaseAsync();
        var user = await db.Table<Models.UserAccount>()
            .Where(u => u.UserId == UserId && u.Password == Password)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            MessageColor = Colors.Red;
            Message = "Invalid User ID or Password.";
            return;
        }

        // Login successful — navigate to Home page with the user's name
        MessageColor = Colors.Green;
        Message = "Login successful!";

        var fullName = $"{user.FirstName} {user.LastName}";
        await Shell.Current.GoToAsync($"{nameof(Views.Home.HomePage)}?userName={Uri.EscapeDataString(fullName)}");
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises the PropertyChanged event to notify the UI of value changes.
    /// CallerMemberName automatically provides the calling property name.
    /// </summary>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
