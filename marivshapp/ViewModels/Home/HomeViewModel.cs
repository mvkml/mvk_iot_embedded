using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MariVshApp.ViewModels.Home;

/// <summary>
/// ViewModel for the Home Page.
/// Displays user name and welcome message, handles logout.
/// </summary>
public class HomeViewModel : INotifyPropertyChanged, IQueryAttributable
{
    private string _userName = string.Empty;
    private string _welcomeMessage = string.Empty;

    /// <summary>
    /// The logged-in user's full name, shown in the top-right corner.
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
    /// Welcome message shown in the center of the page.
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

    public HomeViewModel()
    {
        LogoutCommand = new Command(async () => await OnLogoutAsync());
    }

    /// <summary>
    /// Receives query parameters from Shell navigation.
    /// Expects "userName" parameter passed from LoginViewModel.
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
        // Navigate back to root (Login page) and clear the navigation stack
        await Shell.Current.GoToAsync("//LoginPage");
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
