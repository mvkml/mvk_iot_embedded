using MariVshApp.ViewModels.UserAccount;

namespace MariVshApp.Views.UserAccount;

/// <summary>
/// Code-behind for SignUpPage.
/// Receives SignUpViewModel via dependency injection.
/// </summary>
public partial class SignUpPage : ContentPage
{
    public SignUpPage(SignUpViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
