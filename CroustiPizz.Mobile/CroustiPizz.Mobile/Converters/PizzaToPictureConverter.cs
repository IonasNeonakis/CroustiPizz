using System;
using System.Globalization;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.ViewModels;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.Converters
{
    public class PizzaToPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long pizzaId = (long) value;
            return "https://pizza.julienmialon.ovh/api/v1/shops/1/pizzas/" + pizzaId + "/image";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}