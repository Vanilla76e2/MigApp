using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MigApp.UI.Converters
{
    /// <summary>
    /// Показывает Visible только если Orientation.Horizontal. Иначе Collapsed.
    /// </summary>
    public sealed class HorizontalOnlyVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Orientation orientation && orientation == Orientation.Horizontal
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
