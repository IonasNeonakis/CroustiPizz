using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Controls;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Interfaces;
using CroustiPizz.Mobile.Pages;
using CroustiPizz.Mobile.Services;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

namespace CroustiPizz.Mobile.ViewModels
{
    /// <summary>
    /// ViewModel de pizzerias sur la carte
    /// </summary>
    public class ShopMapViewModel : ViewModelBase
    {
        private bool _selected; // pour ne pas cliquer plusieurs fois sur un bouton
        private bool _locationAsked; // pour ne pas demander la localisation en boucle 
        private Location _location; // localisation de l'utilisateur

        private ObservableCollection<ShopItem> _shops; // la liste des pizzerias

        public ObservableCollection<ShopItem> Shops
        {
            get => _shops;
            set => SetProperty(ref _shops, value);
        }

        private Map _maMap; // La map actuelle

        public Map MaMap
        {
            get => _maMap;
            set => SetProperty(ref _maMap, value);
        }

        private bool _visible; // savoir si on cache ou pas les détails d'une pizzeria

        public bool Visible
        {
            get => _visible;
            set => SetProperty(ref _visible, value);
        }

        private ShopItem _pizzeria;

        public ShopItem Pizzeria
        {
            get => _pizzeria;
            set => SetProperty(ref _pizzeria, value);
        }

        public ShopMapViewModel()
        {
            Visible = false;
            ClickPizzeria = new Command(SelectionPizzeria);
            _locationAsked = false;
        }

        public ICommand ClickPizzeria { get; }

        /// <summary>
        /// On récupère toutes les pizzerias et on instancie la carte
        /// Si on a la localisation on zoom sur l'utilisateur, sinon toute la france est affichée
        /// </summary>
        public override async Task OnResume()
        {
            await base.OnResume();
            _selected = false;
            Visible = false;

            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
            Response<List<ShopItem>> response = await service.ListShops();

            if (response.IsSuccess)
            {
                Shops = new ObservableCollection<ShopItem>(response.Data);
                Position position;
                MapSpan mapSpan;
                if (!_locationAsked)
                {
                    try
                    {
                        _location = await Geolocation.GetLastKnownLocationAsync();
                    }
                    catch (PermissionException)
                    {
                        _locationAsked = true;
                        _location = null;
                    }
                }

                if (_location != null)
                {
                    position = new Position(_location.Latitude, _location.Longitude);
                    mapSpan = new MapSpan(position, 0.01, 0.01);
                }
                else
                {
                    position = new Position(46.4547, 2.2529);
                    mapSpan = new MapSpan(position, 12, 12);
                }

                bool showUser = _location != null;

                MaMap = new Map(mapSpan)
                {
                    IsShowingUser = showUser
                };

                foreach (ShopItem unShop in Shops)
                {
                    BindablePin pin = new BindablePin
                    {
                        Label = unShop.Name,
                        Address = unShop.Address,
                        Type = PinType.Place,
                        Position = new Position(unShop.Latitude, unShop.Longitude)
                    };

                    pin.MarkerClicked += (s, args) =>
                    {
                        args.HideInfoWindow = true;
                        Visible = true;
                        Pizzeria = unShop;

                        Position position = new Position(unShop.Latitude, unShop.Longitude);
                        MaMap.MoveToRegion(new MapSpan(position, 0.01, 0.01));
                    };

                    MaMap.Pins.Add(pin);
                    MaMap.MapClicked += (s, e) => { Visible = false; };
                }
            }
            else
            {
                Position position = new Position(46.4547, 2.2529);
                MapSpan mapSpan = new MapSpan(position, 12, 12);
                MaMap = new Map(mapSpan);
                DependencyService.Get<IMessage>()
                    .LongAlert(Resources.AppResources.AlertPizzaShopAccessError + response.ErrorMessage);
            }
        }

        /// <summary>
        /// Permet de naviguer sur une pizzeria
        /// </summary>
        private void SelectionPizzeria()
        {
            if (!_selected)
            {
                _selected = true;
                INavigationService service = DependencyService.Get<INavigationService>();

                service.PushAsync<PizzaListShopPage>(new Dictionary<string, object>()
                {
                    {"ShopName", Pizzeria.Name},
                    {"ShopId", Pizzeria.Id}
                });
            }
        }
    }
}