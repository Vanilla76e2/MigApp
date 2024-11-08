using Microsoft.Extensions.DependencyInjection;
using MigApp.Core;
using MigApp.MVVM.ViewModel;
using MigApp.Services;
using System;
using MigApp.MVVM.View;
using System.Windows;

namespace MigApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<NavigationService, NavigationService>();

            services.AddSingleton<LoginView>(provider => new LoginView(provider));

            services.AddSingleton<MainView>(provider => new MainView
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();

            services.AddSingleton<FavouriteViewModel>();

            services.AddSingleton<EmployeesViewModel>();
            services.AddSingleton<DepartmentViewModel>();
            services.AddSingleton<ComputersViewModel>();
            services.AddSingleton<LaptopsViewModel>();
            services.AddSingleton<TabletsViewModel>();
            services.AddSingleton<OrgtechViewModel>();
            services.AddSingleton<MonitorsViewModel>();
            services.AddSingleton<RoutersViewModel>();
            services.AddSingleton<SwitchesViewModel>();
            services.AddSingleton<CCTVViewModel>();

            services.AddSingleton<FurnitureViewModel>();
            services.AddSingleton<FurnitureTypeViewModel>();

            services.AddSingleton<UsersViewModel>();
            services.AddSingleton<RolesViewModel>();
            services.AddSingleton<LogsViewModel>();
            services.AddSingleton<IPViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        private void ShowLoginView()
        {
            var loginView = _serviceProvider.GetRequiredService<LoginView>();
            Current.MainWindow = loginView;
            Current.MainWindow.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ShowLoginView();
            base.OnStartup(e);
        }
    }
}
