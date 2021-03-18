using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class PizzaDetailsViewModel : ViewModelBase
    {
        
        private long _shopId;
        public long ShopId
        {
            get => _shopId;
            set => SetProperty(ref _shopId, value);
        }

        private long _pizzaId;
        public long PizzaId
        {
            get => _pizzaId;
            set => SetProperty(ref _pizzaId, value);
        }

        private string _pizzaName;
        public string PizzaName
        {
            get => _pizzaName;
            set => SetProperty(ref _pizzaName, value);
        }

        private string _pizzaDescription;
        public string PizzaDescription
        {
            get => _pizzaDescription;
            set => SetProperty(ref _pizzaDescription, value);
        }

        private double _pizzaPrice;
        public double PizzaPrice
        {
            get => _pizzaPrice;
            set => SetProperty(ref _pizzaPrice, value);
        }

        private long _itemQuantity = 1; // pour stocker la quantité unitaire que l'utilisateur désire 
        public long ItemQuantity
        {
            get => _itemQuantity;
            set => SetProperty(ref _itemQuantity, value);
        }

        private long _totalPrice;
        public long TotalPrice
        {
            get => _totalPrice;
            set => SetProperty(ref _totalPrice, value);
        }

        private string _pizzaPhoto;
        public string PizzaPhoto
        {
            get => _pizzaPhoto;
            set => SetProperty(ref _pizzaPhoto, value);
        }

        public ICommand BackCommand { get; }
        public ICommand MoreCommand { get; }
        public ICommand LessCommand { get; }

        public PizzaDetailsViewModel()
        {
            BackCommand = new Command(BackAction);
            MoreCommand = new Command(MoreAction);
            LessCommand = new Command(LessAction);
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);

            // ShopId = GetNavigationParameter<long>("ShopId");
            // PizzaId = GetNavigationParameter<long>("PizzaId");
            // PizzaName = GetNavigationParameter<string>("PizzaName");
            // PizzaDescription = GetNavigationParameter<string>("PizzaDescription");
            // PizzaPrice = GetNavigationParameter<double>("PizzaPrice");
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            
            ShopId = 1;
            PizzaId = 1;
            PizzaName = "Pizza Magic Jambom";
            PizzaDescription = "Jambon, tomates, pate, champignon, magie";
            PizzaPrice = 12;
            PizzaPhoto = "https://pizza.julienmialon.ovh/api/v1/shops/" + ShopId + "/pizzas/" + PizzaId + "/image";
        }

        private void BackAction()
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void LessAction()
        {
            if (ItemQuantity > 1)
            {
                ItemQuantity--;
            }
        }

        private void MoreAction()
        {
            if (ItemQuantity < 99)
            {
                ItemQuantity++;
            }
        }
    }
}