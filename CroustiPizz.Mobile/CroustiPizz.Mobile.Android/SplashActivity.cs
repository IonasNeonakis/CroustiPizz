using Android.App;
using Android.Content;
using Android.Views;
using Google.Android.Material.Tabs.AppCompat.App;

namespace CroustiPizz.Mobile.Android
{
    [Activity(Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof (MainActivity));
        }
    }
}