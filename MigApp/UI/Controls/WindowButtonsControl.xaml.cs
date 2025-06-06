using System.Windows;
using System.Windows.Controls;

namespace MigApp.UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для WindowButtonsControl.xaml
    /// </summary>
    public partial class WindowButtonsControl : UserControl
    {
        public static readonly DependencyProperty CloseButtonStyleProperty =
            DependencyProperty.Register(nameof(CloseButtonStyle), typeof(Style), typeof(WindowButtonsControl));

        public static readonly DependencyProperty MinimizeButtonStyleProperty =
            DependencyProperty.Register(nameof(MinimizeButtonStyle), typeof(Style), typeof(WindowButtonsControl));

        public static readonly DependencyProperty MaximizeButtonStyleProperty =
            DependencyProperty.Register(nameof(MaximizeButtonStyle), typeof(Style), typeof(WindowButtonsControl));

        public static readonly DependencyProperty InfoButtonStyleProperty =
            DependencyProperty.Register(nameof(InfoButtonStyle), typeof(Style), typeof(WindowButtonsControl));

        public Style MinimizeButtonStyle
        {
            get => (Style)GetValue(MinimizeButtonStyleProperty);
            set => SetValue(MinimizeButtonStyleProperty, value);
        }

        public Style MaximizeButtonStyle
        {
            get => (Style)GetValue(MaximizeButtonStyleProperty);
            set => SetValue(MaximizeButtonStyleProperty, value);
        }

        public Style CloseButtonStyle
        {
            get => (Style)GetValue(CloseButtonStyleProperty);
            set => SetValue(CloseButtonStyleProperty, value);
        }

        public Style InfoButtonStyle
        {
            get => (Style)GetValue(InfoButtonStyleProperty);
            set => SetValue(InfoButtonStyleProperty, value);
        }

        public WindowButtonsControl()
        {
            InitializeComponent();
        }
    }
}
