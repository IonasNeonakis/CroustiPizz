
using CroustiPizz.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentView
    {
        public OrdersPage()
        {
            BindingContext = new OrdersViewModel();
            InitializeComponent();
        }


        protected override async void OnParentSet()
        {
            base.OnParentSet();

            if (BindingContext is OrdersViewModel ordersViewModel)
            {
                await ordersViewModel.OnResume();
            }
        }
    }
}