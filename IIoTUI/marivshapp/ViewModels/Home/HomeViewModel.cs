using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MariVshApp.ViewModels.Home;

public enum HomeSection { Favourites, Projects, Manage, UserInfo }

public class HomeViewModel : INotifyPropertyChanged, IQueryAttributable
{
    private static readonly Color ActiveBg = Color.FromArgb("#EDE7F6");
    private static readonly Color InactiveBg = Colors.Transparent;
    private static readonly Color ActiveColor = Color.FromArgb("#6200EE");
    private static readonly Color InactiveColor = Colors.White;

    private string _userName = string.Empty;
    private bool _isAdmin;
    private HomeSection _activeSection = HomeSection.Favourites;

    public string UserName
    {
        get => _userName;
        set { _userName = value; OnPropertyChanged(); }
    }

    public bool IsAdmin
    {
        get => _isAdmin;
        set { _isAdmin = value; OnPropertyChanged(); }
    }

    public HomeSection ActiveSection
    {
        get => _activeSection;
        set
        {
            _activeSection = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsFavouritesVisible));
            OnPropertyChanged(nameof(IsProjectsVisible));
            OnPropertyChanged(nameof(IsManageVisible));
            OnPropertyChanged(nameof(IsUserInfoVisible));
            OnPropertyChanged(nameof(FavouritesBg));
            OnPropertyChanged(nameof(ProjectsBg));
            OnPropertyChanged(nameof(ManageBg));
            OnPropertyChanged(nameof(UserInfoBg));
            OnPropertyChanged(nameof(FavouritesColor));
            OnPropertyChanged(nameof(ProjectsColor));
            OnPropertyChanged(nameof(ManageColor));
            OnPropertyChanged(nameof(UserInfoColor));
        }
    }

    // Content visibility
    public bool IsFavouritesVisible => ActiveSection == HomeSection.Favourites;
    public bool IsProjectsVisible => ActiveSection == HomeSection.Projects;
    public bool IsManageVisible => ActiveSection == HomeSection.Manage;
    public bool IsUserInfoVisible => ActiveSection == HomeSection.UserInfo;

    // Sidebar active highlight — background
    public Color FavouritesBg => ActiveSection == HomeSection.Favourites ? ActiveBg : InactiveBg;
    public Color ProjectsBg => ActiveSection == HomeSection.Projects ? ActiveBg : InactiveBg;
    public Color ManageBg => ActiveSection == HomeSection.Manage ? ActiveBg : InactiveBg;
    public Color UserInfoBg => ActiveSection == HomeSection.UserInfo ? ActiveBg : InactiveBg;

    // Sidebar active highlight — text color
    public Color FavouritesColor => ActiveSection == HomeSection.Favourites ? ActiveColor : InactiveColor;
    public Color ProjectsColor => ActiveSection == HomeSection.Projects ? ActiveColor : InactiveColor;
    public Color ManageColor => ActiveSection == HomeSection.Manage ? ActiveColor : InactiveColor;
    public Color UserInfoColor => ActiveSection == HomeSection.UserInfo ? ActiveColor : InactiveColor;

    public ICommand ShowFavouritesCommand { get; }
    public ICommand ShowProjectsCommand { get; }
    public ICommand ShowManageCommand { get; }
    public ICommand ShowUserInfoCommand { get; }
    public ICommand GoToAdminCommand { get; }
    public ICommand LogoutCommand { get; }

    public HomeViewModel()
    {
        ShowFavouritesCommand = new Command(() => ActiveSection = HomeSection.Favourites);
        ShowProjectsCommand = new Command(async () =>
            await Shell.Current.GoToAsync(
                $"{nameof(Views.Project.ProjectsPage)}?userName={Uri.EscapeDataString(_userName)}&userTypeId={(_isAdmin ? 2 : 1)}"));
        ShowManageCommand = new Command(async () =>
            await Shell.Current.GoToAsync(
                $"{nameof(Views.Manage.ManagePage)}?userName={Uri.EscapeDataString(_userName)}&userTypeId={(_isAdmin ? 2 : 1)}"));
        ShowUserInfoCommand = new Command(() => ActiveSection = HomeSection.UserInfo);
        GoToAdminCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(Views.Admin.AdminPage)));
        LogoutCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//LoginPage"));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("userName", out var name))
            UserName = Uri.UnescapeDataString(name?.ToString() ?? string.Empty);

        if (query.TryGetValue("userTypeId", out var typeId) &&
            int.TryParse(typeId?.ToString(), out var id))
            IsAdmin = id == 2;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
