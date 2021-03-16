using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Pages;
using CroustiPizz.Mobile.Services;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class PizzaListShopViewModel : ViewModelBase
    {
        
        private ObservableCollection<PizzaItem> _pizzas;
        public ObservableCollection<PizzaItem> Pizzas
        {
            get => _pizzas;
            set => SetProperty(ref _pizzas, value);
        }

        private int _shopId = 1;
        public int ShopId
        {
            get => _shopId;
            set => SetProperty(ref _shopId, value);
        }

        public ICommand SelectedCommand { get; }
        public ICommand GoToCartCommand { get; }

        public PizzaListShopViewModel()
        {
            SelectedCommand = new Command<PizzaItem>(SelectedAction);
            GoToCartCommand = new Command(GoToCartAction);
            Pizzas = new ObservableCollection<PizzaItem>();
            
            PizzaItem pizzaItem = new PizzaItem
            {
                Id = 1,
                Name = "truc",
                Description = "Ntm Romain",
                Price = 10,
                OutOfStock = false
            };
            
            PizzaItem pizzaItem2 = new PizzaItem
            {
                Id = 2,
                Name = "trucMachain",
                Description = "Ntm LOLXD",
                Price = 10,
                OutOfStock = false
            };
            
            Pizzas.Add(pizzaItem);
            Pizzas.Add(pizzaItem2);
        }

        private void SelectedAction(PizzaItem obj)
        {
            
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            
            // IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
            //
            // Response<List<PizzaItem>> response = await service.ListPizzas(_shopId);
            //
            // Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
            // if (response.IsSuccess)
            // {
            //     Console.WriteLine($"Appel HTTP : {response.Data.Count}");
            //     Pizzas = new ObservableCollection<PizzaItem>(response.Data);
            // }
        }

        public void GoToCartAction()
        {
            PopupNavigation.Instance.PushAsync(new ShopCartPopup());
        }
    }
}