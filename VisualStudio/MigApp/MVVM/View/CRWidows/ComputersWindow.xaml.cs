using MigApp.MVVM.ViewModel.CRWindows;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MigApp.MVVM.View.CRWindows
{
    /// <summary>
    /// Логика взаимодействия для ComputersWindow.xaml
    /// </summary>
    public partial class ComputersWindow : Window
    {
        private ComputersWindowModel computersWindowModel;
        public ComputersWindow()
        {
            InitializeComponent();
            computersWindowModel = new ComputersWindowModel();
            DataContext = computersWindowModel;
        }

        private void CustomUI_WindowControl(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(Custom_MinimizeButton))
            {
                this.WindowState = WindowState.Minimized;
            }
            else if (sender.Equals(Custom_CloseButton))
            {
                this.Close();
            }
        }
    }
}
