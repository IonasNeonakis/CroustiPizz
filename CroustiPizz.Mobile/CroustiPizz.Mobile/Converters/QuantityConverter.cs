using System;
using System.Globalization;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.Converters
{
    public class QuantityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long quantite =  (long) value;
            if (quantite >= 100)
            {
                return "99+";
            }

            return quantite.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}