using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MigApp.Controls
{
    /// <summary>
    /// Логика взаимодействия для WindowButtonsControl.xaml
    /// </summary>
    public partial class WindowButtonsControl : UserControl
    {
        public WindowButtonsControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MinimizeButtonStyleProperty =
            DependencyProperty.Register(nameof(MinimizeButtonStyle), typeof(Style), typeof(WindowButtonsControl));

        public Style MinimizeButtonStyle
        {
            get => (Style)GetValue(MinimizeButtonStyleProperty);
            set => SetValue(MinimizeButtonStyleProperty, value);
        }

        public static readonly DependencyProperty MaximizeButtonStyleProperty =
            DependencyProperty.Register(nameof(MaximizeButtonStyle), typeof(Style), typeof(WindowButtonsControl));

        public Style MaximizeButtonStyle
        {
            get => (Style)GetValue(MaximizeButtonStyleProperty);
            set => SetValue(MaximizeButtonStyleProperty, value);
        }

        public static readonly DependencyProperty CloseButtonStyleProperty =
            DependencyProperty.Register(nameof(CloseButtonStyle), typeof(Style), typeof(WindowButtonsControl));

        public Style CloseButtonStyle
        {
            get => (Style)GetValue(CloseButtonStyleProperty);
            set => SetValue(CloseButtonStyleProperty, value);
        }


    }
}
