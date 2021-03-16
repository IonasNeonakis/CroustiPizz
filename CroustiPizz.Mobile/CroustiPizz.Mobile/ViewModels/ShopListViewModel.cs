using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Pages;
using CroustiPizz.Mobile.Services;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class ShopListViewModel : ViewModelBase
    {
	    private ObservableCollection<ShopItem> _shops;

	    public ObservableCollection<ShopItem> Shops
	    {
		    get => _shops;
		    set => SetProperty(ref _shops, value);
	    }

		public ICommand SelectedCommand { get; }

	    public ShopListViewModel()
	    {
		    SelectedCommand = new Command<ShopItem>(SelectedAction);
	    }

	    private void SelectedAction(ShopItem obj)
	    {
		    INavigationService navigationService = DependencyService.Get<INavigationService>();
		    navigationService.PushAsync<PizzaListShopPage>(new Dictionary<string, object>()
		    {
			    {"ShopName", obj.Name}
		    });
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