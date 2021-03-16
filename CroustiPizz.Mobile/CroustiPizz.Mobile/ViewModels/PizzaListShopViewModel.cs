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

        public ICommand SelectedCommand { get; }

        public PizzaListShopViewModel()
        {
            SelectedCommand = new Command<PizzaItem>(SelectedAction);
            
            // Pizzas = new ObservableCollection<PizzaItem>();
            
        }

        private void SelectedAction(PizzaItem obj)
        {
            
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
            
            Response<List<PizzaItem>> response = await service.ListPizzas(1);
            
            if (response.IsSuccess)
            {
                Pizzas = new ObservableCollection<PizzaItem>(response.Data);
            }
        }
    }
}