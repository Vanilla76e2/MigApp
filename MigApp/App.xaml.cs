using Microsoft.Extensions.DependencyInjection;
using System.Windows.Threading;
using MigApp.Core.Services.Dispathcer;
using MigApp.Core.Session;
using MigApp.MVVM.View;
using MigApp.MVVM.ViewModel;
using System.Windows;
using MigApp.Core.Services.AppUpdate;
using MigApp.Core.Services.Installer;
using Microsoft.EntityFrameworkCore;
using MigApp.Core.Services.DemoModeManager;

namespace MigApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IAppLogger _logger;

    public App()
    {
        var tempLogger = new AppLogger();
        tempLogger.LogInformation(GetStartupBanner());

        InitializeComponent();

        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<IDbContextFactory<DbContext>, MigDatabaseContextFactory>();
        services.AddTransient<IDbContextProvider, MigDatabaseContextProvider>();

        // Регистрация сервисов
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IVersionService, VersionService>();
        services.AddSingleton<IAppLogger, AppLogger>();
        services.AddSingleton<IUserSession, UserSession>();
        services.AddSingleton<CrashLogger>();
        services.AddSingleton<Dispatcher>(provider => Application.Current.Dispatcher);
        services.AddSingleton<IDemoModeService, DemoModeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<IDispatcher, WpfDispatcher>();
        services.AddTransient<IUINotificationService, UINotificationService>();
        services.AddTransient<IInternetService, InternetService>();
        services.AddTransient<IDatabaseService, DatabaseService>();
        services.AddTransient<IAppUpdateService, AppUpdateService>();
        services.AddTransient<IDnsResolver, DnsResolver>();
        services.AddTransient<IInstallerService, InstallerService>();
        

        services.AddScoped<LoginWindowModel>();
        services.AddScoped<MainWindowModel>();

        services.AddScoped<FavouriteViewModel>();

        services.AddScoped<EmployeesViewModel>();
        services.AddScoped<DepartmentViewModel>();
        services.AddScoped<ComputersViewModel>();
        services.AddScoped<LaptopsViewModel>();
        services.AddScoped<TabletsViewModel>();
        services.AddScoped<OrgtechViewModel>();
        services.AddScoped<MonitorsViewModel>();
        services.AddScoped<RoutersViewModel>();
        services.AddScoped<SwitchesViewModel>();
        services.AddScoped<CCTVViewModel>();

        services.AddScoped<FurnitureViewModel>();
        services.AddScoped<FurnitureTypeViewModel>();

        services.AddScoped<UsersViewModel>();
        services.AddScoped<RolesViewModel>();
        services.AddScoped<LogsViewModel>();
        services.AddScoped<IPViewModel>();
        
        services.AddScoped<LoginWindow>(provider => new LoginWindow(provider.GetRequiredService<LoginWindowModel>()));
        //services.AddScoped<MainWindow>(provider => new MainWindow(provider.GetRequiredService<MainWindowModel>()));

        services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

        _serviceProvider = services.BuildServiceProvider();
    }

    private void ShowLoginView()
    {
        var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
        Current.MainWindow = loginWindow;
        Current.MainWindow.Show();
    }

    private string GetStartupBanner()
    {
        return $@"
================================================
███╗   ███╗██╗ ██████╗  █████╗ ██████╗ ██████╗    
████╗ ████║██║██╔════╝ ██╔══██╗██╔══██╗██╔══██╗
██╔████╔██║██║██║  ███╗███████║██████╔╝██████╔╝
██║╚██╔╝██║██║██║   ██║██╔══██║██╔═══╝ ██╔═══╝ 
██║ ╚═╝ ██║██║╚██████╔╝██║  ██║██║     ██║      
╚═╝     ╚═╝╚═╝ ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝    
                                                                       
 Version: 2.0.0 | Author: Vanilla76e2
 Startup: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
 OS: {Environment.OSVersion.VersionString}
===============================================
";
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        ShowLoginView();
        base.OnStartup(e);
    }

    public static void ChangeTheme(bool isDarkTheme)
    {
        var themeUri = isDarkTheme
            ? "Styles/Themes/DarkTheme.xaml"
            : "Styles/Themes/LightTheme.xaml";

        var themeDict = new ResourceDictionary { Source = new Uri(themeUri, UriKind.Relative) };

        // Находим и заменяем словарь темы
        var currentDict = Application.Current.Resources.MergedDictionaries
            .FirstOrDefault(d => d.Source?.OriginalString.Contains("Themes/") == true);

        if (currentDict != null)
        {
            Application.Current.Resources.MergedDictionaries.Remove(currentDict);
        }

        Application.Current.Resources.MergedDictionaries.Add(themeDict);
    }
}


