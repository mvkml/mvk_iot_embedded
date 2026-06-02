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
		Routing.RegisterRoute(nameof(ManagePage),     typeof(ManagePage));
		Routing.RegisterRoute(nameof(ItemPage),       typeof(ItemPage));
		Routing.RegisterRoute(nameof(AppImageListPage),   typeof(AppImageListPage));
		Routing.RegisterRoute(nameof(ManageImagesPage),   typeof(ManageImagesPage));
		Routing.RegisterRoute(nameof(AppImagePage),       typeof(AppImagePage));
		Routing.RegisterRoute(nameof(ProjectsPage),        typeof(ProjectsPage));
		Routing.RegisterRoute(nameof(McSitesPage),             typeof(McSitesPage));
		Routing.RegisterRoute(nameof(McSelectSitesPage),       typeof(McSelectSitesPage));
		Routing.RegisterRoute(nameof(McSubsitesPage),          typeof(McSubsitesPage));
		Routing.RegisterRoute(nameof(McSelectSubsitesPage),    typeof(McSelectSubsitesPage));
		Routing.RegisterRoute(nameof(ProjectListPage),    typeof(ProjectListPage));
		Routing.RegisterRoute(nameof(ManageProjectsPage), typeof(ManageProjectsPage));
		Routing.RegisterRoute(nameof(ProjectPage),          typeof(ProjectPage));
		Routing.RegisterRoute(nameof(ManageCategoryPage),  typeof(ManageCategoryPage));
		Routing.RegisterRoute(nameof(ManageCategoriesPage),typeof(ManageCategoriesPage));
		Routing.RegisterRoute(nameof(CategoryListPage),    typeof(CategoryListPage));
		Routing.RegisterRoute(nameof(ComponentTypePage),        typeof(ComponentTypePage));
		Routing.RegisterRoute(nameof(ManageComponentTypePage),  typeof(ManageComponentTypePage));
		Routing.RegisterRoute(nameof(SiteListPage),             typeof(SiteListPage));
		Routing.RegisterRoute(nameof(ManageSitePage),           typeof(ManageSitePage));
		Routing.RegisterRoute(nameof(SubsiteListPage),          typeof(SubsiteListPage));
		Routing.RegisterRoute(nameof(ManageSubsitePage),        typeof(ManageSubsitePage));
		Routing.RegisterRoute(nameof(ComponentGroupListPage),   typeof(ComponentGroupListPage));
		Routing.RegisterRoute(nameof(ManageComponentGroupPage), typeof(ManageComponentGroupPage));
		Routing.RegisterRoute(nameof(ComponentListPage),           typeof(ComponentListPage));
		Routing.RegisterRoute(nameof(ManageComponentPage),         typeof(ManageComponentPage));
		Routing.RegisterRoute(nameof(SelectImagePage),             typeof(SelectImagePage));
		Routing.RegisterRoute(nameof(McComponentGroupsPage),       typeof(McComponentGroupsPage));
		Routing.RegisterRoute(nameof(McSelectComponentGroupsPage), typeof(McSelectComponentGroupsPage));
		Routing.RegisterRoute(nameof(McComponentsPage),            typeof(McComponentsPage));
		Routing.RegisterRoute(nameof(McSelectComponentsPage),      typeof(McSelectComponentsPage));
		Routing.RegisterRoute(nameof(ManageComponentEditPage),     typeof(ManageComponentEditPage));
		Routing.RegisterRoute(nameof(UrlParametersPage),           typeof(UrlParametersPage));
		Routing.RegisterRoute(nameof(MasterTypePage),           typeof(MasterTypePage));
		Routing.RegisterRoute(nameof(ManageMasterTypePage),     typeof(ManageMasterTypePage));
		Routing.RegisterRoute(nameof(MasterDetailsPage),        typeof(MasterDetailsPage));
		Routing.RegisterRoute(nameof(ManageMasterDetailsPage),  typeof(ManageMasterDetailsPage));
		Routing.RegisterRoute(nameof(HubListPage),   typeof(HubListPage));
		Routing.RegisterRoute(nameof(ManageHubPage), typeof(ManageHubPage));
	}
}
