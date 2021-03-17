using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Pages;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
         public ICommand ShowMapCommand { get; }
         
         public ICommand ShowListCommand { get; }

         public ICommand ShowOrdersCommand { get; }
         
         public ICommand ShowProfileCommand { get; }

         private bool _showingMap;
         public bool ShowingMap
         {
             get => _showingMap;
             set => SetProperty(ref _showingMap, value);
         }
         
         private bool _showingList;
         public bool ShowingList
         {
             get => _showingList;
             set => SetProperty(ref _showingList, value);
         }
         
         private bool _showingOrders;
         public bool ShowingOrders
         {
             get => _showingOrders;
             set => SetProperty(ref _showingOrders, value);
         }
         
         /*
         private bool _showingProfile;
         public bool ShowingProfile
         {
             get => _showingProfile;
             set => SetProperty(ref _showingProfile, value);
         }
         */

         public MainViewModel()
         {
             ShowMapCommand = new Command(ShowMapAction);
             ShowListCommand = new Command(ShowListAction);
             ShowOrdersCommand = new Command(ShowOrdersAction);
             ShowProfileCommand = new Command(ShowProfileAction);

             ShowingMap = true;
             ShowingList = false;
             ShowingOrders = false;
             // ShowingProfile = false;
         }

         private void ShowMapAction()
         {
             ShowingMap = true;
             ShowingList = false;
             ShowingOrders = false;
             // ShowingProfile = false;
         }
         
         private void ShowListAction()
         {
             ShowingMap = false;
             ShowingList = true;
             ShowingOrders = false;
             // ShowingProfile = false;
         }
         
         private void ShowOrdersAction()
         {
             ShowingMap = false;
             ShowingList = false;
             ShowingOrders = true;
             // ShowingProfile = false;
         }
         
         private void ShowProfileAction()
         {
             /*ShowingMap = false;
             ShowingList = false;
             ShowingOrders = false;
             ShowingProfile = true;*/
             PopupNavigation.Instance.PushAsync(new ProfilePage());
         }

         public override async Task OnResume()
         {
            await base.OnResume();
            
            ShopMapViewModel shopMapViewModel = new ShopMapViewModel();
            await shopMapViewModel.OnResume();
         }
    }
}