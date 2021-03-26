using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using CroustiPizz.Mobile.Interfaces;
using CroustiPizz.Mobile.Pages;
using CroustiPizz.Mobile.Services;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class ShopListViewModel : ViewModelBase
    {
	    private bool _selected;
	    
	    private ObservableCollection<ShopItem> _shops;

	    private Location _userLocation;

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
			
		    if (!_selected)
		    {
			    _selected = true;
			    INavigationService navigationService = DependencyService.Get<INavigationService>();
			    navigationService.PushAsync<PizzaListShopPage>(new Dictionary<string, object>
			    {
				    {"ShopName", obj.Name},
				    {"ShopId", obj.Id}
			    });
		    }

		}

	    public override async Task OnResume()
        {
	        await base.OnResume();
	        _selected = false;
	        
	        try {
		        _userLocation = await Geolocation.GetLastKnownLocationAsync();
	        }catch (PermissionException) {
		        _userLocation = null;
	        }
	        
	        IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

	        Response<List<ShopItem>> response = await service.ListShops();

	        if (response.IsSuccess)
	        {
		        List<ShopItem> list = response.Data;
		        
		        list.Sort(DistancePoint);


		        Shops = new ObservableCollection<ShopItem>(list);
				
				foreach (ShopItem shopItem in Shops)
				{
					if (_userLocation == null) // si la position n'est pas donnée alors on met la distance a -oo
					{
						shopItem.Distance = Double.NegativeInfinity;
					}else{
						shopItem.Distance = GetDistance(shopItem.Longitude, shopItem.Latitude, _userLocation.Longitude, _userLocation.Latitude);
					}
				}
				
	        }
	        else
	        {
		        Shops = new ObservableCollection<ShopItem>();
		        DependencyService.Get<IMessage>().LongAlert( Resources.AppResources.AlertPizzaShopAccessError + response.ErrorMessage );

	        }
        }

	    private int DistancePoint(ShopItem item1, ShopItem item2)
	    {
		    if (_userLocation == null) // si la position de l'utilisateur n'est pas donnée alors on laisse la liste tel quelle
		    {
			    return String.Compare(item1.Name, item2.Name, StringComparison.Ordinal);
		    }
		    
		    double distanceX = GetDistance(item1.Longitude, item1.Latitude, _userLocation.Longitude, _userLocation.Latitude);
		    double distanceY = GetDistance(item2.Longitude, item2.Latitude, _userLocation.Longitude, _userLocation.Latitude);

		    
		    //le deuxieme plus grand on return -1
		    if (distanceY > distanceX)
		    {
			    return -1;
		    }
		    //le premier plus grand on return 1
		    if (distanceX > distanceY)
		    {
			    return 1;
		    }
		    //egaux on return 0
		    return 0;


	    }
	    
	    
	    private double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
	    {
		    var d1 = latitude * (Math.PI / 180.0);
		    var num1 = longitude * (Math.PI / 180.0);
		    var d2 = otherLatitude * (Math.PI / 180.0);
		    var num2 = otherLongitude * (Math.PI / 180.0) - num1;
		    var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
    
		    return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
	    }
    }
}