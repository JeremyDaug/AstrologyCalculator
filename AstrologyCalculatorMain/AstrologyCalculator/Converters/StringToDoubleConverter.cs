using System;
using System.Globalization;
using System.Windows.Data;

namespace AstrologyCalculator.Converters
{
    class StringToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(string))
            {
                return null;
            }

            return double.Parse((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(double))
            {
                return null;
            }

            return value.ToString();
        }
    }
}
