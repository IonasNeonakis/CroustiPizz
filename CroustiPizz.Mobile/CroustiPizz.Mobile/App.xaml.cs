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
        public App() : base(() => new ProfilePage(), RegisterServices)
        {
#if DEBUG
            Log.Listeners.Add(new DelegateLogListener((arg1, arg2) => Debug.WriteLine($"{arg1} : {arg2}")));
#endif
            InitializeComponent();
        }

        private static void RegisterServices()
        {
            DependencyService.RegisterSingleton<IApiService>(new ApiService());
            
            DependencyService.RegisterSingleton<IPizzaApiService>(new PizzaApiService());
            
            DependencyService.RegisterSingleton<IUserApiService>(new UserApiService());

        }
    }
}