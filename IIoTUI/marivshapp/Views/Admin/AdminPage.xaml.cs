using MariVshApp.ViewModels.Admin;

namespace MariVshApp.Views.Admin;

/// <summary>
/// Code-behind for AdminPage.
/// Kept minimal since all logic is in the AdminViewModel (MVVM pattern).
/// </summary>
public partial class AdminPage : ContentPage
{
    public AdminPage(AdminViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
