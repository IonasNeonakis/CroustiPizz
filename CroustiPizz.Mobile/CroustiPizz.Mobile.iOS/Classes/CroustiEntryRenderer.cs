using CroustiPizz.Mobile.Controls;
using CroustiPizz.Mobile.iOS.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof(CroustiEntry), typeof(CroustiEntryRenderer))]
namespace CroustiPizz.Mobile.iOS.Classes
{
    public class CroustiEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged (e);

            if (Control != null)
            {
                Control.BackgroundColor = Color.FromHex("#EBEFF5").ToUIColor();
                Control.Layer.ShadowOpacity = 0;
            }
        }
    }
}