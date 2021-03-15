using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

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

        private Map _maMap;

        public Map MaMap
        {
            get => _maMap;
            set => SetProperty(ref _maMap, value);
        }
        


        public override async Task OnResume()
        {
            await base.OnResume();
            
            
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<List<ShopItem>> response = await service.ListShops();

            if (response.IsSuccess)
            {
                Shops = new ObservableCollection<ShopItem>(response.Data);

                Position position;
                MapSpan mapSpan;
                Location location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null) {
                    position = new Position(location.Latitude, location.Longitude);
                    mapSpan = new MapSpan(position, 0.01, 0.01);
                }else {
                    position = new Position(46.4547, 2.2529);
                    mapSpan = new MapSpan(position, 12, 12);
                }
                MaMap = new Map(mapSpan)
                {
                    IsShowingUser = true
                };
                
                foreach (ShopItem unShop in Shops)
                {
                    Pin pin = new Pin
                    {
                        Label = unShop.Name,
                        Address = unShop.Address,
                        Type = PinType.Place,
                        Position = new Position(unShop.Latitude, unShop.Longitude)
                    };
                    MaMap.Pins.Add(pin);
                }
                
                
                
                
            }
            
            
        }
    }
}