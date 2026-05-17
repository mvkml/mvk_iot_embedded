using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MariVshApp.ViewModels.Admin;

/// <summary>
/// ViewModel for the Admin Landing Page.
/// Displays admin user name, welcome message, and handles logout.
/// </summary>
public class AdminViewModel : INotifyPropertyChanged, IQueryAttributable
{
    private string _userName = string.Empty;
    private string _welcomeMessage = string.Empty;

    /// <summary>
    /// The logged-in admin's full name, shown in the top-right corner.
    /// </summary>
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Welcome message shown on the page.
    /// </summary>
    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set
        {
            _welcomeMessage = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Command bound to the Logout button.
    /// </summary>
    public ICommand LogoutCommand { get; }

    public AdminViewModel()
    {
        LogoutCommand = new Command(async () => await OnLogoutAsync());
    }

    /// <summary>
    /// Receives query parameters from Shell navigation.
    /// Expects "userName" parameter.
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("userName", out var name) && name is string userName)
        {
            UserName = userName;
            WelcomeMessage = $"Welcome, {userName}!";
        }
    }

    /// <summary>
    /// Handles logout — navigates back to the Login page.
    /// </summary>
    private async Task OnLogoutAsync()
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
