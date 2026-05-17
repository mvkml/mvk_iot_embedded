using MariVshApp.Database;
using MariVshApp.Repositories.Interfaces;
using MariVshApp.Repositories.SQLite;
using MariVshApp.Services.UserAccount;
using MariVshApp.ViewModels.Admin;
using MariVshApp.ViewModels.Home;
using MariVshApp.ViewModels.UserAccount;
using MariVshApp.Views.Admin;
using MariVshApp.Views.Home;
using MariVshApp.Views.UserAccount;
using Microsoft.Extensions.Logging;

namespace MariVshApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Database
		builder.Services.AddSingleton<DatabaseService>();

		// Repositories
		builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
		builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();

		// Services
		builder.Services.AddScoped<UserAccountService>();

		// ViewModels and Pages
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<SignUpViewModel>();
		builder.Services.AddTransient<SignUpPage>();
		builder.Services.AddTransient<HomeViewModel>();
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<AdminViewModel>();
		builder.Services.AddTransient<AdminPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
