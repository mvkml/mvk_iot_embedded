using MariVshApp.Database;
using MariVshApp.Repositories.Interfaces;
using MariVshApp.Repositories.SQLite;
using MariVshApp.Services.AppImage;
using MariVshApp.Services.Category;
using MariVshApp.Services.Component;
using MariVshApp.Services.ManageComponent;
using MariVshApp.Services.ComponentGroup;
using MariVshApp.Services.ComponentType;
using MariVshApp.Services.Manage;
using MariVshApp.Services.Hub;
using MariVshApp.Services.MasterDetails;
using MariVshApp.Services.MasterType;
using MariVshApp.Services.Project;
using MariVshApp.Services.Site;
using MariVshApp.Services.Subsite;
using MariVshApp.Services.UserAccount;
using MariVshApp.ViewModels.Admin;
using MariVshApp.ViewModels.AppImage;
using MariVshApp.ViewModels.Category;
using MariVshApp.ViewModels.Component;
using MariVshApp.ViewModels.ComponentGroup;
using MariVshApp.ViewModels.ComponentType;
using MariVshApp.ViewModels.Home;
using MariVshApp.ViewModels.Hub;
using MariVshApp.ViewModels.MasterDetails;
using MariVshApp.ViewModels.MasterType;
using MariVshApp.ViewModels.Manage;
using MariVshApp.ViewModels.Project;
using MariVshApp.ViewModels.Site;
using MariVshApp.ViewModels.Subsite;
using MariVshApp.ViewModels.UserAccount;
using MariVshApp.Views.Admin;
using MariVshApp.Views.AppImage;
using MariVshApp.Views.Category;
using MariVshApp.Views.Component;
using MariVshApp.Views.ComponentGroup;
using MariVshApp.Views.ComponentType;
using MariVshApp.Views.Home;
using MariVshApp.Views.Hub;
using MariVshApp.Views.MasterDetails;
using MariVshApp.Views.MasterType;
using MariVshApp.Views.Manage;
using MariVshApp.Views.Project;
using MariVshApp.Views.Site;
using MariVshApp.Views.Subsite;
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
		builder.Services.AddScoped<IManageItemRepository, ManageItemRepository>();
		builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
		builder.Services.AddScoped<IAppImageRepository, AppImageRepository>();
		builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
		builder.Services.AddScoped<IComponentTypeRepository, ComponentTypeRepository>();
		builder.Services.AddScoped<ISiteRepository, SiteRepository>();
		builder.Services.AddScoped<ISubsiteRepository, SubsiteRepository>();
		builder.Services.AddScoped<IComponentGroupRepository, ComponentGroupRepository>();
		builder.Services.AddScoped<IComponentRepository, ComponentRepository>();
		builder.Services.AddScoped<IManageComponentRepository, ManageComponentRepository>();
		builder.Services.AddScoped<IMasterTypeRepository, MasterTypeRepository>();
		builder.Services.AddScoped<IMasterDetailsRepository, MasterDetailsRepository>();
		builder.Services.AddScoped<IHubRepository, HubRepository>();

		// Services
		builder.Services.AddScoped<UserAccountService>();
		builder.Services.AddScoped<ManageItemService>();
		builder.Services.AddScoped<ProjectService>();
		builder.Services.AddScoped<AppImageService>();
		builder.Services.AddScoped<CategoryService>();
		builder.Services.AddScoped<ComponentTypeService>();
		builder.Services.AddScoped<SiteService>();
		builder.Services.AddScoped<SubsiteService>();
		builder.Services.AddScoped<ComponentGroupService>();
		builder.Services.AddScoped<ComponentService>();
		builder.Services.AddScoped<ManageComponentService>();
		builder.Services.AddScoped<MasterTypeService>();
		builder.Services.AddScoped<MasterDetailsService>();
		builder.Services.AddScoped<HubService>();

		// ViewModels and Pages
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<SignUpViewModel>();
		builder.Services.AddTransient<SignUpPage>();
		builder.Services.AddTransient<HomeViewModel>();
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<AdminViewModel>();
		builder.Services.AddTransient<AdminPage>();
		builder.Services.AddTransient<ManageViewModel>();
		builder.Services.AddTransient<ManagePage>();
		builder.Services.AddTransient<ItemViewModel>();
		builder.Services.AddTransient<ItemPage>();
		builder.Services.AddTransient<AppImageListViewModel>();
		builder.Services.AddTransient<AppImageListPage>();
		builder.Services.AddTransient<ManageImagesViewModel>();
		builder.Services.AddTransient<ManageImagesPage>();
		builder.Services.AddTransient<AppImageViewModel>();
		builder.Services.AddTransient<AppImagePage>();
		builder.Services.AddTransient<ProjectsViewModel>();
		builder.Services.AddTransient<ProjectsPage>();
		builder.Services.AddTransient<McSitesViewModel>();
		builder.Services.AddTransient<McSitesPage>();
		builder.Services.AddTransient<McSelectSitesViewModel>();
		builder.Services.AddTransient<McSelectSitesPage>();
		builder.Services.AddTransient<McSubsitesViewModel>();
		builder.Services.AddTransient<McSubsitesPage>();
		builder.Services.AddTransient<McSelectSubsitesViewModel>();
		builder.Services.AddTransient<McSelectSubsitesPage>();
		builder.Services.AddTransient<ProjectListViewModel>();
		builder.Services.AddTransient<ProjectListPage>();
		builder.Services.AddTransient<ManageProjectsViewModel>();
		builder.Services.AddTransient<ManageProjectsPage>();
		builder.Services.AddTransient<ProjectViewModel>();
		builder.Services.AddTransient<ProjectPage>();
		builder.Services.AddTransient<CategoryViewModel>();
		builder.Services.AddTransient<ManageCategoryPage>();
		builder.Services.AddTransient<ManageCategoriesViewModel>();
		builder.Services.AddTransient<ManageCategoriesPage>();
		builder.Services.AddTransient<CategoryListViewModel>();
		builder.Services.AddTransient<CategoryListPage>();
		builder.Services.AddTransient<ManageComponentTypesViewModel>();
		builder.Services.AddTransient<ComponentTypePage>();
		builder.Services.AddTransient<ManageComponentTypeViewModel>();
		builder.Services.AddTransient<ManageComponentTypePage>();
		builder.Services.AddTransient<ManageSitesViewModel>();
		builder.Services.AddTransient<SiteListPage>();
		builder.Services.AddTransient<ManageSiteViewModel>();
		builder.Services.AddTransient<ManageSitePage>();
		builder.Services.AddTransient<ManageSubsitesViewModel>();
		builder.Services.AddTransient<SubsiteListPage>();
		builder.Services.AddTransient<ManageSubsiteViewModel>();
		builder.Services.AddTransient<ManageSubsitePage>();
		builder.Services.AddTransient<ManageComponentGroupsViewModel>();
		builder.Services.AddTransient<ComponentGroupListPage>();
		builder.Services.AddTransient<ManageComponentGroupViewModel>();
		builder.Services.AddTransient<ManageComponentGroupPage>();
		builder.Services.AddTransient<ManageComponentsViewModel>();
		builder.Services.AddTransient<ComponentListPage>();
		builder.Services.AddTransient<ManageComponentViewModel>();
		builder.Services.AddTransient<ManageComponentPage>();
		builder.Services.AddTransient<SelectImageViewModel>();
		builder.Services.AddTransient<SelectImagePage>();
		builder.Services.AddTransient<McComponentGroupsViewModel>();
		builder.Services.AddTransient<McComponentGroupsPage>();
		builder.Services.AddTransient<McSelectComponentGroupsViewModel>();
		builder.Services.AddTransient<McSelectComponentGroupsPage>();
		builder.Services.AddTransient<McComponentsViewModel>();
		builder.Services.AddTransient<McComponentsPage>();
		builder.Services.AddTransient<McSelectComponentsViewModel>();
		builder.Services.AddTransient<McSelectComponentsPage>();
		builder.Services.AddTransient<ManageComponentEditViewModel>();
		builder.Services.AddTransient<ManageComponentEditPage>();
		builder.Services.AddTransient<UrlParametersViewModel>();
		builder.Services.AddTransient<UrlParametersPage>();
		builder.Services.AddTransient<MasterTypeListViewModel>();
		builder.Services.AddTransient<MasterTypePage>();
		builder.Services.AddTransient<ManageMasterTypeViewModel>();
		builder.Services.AddTransient<ManageMasterTypePage>();
		builder.Services.AddTransient<MasterDetailsListViewModel>();
		builder.Services.AddTransient<MasterDetailsPage>();
		builder.Services.AddTransient<ManageMasterDetailsViewModel>();
		builder.Services.AddTransient<ManageMasterDetailsPage>();
		builder.Services.AddTransient<HubListViewModel>();
		builder.Services.AddTransient<HubListPage>();
		builder.Services.AddTransient<ManageHubViewModel>();
		builder.Services.AddTransient<ManageHubPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
