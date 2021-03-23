using CroustiPizz.Mobile.Controls;
using CroustiPizz.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : CroustiTabs
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}