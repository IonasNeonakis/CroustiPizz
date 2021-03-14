using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Services;
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

        public PizzaListShopViewModel()
        {
            SelectedCommand = new Command<PizzaItem>(SelectedAction);
        }

        private void SelectedAction(PizzaItem obj)
        {
            
        }

        public override async Task OnResume()
        {
            await base.OnResume();

            PizzaItem pizzaItem = new PizzaItem
            {
                Id = 1,
                Name = "truc",
                Description = "Ntm Romain",
                Price = 10,
                OutOfStock = false
            };
            
            _pizzas.Add(pizzaItem);
            

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
    }
}