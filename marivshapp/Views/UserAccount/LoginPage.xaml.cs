using MariVshApp.ViewModels.UserAccount;

namespace MariVshApp.Views.UserAccount;

/// <summary>
/// Code-behind for LoginPage.
/// Kept minimal since all logic is in the LoginViewModel (MVVM pattern).
/// </summary>
public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    /// <summary>
    /// Navigates to the Sign Up page when the user taps "Sign Up".
    /// </summary>
    private async void OnSignUpTapped(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignUpPage));
    }
}
