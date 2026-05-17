using MariVshApp.ViewModels.Home;

namespace MariVshApp.Views.Home;

/// <summary>
/// Code-behind for HomePage.
/// Kept minimal since all logic is in the HomeViewModel (MVVM pattern).
/// </summary>
public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
