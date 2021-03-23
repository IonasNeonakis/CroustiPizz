using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Interfaces;
using CroustiPizz.Mobile.Services;
using Rg.Plugins.Popup.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class ShopCartViewModel : ViewModelBase
    {
        public ICommand CloseShopCartPopupCommand { get; }
        
        public ICommand CommanderCommand { get; }

        public ICommand ViderCorbeilleCommand { get; }

        public ICommand SupprimerPizzas
        {
            get
            {
                return new Command(e =>
                {
                    PizzaItem pizza = e as PizzaItem;
                    CartService service = DependencyService.Get<CartService>();

                    Total = Total - pizza.Price * pizza.Quantite;

                    service.RemoveAllFromCart(ShopId, pizza.Id);

                    Cart.Remove(pizza);
                });
            }
        }

        private ObservableCollection<PizzaItem> _cart;

        public ObservableCollection<PizzaItem> Cart
        {
            get => _cart;
            set => SetProperty(ref _cart, value);
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

        private double _total;

        public double Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }


        public ShopCartViewModel(Dictionary<string, object> dico)
        {
            CloseShopCartPopupCommand = new Command(CloseShopCartPopupAction);
            ShopName = dico["ShopName"] as string;
            ShopId = (long) dico["ShopId"];
            CommanderCommand = new Command(Commander);
            ViderCorbeilleCommand = new Command(ViderPanier);
        }

        private void ViderPanier()
        {
            CartService service = DependencyService.Get<CartService>();

            Total = 0;

            service.EmptyCart(ShopId);

            Cart = new ObservableCollection<PizzaItem>();
        }

        private void CloseShopCartPopupAction()
        {
            PopupNavigation.Instance.PopAsync();
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


            CartService cartService = DependencyService.Get<CartService>();
            IPizzaApiService apiService = DependencyService.Get<IPizzaApiService>();

            Dictionary<long, Dictionary<long, int>> cartDur = cartService.Cart;

            Dictionary<long, int> idPizQuantite =
                cartDur.ContainsKey(ShopId) ? cartDur[ShopId] : new Dictionary<long, int>();

            Response<List<PizzaItem>> response = await apiService.ListPizzas(ShopId);
            Cart = new ObservableCollection<PizzaItem>();

            if (response.IsSuccess)
            {
                foreach (PizzaItem pizzaItem in response.Data)
                {
                    if (idPizQuantite.ContainsKey(pizzaItem.Id))
                    {
                        pizzaItem.Quantite = idPizQuantite[pizzaItem.Id];
                        pizzaItem.Url = "https://pizza.julienmialon.ovh/api/v1/shops/" + ShopId + "/pizzas/" +
                                        pizzaItem.Id + "/image";
                        Cart.Add(pizzaItem);

                        Total += pizzaItem.Price * pizzaItem.Quantite;
                    }
                }
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert( "Probleme d'accès à votre panier" );
            }
        }

        private async void Commander()
        {
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            CartService cartService = DependencyService.Get<CartService>();

            List<long> listID = cartService.GetListId(ShopId);

            CreateOrderRequest body = new CreateOrderRequest();
            body.PizzaIds = listID;

            Response<OrderItem> response = await service.OrderPizzas(ShopId, body);


            if (response.IsSuccess)
            {
                cartService.EmptyCart(ShopId);
                CloseShopCartPopupAction();
                DependencyService.Get<IMessage>().LongAlert( "Commande passée avec succès" );

            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert( "Erreur dans la commande" );
            }
        }
    }
}