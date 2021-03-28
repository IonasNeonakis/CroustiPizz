using System;
using System.Globalization;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.Converters
{
/// <summary>
/// Converter pour afficher une distance en mètres 
/// </summary>
    public class DistanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double distance = (double) value;
            if (double.IsNegativeInfinity(distance)) // si la geoloc n'est pas donnée
            {
                return "";
            }

            if (distance > 1000)
            {
                distance = distance / 1000;
                return distance.ToString("0.0") + " km";
            }

            return distance.ToString("0") + " m";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}