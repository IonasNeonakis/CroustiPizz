
using CroustiPizz.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : BaseContentPage
    {
        public OrdersPage()
        {
            BindingContext = new OrdersViewModel();
            InitializeComponent();
        }
    }
}