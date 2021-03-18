using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Services;
using Rg.Plugins.Popup.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class ShopCartViewModel : ViewModelBase
    {
        public ICommand CloseShopCartPopupCommand { get; }

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


        public ShopCartViewModel()
        {
            CloseShopCartPopupCommand = new Command(CloseShopCartPopupAction);

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
            
                        
            CartService service = DependencyService.Get<CartService>();

            Dictionary<long, Dictionary<long, int>> cartDur = service.Cart;

            Dictionary<long, int> idPizQuantite = cartDur[ShopId];

            Cart = new ObservableCollection<PizzaItem>();
        }
    }
}