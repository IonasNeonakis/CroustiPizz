using Android.App;
using Android.Widget;
using CroustiPizz.Mobile.Android.Classes;
using CroustiPizz.Mobile.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace CroustiPizz.Mobile.Android.Classes
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long)?.Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short)?.Show();
        }
    }
}