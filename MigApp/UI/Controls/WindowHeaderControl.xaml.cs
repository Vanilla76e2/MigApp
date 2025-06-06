using System.Windows;
using System.Windows.Controls;

namespace MigApp.UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для WindowHeader.xaml
    /// </summary>
    public partial class WindowHeaderControl : UserControl
    {
        public WindowHeaderControl()
        {
            InitializeComponent();
        }

        // Иконка окна

        public static readonly DependencyProperty HasIconProperty =
            DependencyProperty.Register(nameof(HasIcon), typeof(bool), typeof(WindowHeaderControl), new PropertyMetadata(true));

        public bool HasIcon
        {
            get => (bool)GetValue(HasIconProperty);
            set => SetValue(HasIconProperty, value);
        }

        public static readonly DependencyProperty TitleStyleProperty =
            DependencyProperty.Register(nameof(TitleStyle), typeof(Style), typeof(WindowHeaderControl));

        public Style TitleStyle
        {
            get => (Style)GetValue(TitleStyleProperty);
            set => SetValue(TitleStyleProperty, value);
        }

        // Подзаголовок окна

        public static readonly DependencyProperty SubTitleTextProperty =
            DependencyProperty.Register(nameof(SubTitleText), typeof(string), typeof(WindowHeaderControl), new PropertyMetadata(string.Empty));

        public string SubTitleText
        {
            get => (string)GetValue(SubTitleTextProperty);
            set => SetValue(SubTitleTextProperty, value);
        }

        public static readonly DependencyProperty SubTitleStyleProperty =
            DependencyProperty.Register(nameof(SubTitleStyle), typeof(Style), typeof(WindowHeaderControl));

        public Style SubTitleStyle
        {
            get => (Style)GetValue(SubTitleStyleProperty);
            set => SetValue(SubTitleStyleProperty, value);
        }
    }
}
