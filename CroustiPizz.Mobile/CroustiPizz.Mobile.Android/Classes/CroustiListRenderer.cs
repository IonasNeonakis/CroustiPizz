using CroustiPizz.Mobile.Android.Classes;
using CroustiPizz.Mobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CroustiList), typeof(CroustiListRenderer))]

namespace CroustiPizz.Mobile.Android.Classes
{
    public class CroustiListRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            Control.SetSelector(Android.Resource.Layout.no_selector);
        }
    }
}