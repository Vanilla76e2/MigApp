using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MigApp.UI.Controls
{
    [ContentProperty(nameof(InputContent))]
    public partial class LabeledInput : UserControl
    {
        public LabeledInput() => InitializeComponent();

        #region Label

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(LabeledInput), new PropertyMetadata(string.Empty));

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(nameof(IsRequired), typeof(bool), typeof(LabeledInput), new PropertyMetadata(false));

        public bool IsRequired
        {
            get => (bool)GetValue(IsRequiredProperty);
            set => SetValue(IsRequiredProperty, value);
        }

        //public static readonly DependencyProperty LabelWidthProperty =
        //    DependencyProperty.Register(nameof(LabelWidth), typeof(GridLength), typeof(LabeledInput), new PropertyMetadata(new GridLength(1, GridUnitType.Auto)));

        //public GridLength LabelWidth
        //{
        //    get => (GridLength)GetValue(LabelWidthProperty);
        //    set => SetValue(LabelWidthProperty, value);
        //}

        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register(nameof(LabelWidth), typeof(double), typeof(LabeledInput), new PropertyMetadata(double.NaN));

        public double LabelWidth
        {
            get => (double)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        public static readonly DependencyProperty LabelMinWidthProperty =
            DependencyProperty.Register(nameof(LabelMinWidth), typeof(double), typeof(LabeledInput), new PropertyMetadata(0d));

        public double LabelMinWidth
        {
            get => (double)GetValue(LabelMinWidthProperty);
            set => SetValue(LabelMinWidthProperty, value);
        }

        public static readonly DependencyProperty LabelMaxWidthProperty =
            DependencyProperty.Register(nameof(LabelMaxWidth), typeof(double), typeof(LabeledInput), new PropertyMetadata(double.MaxValue));

        public double LabelMaxWidth
        {
            get => (double)GetValue(LabelMaxWidthProperty);
            set => SetValue(LabelMaxWidthProperty, value);
        }

        public static readonly DependencyProperty LabelHeightProperty =
            DependencyProperty.Register(nameof(LabelHeight), typeof(double), typeof(LabeledInput), new PropertyMetadata(double.NaN));

        public double LabelHeight
        {
            get => (double)GetValue(LabelHeightProperty);
            set => SetValue(LabelHeightProperty, value);
        }

        public static readonly DependencyProperty LabelMinHeightProperty =
            DependencyProperty.Register(nameof(LabelMinHeight), typeof(double), typeof(LabeledInput), new PropertyMetadata(0d));

        public double LabelMinHeight
        {
            get => (double)GetValue(LabelMinHeightProperty);
            set => SetValue(LabelMinHeightProperty, value);
        }

        public static readonly DependencyProperty LabelMaxHeightProperty =
            DependencyProperty.Register(nameof(LabelMaxHeight), typeof(double), typeof(LabeledInput), new PropertyMetadata(double.MaxValue));

        public double LabelMaxHeight
        {
            get => (double)GetValue(LabelMaxHeightProperty);
            set => SetValue(LabelMaxHeightProperty, value);
        }

        public static readonly DependencyProperty LabelHorizontalAlignmentProperty =
            DependencyProperty.Register(nameof(LabelHorizontalAlignment), typeof(HorizontalAlignment), typeof(LabeledInput), new PropertyMetadata(HorizontalAlignment.Left));
        public HorizontalAlignment LabelHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(LabelHorizontalAlignmentProperty);
            set => SetValue(LabelHorizontalAlignmentProperty, value);
        }

        #endregion

        #region Input

        //public static readonly DependencyProperty InputWidthProperty =
        //    DependencyProperty.Register(nameof(InputWidth), typeof(GridLength), typeof(LabeledInput), new PropertyMetadata(new GridLength(1, GridUnitType.Auto)));

        //public GridLength InputWidth
        //{
        //    get => (GridLength)GetValue(InputWidthProperty);
        //    set => SetValue(InputWidthProperty, value);
        //}

        public static readonly DependencyProperty InputWidthProperty =
            DependencyProperty.Register(nameof(InputWidth), typeof(double), typeof(LabeledInput), new PropertyMetadata(double.NaN));

        public double InputWidth
        {
            get => (double)GetValue(InputWidthProperty);
            set => SetValue(InputWidthProperty, value);
        }

        public static readonly DependencyProperty InputMinWidthProperty =
            DependencyProperty.Register(nameof(InputMinWidth), typeof(double), typeof(LabeledInput), new PropertyMetadata(0d));

        public double InputMinWidth
        {
            get => (double)GetValue(InputMinWidthProperty);
            set => SetValue(InputMinWidthProperty, value);
        }

        public static readonly DependencyProperty InputMaxWidthProperty =
            DependencyProperty.Register(nameof(InputMaxWidth), typeof(double), typeof(LabeledInput), new PropertyMetadata(double.PositiveInfinity));

        public double InputMaxWidth
        {
            get => (double)GetValue(InputMaxWidthProperty);
            set => SetValue(InputMaxWidthProperty, value);
        }

        public static readonly DependencyProperty InputHeightProperty =
            DependencyProperty.Register(nameof(InputHeight), typeof(double), typeof(LabeledInput), new PropertyMetadata(double.NaN));

        public double InputHeight
        {
            get => (double)GetValue(InputHeightProperty);
            set => SetValue(InputHeightProperty, value);
        }

        public static readonly DependencyProperty InputMinHeightProperty =
            DependencyProperty.Register(nameof(InputMinHeight), typeof(double), typeof(LabeledInput), new PropertyMetadata(0d));

        public double InputMinHeight
        {
            get => (double)GetValue(InputMinHeightProperty);
            set => SetValue(InputMinHeightProperty, value);
        }

        public static readonly DependencyProperty InputMaxHeightProperty =
            DependencyProperty.Register(nameof(InputMaxHeight), typeof(double), typeof(LabeledInput), new PropertyMetadata(double.PositiveInfinity));

        public double InputMaxHeight
        {
            get => (double)GetValue(InputMaxHeightProperty);
            set => SetValue(InputMaxHeightProperty, value);
        }

        public static readonly DependencyProperty InputHorizontalAlignmentProperty =
            DependencyProperty.Register(nameof(InputHorizontalAlignment), typeof(HorizontalAlignment), typeof(LabeledInput), new PropertyMetadata(HorizontalAlignment.Stretch));
        public HorizontalAlignment InputHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(InputHorizontalAlignmentProperty);
            set => SetValue(InputHorizontalAlignmentProperty, value);
        }

        public static readonly DependencyProperty InputContentProperty =
            DependencyProperty.Register(nameof(InputContent), typeof(object), typeof(LabeledInput), new PropertyMetadata(null));

        public object InputContent
        {
            get => GetValue(InputContentProperty);
            set => SetValue(InputContentProperty, value);
        }

        #endregion

        #region Layout

        //public static readonly DependencyProperty GapWidthProperty =
        //    DependencyProperty.Register(nameof(GapWidth), typeof(GridLength), typeof(LabeledInput), new PropertyMetadata(new GridLength(1, GridUnitType.Auto)));

        //public GridLength GapWidth
        //{
        //    get => (GridLength)GetValue(GapWidthProperty);
        //    set => SetValue(GapWidthProperty, value);
        //}

        public static readonly DependencyProperty GapWidthProperty =
            DependencyProperty.Register(nameof(GapWidth), typeof(double), typeof(LabeledInput), new PropertyMetadata(double.NaN));

        public double GapWidth
        {
            get => (double)GetValue(GapWidthProperty);
            set => SetValue(GapWidthProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(LabeledInput), new PropertyMetadata(Orientation.Horizontal));

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty IsErrorProperty =
            DependencyProperty.Register(nameof(IsError), typeof(bool), typeof(LabeledInput), new PropertyMetadata(false));
        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, value);
        }

        #endregion
    }
}
