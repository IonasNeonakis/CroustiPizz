using CroustiPizz.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopListPage : ContentPage
    {
        public ShopListPage()
        {
            BindingContext = new ShopListViewModel();
            InitializeComponent();
        }
        
        protected override async void OnParentSet()
        {
            base.OnParentSet();

            if (BindingContext is ShopListViewModel shopListViewModel)
            {
                await shopListViewModel.OnResume();
            }
        }
    }
}