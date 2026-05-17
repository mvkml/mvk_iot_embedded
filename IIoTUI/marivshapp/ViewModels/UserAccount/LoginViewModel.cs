using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MariVshApp.Services.UserAccount;

namespace MariVshApp.ViewModels.UserAccount;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly UserAccountService _userAccountService;

    private string _userId = string.Empty;
    private string _password = string.Empty;
    private string _pin = string.Empty;
    private string _message = string.Empty;
    private Color _messageColor = Colors.Red;

    public string UserId
    {
        get => _userId;
        set { _userId = value; OnPropertyChanged(); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public string Pin
    {
        get => _pin;
        set { _pin = value; OnPropertyChanged(); }
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

    public ICommand LoginCommand { get; }
    public ICommand LoginByPinCommand { get; }

    public LoginViewModel(UserAccountService userAccountService)
    {
        _userAccountService = userAccountService;
        LoginCommand = new Command(async () => await OnLoginAsync());
        LoginByPinCommand = new Command(async () => await OnLoginByPinAsync());
    }

    private async Task OnLoginAsync()
    {
        if (string.IsNullOrWhiteSpace(UserId))
        {
            MessageColor = Colors.Red;
            Message = "Please enter a User ID.";
            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            MessageColor = Colors.Red;
            Message = "Please enter a password.";
            return;
        }

        var (success, message, user) = await _userAccountService.LoginAsync(UserId, Password);

        MessageColor = success ? Colors.Green : Colors.Red;
        Message = message;

        if (success && user is not null)
            await Shell.Current.GoToAsync($"{nameof(Views.Home.HomePage)}?userName={Uri.EscapeDataString(user.Name)}");
    }

    private async Task OnLoginByPinAsync()
    {
        if (string.IsNullOrWhiteSpace(Pin) || Pin.Length != 4 || !Pin.All(char.IsDigit))
        {
            MessageColor = Colors.Red;
            Message = "PIN must be exactly 4 digits.";
            return;
        }

        var (success, message, user) = await _userAccountService.LoginByPinAsync(Pin);

        MessageColor = success ? Colors.Green : Colors.Red;
        Message = message;

        if (success && user is not null)
            await Shell.Current.GoToAsync($"{nameof(Views.Home.HomePage)}?userName={Uri.EscapeDataString(user.Name)}");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
