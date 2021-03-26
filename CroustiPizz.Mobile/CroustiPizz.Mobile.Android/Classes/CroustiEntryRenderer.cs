using Android.Content;
using CroustiPizz.Mobile.Android.Classes;
using CroustiPizz.Mobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CroustiEntry), typeof(CroustiEntryRenderer))]

namespace CroustiPizz.Mobile.Android.Classes
{
    public class CroustiEntryRenderer : EntryRenderer
    {
        public CroustiEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(Color.FromHex("#EBEFF5").ToAndroid());
                Control.SetElevation(0);
            }
        }
    }
}