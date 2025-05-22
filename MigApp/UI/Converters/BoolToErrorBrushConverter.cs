using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MigApp.UI.Converters
{
    /// <summary>
    /// Преобразует булевое значение в кисть для отображения ошибки.
    /// </summary>
    /// <remarks>
    /// Если значение <see langword="true"/>, возвращается <see cref="ErrorBrush"/> (по умолчанию <see cref="Brushes.Red"/>),
    /// иначе возвращается <see cref="NormalBrush"/> (по умолчанию <see cref="Brushes.Transparent"/>).
    /// </remarks>
    public sealed class BoolToErrorBrushConverter : IValueConverter
    {
        public Brush ErrorBrush { get; set; } = Brushes.Red;
        public Brush NormalBrush { get; set; } = Brushes.Transparent;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && b ? ErrorBrush : NormalBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
