using CroustiPizz.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopListPage
    {
        public ShopListPage()
        {
            BindingContext = new ShopListViewModel();
            InitializeComponent();
        }
    }
}