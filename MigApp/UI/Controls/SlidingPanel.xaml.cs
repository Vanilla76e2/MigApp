using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MigApp.UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для SlidingPanel.xaml
    /// </summary>
    public partial class SlidingPanel : UserControl
    {
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            nameof(IsOpen), typeof(bool), typeof(SlidingPanel), new PropertyMetadata(false, OnIsOpenChanged));

        public static readonly DependencyProperty CurrentContentProperty = DependencyProperty.Register(
            nameof(CurrentContent), typeof(object), typeof(SlidingPanel), new PropertyMetadata());

        public static readonly DependencyProperty PanelStyleProperty = DependencyProperty.Register(
            nameof(PanelStyle), typeof(Style), typeof(SlidingPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register(
            nameof(ContentMargin), typeof(Thickness), typeof(SlidingPanel), new PropertyMetadata(new Thickness(0)));

        public static readonly DependencyProperty ContentHorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(ContentHorizontalAlignment), typeof(HorizontalAlignment), typeof(SlidingPanel), new PropertyMetadata(HorizontalAlignment.Center));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public object CurrentContent
        {
            get { return (object)GetValue(CurrentContentProperty); }
            set { SetValue(CurrentContentProperty, value); }
        }

        public Style PanelStyle
        {
            get { return (Style)GetValue(PanelStyleProperty); }
            set { SetValue(PanelStyleProperty, value); }
        }

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        public HorizontalAlignment ContentHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(ContentHorizontalAlignmentProperty); }
            set { SetValue(ContentHorizontalAlignmentProperty, value); }
        }

        public SlidingPanel()
        {
            InitializeComponent();
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (SlidingPanel)d;
            if ((bool)e.NewValue)
                control.ShowPanel();
            else
                control.HidePanel();
        }

        public void ShowPanel()
        {
            var anim = new DoubleAnimation(0, TimeSpan.FromMilliseconds(300))
            {
                EasingFunction = new QuadraticEase()
            };
            SlideTransform.BeginAnimation(TranslateTransform.XProperty, anim);
        }

        public void HidePanel()
        {
            var anim = new DoubleAnimation(-PanelRoot.ActualWidth, TimeSpan.FromMilliseconds(300))
            {
                EasingFunction = new QuadraticEase()
            };
            SlideTransform.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }
}
