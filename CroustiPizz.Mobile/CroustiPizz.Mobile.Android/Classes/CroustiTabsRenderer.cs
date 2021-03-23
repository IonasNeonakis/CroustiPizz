using Android.Content;
using Android.Views;
using CroustiPizz.Mobile.Android.Classes;
using CroustiPizz.Mobile.Controls;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Tabs;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(CroustiTabs), typeof(CroustiTabsRenderer))]

namespace CroustiPizz.Mobile.Android.Classes
{
    public class CroustiTabsRenderer : TabbedPageRenderer
    {
        
        public CroustiTabsRenderer(Context context) : base(context)
        {
        }

        protected override void OnVisibilityChanged(View changedView, ViewStates visibility)
        {
            base.OnVisibilityChanged(changedView, visibility);

            if (visibility == ViewStates.Visible)
            {
                var tabs = this.GetChildAt(0);
                if (tabs != null)
                {
                    tabs.SetBackgroundColor(Color.FromHex("#EBEFF5").ToAndroid());
                    tabs.SetElevation(0);
                }
            }
        }
    }
}