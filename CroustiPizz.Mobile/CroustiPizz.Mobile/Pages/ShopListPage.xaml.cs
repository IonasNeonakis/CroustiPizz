using CroustiPizz.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopListPage : BaseContentPage
    {
        public ShopListPage()
        {
            BindingContext = new ShopListViewModel();
            InitializeComponent();
        }
    }
}