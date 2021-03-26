using CroustiPizz.Mobile.Interfaces;
using CroustiPizz.Mobile.iOS.Classes;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(MessageIOS))]

namespace CroustiPizz.Mobile.iOS.Classes
{
    public class MessageIOS : IMessage
    {
        const double LONG_DELAY = 3.5;

        NSTimer alertDelay;
        UIAlertController alert;

        public void LongAlert(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }

        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) => { DismissMessage(); });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            if (UIApplication.SharedApplication.KeyWindow.RootViewController != null)
                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void DismissMessage()
        {
            alert?.DismissViewController(true, null);

            alertDelay?.Dispose();
        }
    }
}