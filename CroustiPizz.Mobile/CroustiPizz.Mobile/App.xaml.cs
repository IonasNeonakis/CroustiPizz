using System.Diagnostics;
using CroustiPizz.Mobile.Pages;
using CroustiPizz.Mobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CroustiPizz.Mobile
{
    public partial class App
    {
        public App() : base(() => new AuthView(), RegisterServices)
        {
#if DEBUG
            Log.Listeners.Add(new DelegateLogListener((arg1, arg2) => Debug.WriteLine($"{arg1} : {arg2}")));
#endif
            
            Sharpnado.Tabs.Initializer.Initialize(false, false);
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            
            InitializeComponent();
        }

        private static void RegisterServices()
        {
            DependencyService.RegisterSingleton<IApiService>(new ApiService());
            
            DependencyService.RegisterSingleton<IPizzaApiService>(new PizzaApiService());
            
            DependencyService.RegisterSingleton<IUserApiService>(new UserApiService());

            DependencyService.RegisterSingleton(new CartService());

        }
    }
}