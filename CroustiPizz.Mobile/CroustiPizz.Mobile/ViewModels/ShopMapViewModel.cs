using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class ShopMapViewModel : ViewModelBase
    {
        private ObservableCollection<ShopItem> _shops;

        public ObservableCollection<ShopItem> Shops
        {
            get => _shops;
            set => SetProperty(ref _shops, value);
        }

     
        public override async Task OnResume()
        {
            await base.OnResume();
           
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<List<ShopItem>> response = await service.ListShops();

            if (response.IsSuccess)
            {
                Shops = new ObservableCollection<ShopItem>(response.Data);
            }
            
            
        }
    }
}