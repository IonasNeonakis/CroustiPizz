using System;
using CroustiPizz.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopCartPopup : PopupPage
    {
        public ShopCartPopup()
        {
            InitializeComponent();
            BindingContext = new ShopCartViewModel();
        }
    }
}