
using Microsoft.Extensions.DependencyInjection;
using MigApp.UI.MVVM.ViewModel;
using MigApp.UI.Services.Navigation;
using System.Windows;
using System.Windows.Input;

namespace MigApp.MVVM.View
{
    public partial class MainWindow : Window
    {
        public MainWindowModel ViewModel;

        public MainWindow(MainWindowModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
        }
    }
}