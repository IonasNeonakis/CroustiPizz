using System;
using System.Globalization;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Double truc = (Double) value;
            return truc.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}