using System.Collections.ObjectModel;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos.Pizzas;
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

        public ShopCartViewModel()
        {
            CloseShopCartPopupCommand = new Command(CloseShopCartPopupAction);
            Cart = new ObservableCollection<PizzaItem>();
            Cart.Add(new PizzaItem()
            {
                Id = 1,
                Name = "Pizza de fou dingue",
                Description = "OQzdoinqzdoiNI oQZdoINQZDoin O QOZDNQOZ o OQZIQOIOIN?DOINQOnoinoqinzdoiqndoinq",
                Price = 20
            });
            
            Cart.Add(new PizzaItem()
            {
                Id = 1,
                Name = "Pizza de fou dingue 2",
                Description = "OQzdoinqzdoiNI oQZdoINQZDoin O QOZDNQOZ o OQZIQOIOIN?DOINQOnoinoqinzdoiqndoinq",
                Price = 90
            });
            
            Cart.Add(new PizzaItem()
            {
                Id = 1,
                Name = "Pizza de fou dingue 3",
                Description = "OQzdoin",
                Price = 40
            });
        }

        private void CloseShopCartPopupAction()
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}