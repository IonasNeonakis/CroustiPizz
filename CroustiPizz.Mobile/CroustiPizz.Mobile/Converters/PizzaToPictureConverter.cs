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
            PizzaItem pizzaId = (PizzaItem) value;
            VisualElement sender = parameter as VisualElement;
            PizzaListShopViewModel viewModel = sender?.BindingContext as PizzaListShopViewModel;

            if (viewModel != null)
            {
                return "https://pizza.julienmialon.ovh/api/v1/shops/" + viewModel.ShopId + "/pizzas/" + pizzaId.Id + "/image";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}