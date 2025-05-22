using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MigApp.UI.Converters
{
    /// <summary>
    /// Преобразует bool в Visibility. Поддерживает инверсию через свойство Invert или параметр "Invert".
    /// </summary>
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = value is bool b && b;

            if (IsInverted(parameter))
                boolValue = !boolValue;

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = value is Visibility v && v == Visibility.Visible;
            if (IsInverted(parameter))
                result = !result;

            return result;
        }

        private bool IsInverted(object parameter) =>
            Invert || parameter?.ToString()?.Equals("Invert", StringComparison.OrdinalIgnoreCase) == true;

    }
}
