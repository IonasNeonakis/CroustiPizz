using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaDetailsPopup : PopupPage
    {
        public PizzaDetailsPopup(Dictionary<string, object> dico)
        {
            InitializeComponent();
            BindingContext = new PizzaDetailsViewModel(dico);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is PizzaDetailsViewModel pizzaDetailsViewModel)
            {
                await pizzaDetailsViewModel.OnResume();
            }
        }
        
        private async void OnCancelClicked(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}