using System;
using System.Collections.Generic;
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
        public ShopCartPopup(Dictionary<String, object> dico)
        {
            InitializeComponent();
            BindingContext = new ShopCartViewModel(dico);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ShopCartViewModel shopCartViewModel)
            {
                await shopCartViewModel.OnResume();
            }
        }
    }
}