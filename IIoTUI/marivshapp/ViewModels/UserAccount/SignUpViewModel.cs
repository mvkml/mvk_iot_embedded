using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MariVshApp.Models;
using MariVshApp.Repositories.Interfaces;
using MariVshApp.Services.UserAccount;

namespace MariVshApp.ViewModels.UserAccount;

public class SignUpViewModel : INotifyPropertyChanged
{
    private readonly UserAccountService _userAccountService;
    private readonly IUserTypeRepository _userTypeRepository;

    private string _userId = string.Empty;
    private string _name = string.Empty;
    private string _password = string.Empty;
    private string _confirmPassword = string.Empty;
    private string _pin = string.Empty;
    private string _confirmPin = string.Empty;
    private string _description = string.Empty;
    private string _message = string.Empty;
    private Color _messageColor = Colors.Red;
    private bool _isSignUpSuccess;
    private UserType? _selectedUserType;

    public string UserId
    {
        get => _userId;
        set { _userId = value; OnPropertyChanged(); }
    }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set { _confirmPassword = value; OnPropertyChanged(); }
    }

    public string Pin
    {
        get => _pin;
        set { _pin = value; OnPropertyChanged(); }
    }

    public string ConfirmPin
    {
        get => _confirmPin;
        set { _confirmPin = value; OnPropertyChanged(); }
    }

    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(); }
    }

    public string Message
    {
        get => _message;
        set { _message = value; OnPropertyChanged(); }
    }

    public Color MessageColor
    {
        get => _messageColor;
        set { _messageColor = value; OnPropertyChanged(); }
    }

    public bool IsSignUpSuccess
    {
        get => _isSignUpSuccess;
        set { _isSignUpSuccess = value; OnPropertyChanged(); }
    }

    public ObservableCollection<UserType> UserTypeOptions { get; } = new();

    public UserType? SelectedUserType
    {
        get => _selectedUserType;
        set { _selectedUserType = value; OnPropertyChanged(); }
    }

    public ICommand SignUpCommand { get; }
    public ICommand GoToLoginCommand { get; }

    public SignUpViewModel(UserAccountService userAccountService, IUserTypeRepository userTypeRepository)
    {
        _userAccountService = userAccountService;
        _userTypeRepository = userTypeRepository;
        SignUpCommand = new Command(async () => await OnSignUpAsync());
        GoToLoginCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        _ = LoadUserTypesAsync();
    }

    private async Task LoadUserTypesAsync()
    {
        var types = await _userTypeRepository.GetAllActiveAsync();
        foreach (var type in types)
            UserTypeOptions.Add(type);
    }

    private async Task OnSignUpAsync()
    {
        if (string.IsNullOrWhiteSpace(UserId))
        {
            MessageColor = Colors.Red;
            Message = "Please enter a User ID (phone number or email).";
            return;
        }

        if (string.IsNullOrWhiteSpace(Name))
        {
            MessageColor = Colors.Red;
            Message = "Please enter your name.";
            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            MessageColor = Colors.Red;
            Message = "Please enter a password.";
            return;
        }

        if (Password != ConfirmPassword)
        {
            MessageColor = Colors.Red;
            Message = "Passwords do not match.";
            return;
        }

        if (string.IsNullOrWhiteSpace(Pin) || Pin.Length != 4 || !Pin.All(char.IsDigit))
        {
            MessageColor = Colors.Red;
            Message = "PIN must be exactly 4 digits.";
            return;
        }

        if (Pin != ConfirmPin)
        {
            MessageColor = Colors.Red;
            Message = "PINs do not match.";
            return;
        }

        if (SelectedUserType is null)
        {
            MessageColor = Colors.Red;
            Message = "Please select an account type.";
            return;
        }

        var (success, message) = await _userAccountService.SignUpAsync(
            UserId, Name, Password, Pin, Description, SelectedUserType!.TypeId);

        MessageColor = success ? Colors.Green : Colors.Red;
        Message = message;
        IsSignUpSuccess = success;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
