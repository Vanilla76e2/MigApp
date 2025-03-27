using Microsoft.Extensions.DependencyInjection;
using MigApp.Core;
using MigApp.Core.Services.DatabaseService;
using MigApp.Core.Services.NavigationService;
using MigApp.Core.Session;
using MigApp.MVVM.View;
using MigApp.MVVM.ViewModel;
using System.Windows;

namespace MigApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();

        // Регистрация сервисов
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IVersionService, VersionService>();
        services.AddSingleton<IAppLogger, AppLogger>();
        services.AddTransient<IDatabaseService, DatabaseService>();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IUserSession, UserSession>();
        services.AddTransient<IInternetService, InternetService>();
        services.AddSingleton<CrashLogger>();

        services.AddScoped<LoginWindow>(provider => new LoginWindow(provider));

        services.AddScoped<MainView>(provider => new MainView
        {
            DataContext = provider.GetRequiredService<MainWindowModel>()
        });

        services.AddScoped<MainWindowModel>();
        services.AddScoped<LoginWindowModel>();

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

        services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

        _serviceProvider = services.BuildServiceProvider();
    }

    private void ShowLoginView()
    {
        var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
        Current.MainWindow = loginWindow;
        Current.MainWindow.Show();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        ShowLoginView();
        base.OnStartup(e);
    }
}


