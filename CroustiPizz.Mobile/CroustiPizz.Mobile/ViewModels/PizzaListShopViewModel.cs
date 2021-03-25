using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Interfaces;
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
        private bool _backButtonPressed;

        private ObservableCollection<PizzaItem> _pizzas;

        public ObservableCollection<PizzaItem> Pizzas
        {
            get => _pizzas;
            set => SetProperty(ref _pizzas, value);
        }

        private ObservableCollection<PizzaItem> _displayedPizzas;

        public ObservableCollection<PizzaItem> DisplayedPizzas
        {
            get => _displayedPizzas;
            set => SetProperty(ref _displayedPizzas, value);
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

        private long _cartQuantity;

        public long CartQuantity
        {
            get => _cartQuantity;
            set => SetProperty(ref _cartQuantity, value);
        }

        private string _filter;

        public string Filter
        {
            get => _filter;
            set => SetProperty(ref _filter, value);
        }

        private ICartService CartService { get; set; }

        public ICommand SelectedCommand { get; }
        public ICommand GoToCartCommand { get; }
        public ICommand FilterReturnCommand { get; }


        public ICommand CommandeAjoutPanier
        {
            get
            {
                return new Command(e =>
                {
                    PizzaItem pizza = e as PizzaItem;


                    if (!pizza.OutOfStock)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Ajout de " + pizza.Quantite + " " + pizza.Name);
                        CartService.AddToCart(ShopId, pizza.Id, pizza.Quantite);
                        CartQuantity = CartService.NumberOfItems(ShopId);
                    }
                }, e =>
                {
                    PizzaItem pizza = e as PizzaItem;
                    return pizza != null && !pizza.OutOfStock;
                });
            }
        }


        public ICommand IncrementerQuantite
        {
            get
            {
                return new Command(e =>
                {
                    PizzaItem pizza = e as PizzaItem;
                    pizza.Quantite++;
                }, e =>
                {
                    PizzaItem pizza = e as PizzaItem;
                    return pizza != null && !pizza.OutOfStock;
                });
            }
        }

        public ICommand DecrementerQuantite
        {
            get
            {
                return new Command(e =>
                {
                    PizzaItem pizza = e as PizzaItem;
                    if (pizza.Quantite > 1)
                    {
                        pizza.Quantite--;
                    }
                }, e =>
                {
                    PizzaItem pizza = e as PizzaItem;
                    return pizza != null && !pizza.OutOfStock;
                });
            }
        }

        public ICommand BackCommand { get; }

        public ICommand DetailsCommand
        {
            get
            {
                return new Command(e =>
                {

                    if (PopupNavigation.Instance.PopupStack.Count == 0)
                    {
                        PizzaItem pizzaItem = e as PizzaItem;
                        PopupNavigation.Instance.PushAsync(new PizzaDetailsPopup(new Dictionary<string, object>()
                        {
                            {"PizzaName", pizzaItem.Name},
                            {"PizzaDescription", pizzaItem.Description},
                            {"PizzaPhoto", pizzaItem.Url},
                            {"PizzaPrice", pizzaItem.Price}
                        }));
                    }
                });
            }
        }

        public PizzaListShopViewModel()
        {
            GoToCartCommand = new Command(GoToCartAction);
            BackCommand = new Command(BackAction);
            FilterReturnCommand = new Command(FilterReturnAction);

            Pizzas = new ObservableCollection<PizzaItem>();
            DisplayedPizzas = new ObservableCollection<PizzaItem>();

            MessagingCenter.Subscribe<ShopCartViewModel>(this, "CartUpdated",
                (sender) => { CartQuantity = CartService.NumberOfItems(ShopId); });
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
            _backButtonPressed = false;
            CartService = DependencyService.Get<ICartService>();
            CartQuantity = CartService.NumberOfItems(ShopId);


            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<List<PizzaItem>> response = await service.ListPizzas(ShopId);

            if (response.IsSuccess)
            {
                Pizzas = new ObservableCollection<PizzaItem>(response.Data);
                Pizzas.ForEach(el =>
                {
                    el.Url = "https://pizza.julienmialon.ovh/api/v1/shops/" + ShopId + "/pizzas/" + el.Id + "/image";
                    el.Quantite = el.OutOfStock ? 0 : 1;
                });
                DisplayedPizzas = Pizzas;
            }
            else
            {
                Pizzas = new ObservableCollection<PizzaItem>();
                DependencyService.Get<IMessage>().LongAlert("Probleme d'accès aux pizzerias " + response.ErrorMessage);
            }
        }

        public void GoToCartAction()
        {
            if (PopupNavigation.Instance.PopupStack.Count == 0)
            {
                PopupNavigation.Instance.PushAsync(new ShopCartPopup(new Dictionary<string, object>()
                {
                    {"ShopName", ShopName},
                    {"ShopId", ShopId}
                }));
            }
        }

        private void BackAction()
        {
            if (!_backButtonPressed)
            {
                _backButtonPressed = true;
                INavigationService navigationService = DependencyService.Get<INavigationService>();
                navigationService.PopAsync();
            }
        }

        private void FilterReturnAction()
        {
            if (Filter == "")
            {
                DisplayedPizzas = Pizzas;
            }
            else
            {
                DisplayedPizzas = Pizzas;
                DisplayedPizzas =
                    new ObservableCollection<PizzaItem>(DisplayedPizzas.Where(el =>
                        el.Name.ToLower().Contains(Filter.ToLower())));
            }
        }
    }
}