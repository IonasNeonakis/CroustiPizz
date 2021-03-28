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
    /// <summary>
    /// ViewModel du panier
    /// </summary>
    public class ShopCartViewModel : ViewModelBase
    {
        public ICommand CloseShopCartPopupCommand { get; }

        public ICommand CommanderCommand { get; }

        public ICommand ViderCorbeilleCommand { get; }

        private ICartService CartService { get; set; }

        /// <summary>
        /// Action lorsqu'on clique sur supprimer une pizza
        /// </summary>
        public ICommand SupprimerPizzas
        {
            get
            {
                return new Command(e =>
                {
                    PizzaItem pizza = e as PizzaItem;

                    Total = Total - pizza.Price * pizza.Quantite;

                    CartService.RemoveAllFromCart(ShopId, pizza.Id);

                    Cart.Remove(pizza);

                    MessagingCenter.Send(this, "CartUpdated");
                });
            }
        }

        /// <summary>
        /// L'objet panier à afficher
        /// </summary>
        private ObservableCollection<PizzaItem> _cart;

        public ObservableCollection<PizzaItem> Cart
        {
            get => _cart;
            set => SetProperty(ref _cart, value);
        }

        /// <summary>
        /// Le nom de la pizzeria
        /// </summary>
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

        
        /// <summary>
        /// Le total d'un panier
        /// </summary>
        private double _total;

        public double Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        /// <summary>
        /// Constructeur qui récupère les paramètres de navigation
        /// </summary>
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
            Total = 0;

            CartService.EmptyCart(ShopId);

            Cart = new ObservableCollection<PizzaItem>();

            MessagingCenter.Send(this, "CartUpdated");
        }

        private void CloseShopCartPopupAction()
        {
            MessagingCenter.Send(this, "CartUpdated");
            PopupNavigation.Instance.PopAsync();
        }


        /// <summary>
        /// Récuperation des données depuis l'api
        /// </summary>
        public override async Task OnResume()
        {
            await base.OnResume();


            CartService = DependencyService.Get<ICartService>();
            IPizzaApiService apiService = DependencyService.Get<IPizzaApiService>();

            Dictionary<long, Dictionary<long, int>> cartDur = CartService.GetCart();

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
                DependencyService.Get<IMessage>()
                    .LongAlert(Resources.AppResources.AlertCartAccessError + response.ErrorMessage);
            }
        }

        /// <summary>
        /// Passe une comamnde avec le panier courant
        /// </summary>
        private async void Commander()
        {
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            if (CartService.GetCart().ContainsKey(ShopId))
            {
                List<long> listID = CartService.GetListId(ShopId);

                CreateOrderRequest body = new CreateOrderRequest();
                body.PizzaIds = listID;

                Response<OrderItem> response = await service.OrderPizzas(ShopId, body);


                if (response.IsSuccess)
                {
                    CartService.EmptyCart(ShopId);
                    CloseShopCartPopupAction();
                    DependencyService.Get<IMessage>().LongAlert(Resources.AppResources.AlertPlacingOrderSuccess);
                }
                else
                {
                    DependencyService.Get<IMessage>()
                        .LongAlert(Resources.AppResources.AlertPlacingOrderError + response.ErrorMessage);
                }
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(Resources.AppResources.AlertEmptyCart);
            }
        }
    }
}