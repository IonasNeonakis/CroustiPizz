using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Pages;
using CroustiPizz.Mobile.Services;
using Rg.Plugins.Popup.Services;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
        
        private long _itemQuantity;

        public long ItemQuantity
        {
            get => _itemQuantity;
            set => SetProperty(ref _itemQuantity, value);
        }

        public ICommand SelectedCommand { get; }
        public ICommand GoToCartCommand { get; }

        public ICommand BackCommand { get; }
        public ICommand LessCommand { get; }
        public ICommand MoreCommand { get; }

        public PizzaListShopViewModel()
        {
            SelectedCommand = new Command<PizzaItem>(SelectedAction);
            GoToCartCommand = new Command(GoToCartAction);
            BackCommand = new Command(BackAction);
            LessCommand = new Command(LessAction);
            MoreCommand = new Command(MoreAction);
        }

        private void SelectedAction(PizzaItem obj)
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PushAsync<PizzaDetailsPage>(new Dictionary<string, object>()
            {
                {"PizzaImage", ShopId},
                {"PizzaId", obj.Id},
                {"PizzaName", obj.Name},
                {"PizzaDescription", obj.Description},
                {"PizzaPrice", obj.Price}
            });
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

            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<List<PizzaItem>> response = await service.ListPizzas(ShopId);

            if (response.IsSuccess)
            {
                Pizzas = new ObservableCollection<PizzaItem>(response.Data);
                Pizzas.ForEach(el =>
                    el.Url = "https://pizza.julienmialon.ovh/api/v1/shops/" + ShopId + "/pizzas/" + el.Id + "/image");
            }
        }

        private void GoToCartAction()
        {
            PopupNavigation.Instance.PushAsync(new ShopCartPopup());
        }

        private void BackAction()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PopAsync();
        }
        
        private void LessAction()
        {
        }

        private void MoreAction()
        {
            
        }
    }
}