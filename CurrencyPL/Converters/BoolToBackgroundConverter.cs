using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CurrencyPL
{
    public class BoolToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isClick;
            bool.TryParse(value.ToString(), out isClick);
            if (isClick)
                return  new SolidColorBrush(Colors.LightBlue);
            return  new SolidColorBrush(Colors.LightGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
