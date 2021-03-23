using System.Collections.Generic;
using System.Collections.ObjectModel;
using CroustiPizz.Mobile.Controls;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Services;
using CroustiPizz.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Map = Xamarin.Forms.Maps.Map;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopMapPage : BaseContentPage
    {
        public ShopMapPage()
        {
            BindingContext = new ShopMapViewModel();
            InitializeComponent();
        }
    }
}