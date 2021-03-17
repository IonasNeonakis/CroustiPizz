using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Pages;
using CroustiPizz.Mobile.Services;
using Storm.Mvvm;
using Storm.Mvvm.Services;
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

        private string _shopName;

        public string ShopName
        {
            get => _shopName;
            set => SetProperty(ref _shopName, value);
        }

        private long _shopId;

        public long ShopId
        {
            get => _shopId;
            set => SetProperty(ref _shopId, value);
        }

        public ICommand SelectedCommand { get; }

        public PizzaListShopViewModel()
        {
            SelectedCommand = new Command<PizzaItem>(SelectedAction);
            
            // Pizzas = new ObservableCollection<PizzaItem>();
            
        }

        private void SelectedAction(PizzaItem obj)
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PushAsync<PizzaDetailsPage>(new Dictionary<string, object>()
            {
                {"PizzaImage", ShopId},
                {"PizzaId", obj.Id},
                {"PizzaName", obj.Name},
                {"PizzaDescription", obj.Description},
                {"PizzaPrice", obj.Price}
            });
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);

            ShopName = GetNavigationParameter<string>("ShopName");
            ShopId = GetNavigationParameter<long>("ShopId");
        }

        public override async Task OnResume()
        {
            await base.OnResume();

            // PizzaItem pizzaItem = new PizzaItem
            // {
            //     Id = 1,
            //     Name = "truc",
            //     Description = "Du jambon, des olives, des grenouilles, du sel, des tomates, du gruyère râpé",
            //     Price = 10,
            //     OutOfStock = false
            // };
            //
            // PizzaItem pizzaItem2 = new PizzaItem
            // {
            //     Id = 2,
            //     Name = "trucMachain",
            //     Description = "Ntm LOLXD",
            //     Price = 10,
            //     OutOfStock = false
            // };
            //
            // Pizzas.Add(pizzaItem);
            // Pizzas.Add(pizzaItem2);
            
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<List<PizzaItem>> response = await service.ListPizzas(_shopId);
            
            if (response.IsSuccess)
            {
                Pizzas = new ObservableCollection<PizzaItem>(response.Data);
            }
        }

        public string GetPizzaPicture(PizzaItem pizzaItem)
        {
            return "https://pizza.julienmialon.ovh/api/v1/shops/" + ShopId + "/pizzas/" + pizzaItem.Id + "/image";
        }
    }
}