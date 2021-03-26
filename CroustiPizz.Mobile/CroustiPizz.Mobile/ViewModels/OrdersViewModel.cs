using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Interfaces;
using CroustiPizz.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        private ObservableCollection<OrderItem> _orders;
        public ObservableCollection<OrderItem> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        
        public OrdersViewModel()
        {
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
            Response<List<OrderItem>> response = await service.ListOrders();

            if (response.IsSuccess)
            {
                response.Data.Reverse();
                Orders = new ObservableCollection<OrderItem>(response.Data);
                
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert( "Probleme d'accès à vos commandes " + response.ErrorMessage );

            }
            
        }


    }
}