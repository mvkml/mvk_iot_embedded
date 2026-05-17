using MariVshApp.Views.Admin;
using MariVshApp.Views.Home;
using MariVshApp.Views.UserAccount;

namespace MariVshApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Register routes for navigation
		Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
	}
}
