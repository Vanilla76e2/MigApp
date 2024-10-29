using Microsoft.Extensions.DependencyInjection;
using MigApp.Core;
using MigApp.MVVM.ViewModel;
using MigApp.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<FavouriteViewModel>();

            services.AddSingleton<EmployeesViewModel>();
            services.AddSingleton<EmployeesGroupsViewModel>();
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

        protected override void OnStartup(StartupEventArgs e)
        {
            _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
