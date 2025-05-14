using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MigApp.Core.Converters
{
    class ResizeModeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ResizeMode resizeMode)
            {
                return resizeMode == ResizeMode.NoResize ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
