using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MariVshApp.ViewModels.Admin;

public class AdminViewModel : INotifyPropertyChanged, IQueryAttributable
{
    // ── State ──────────────────────────────────────────────────────────
    private string _userName        = string.Empty;
    private string _welcomeMessage  = string.Empty;
    private string _activeTab       = "Dashboard";
    private int    _userTypeId      = 1;

    // ── User ───────────────────────────────────────────────────────────
    public string UserName
    {
        get => _userName;
        set { _userName = value; OnPropertyChanged(); }
    }

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set { _welcomeMessage = value; OnPropertyChanged(); }
    }

    // ── Tab visibility ─────────────────────────────────────────────────
    public bool IsDashboardVisible => _activeTab == "Dashboard";
    public bool IsIotHubVisible    => _activeTab == "IotHub";

    // ── Sidebar highlight colours ──────────────────────────────────────
    public string DashboardBg => _activeTab == "Dashboard" ? "#6200EE22" : "Transparent";
    public string DashboardFg => _activeTab == "Dashboard" ? "#BB86FC"   : "#AAAAAA";
    public string IotHubBg    => _activeTab == "IotHub"    ? "#6200EE22" : "Transparent";
    public string IotHubFg    => _activeTab == "IotHub"    ? "#BB86FC"   : "#AAAAAA";

    // ── Commands ───────────────────────────────────────────────────────
    public ICommand ShowDashboardCommand { get; }
    public ICommand ShowIotHubCommand    { get; }
    public ICommand LogoutCommand        { get; }

    public AdminViewModel()
    {
        ShowDashboardCommand = new Command(() => SetTab("Dashboard"));
        ShowIotHubCommand    = new Command(async () =>
            await Shell.Current.GoToAsync($"HubListPage?userName={_userName}&userTypeId={_userTypeId}"));
        LogoutCommand        = new Command(async () => await OnLogoutAsync());
    }

    // ── Helpers ────────────────────────────────────────────────────────
    private void SetTab(string tab)
    {
        _activeTab = tab;
        OnPropertyChanged(nameof(IsDashboardVisible));
        OnPropertyChanged(nameof(IsIotHubVisible));
        OnPropertyChanged(nameof(DashboardBg));
        OnPropertyChanged(nameof(DashboardFg));
        OnPropertyChanged(nameof(IotHubBg));
        OnPropertyChanged(nameof(IotHubFg));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("userName", out var name) && name is string userName)
        {
            UserName       = userName;
            WelcomeMessage = $"Welcome, {userName}!";
        }
        if (query.TryGetValue("userTypeId", out var t) && t is string tid &&
            int.TryParse(tid, out var tid2)) _userTypeId = tid2;
    }

    private async Task OnLogoutAsync()
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }

    // ── INotifyPropertyChanged ─────────────────────────────────────────
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
