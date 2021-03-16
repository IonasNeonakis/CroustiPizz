using CroustiPizz.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Gilroy-Regular.ttf", Alias = "GilroyRegular")]
[assembly: ExportFont("Gilroy-Bold.ttf", Alias = "GilroyBold")]
[assembly: ExportFont("Gilroy-Heavy.ttf", Alias = "GilroyHeavy")]
[assembly: ExportFont("Gilroy-Light.ttf", Alias = "GilroyLight")]
[assembly: ExportFont("Gilroy-Medium.ttf", Alias = "GilroyMedium")]

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthView : ContentPage
    {
        public AuthView()
        {
            InitializeComponent();
            BindingContext = new AuthViewModel();
        }
    }
}