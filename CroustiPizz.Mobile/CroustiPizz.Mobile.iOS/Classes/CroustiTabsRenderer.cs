using CoreGraphics;
using CroustiPizz.Mobile.Controls;
using CroustiPizz.Mobile.iOS.Classes;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CroustiTabs), typeof(CroustiTabsRenderer))]

namespace CroustiPizz.Mobile.iOS.Classes
{
    public class CroustiTabsRenderer : TabbedRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            foreach (var item in TabBar.Items)
            {
                item.Image = ScalingImageToSize(item.Image, new CGSize(30, 30)); // set the size here as you want 
            }
        }



        public UIImage ScalingImageToSize(UIImage sourceImage, CGSize newSize)
        {

            if (UIScreen.MainScreen.Scale == 2.0) //@2x iPhone 6 7 8 
            {
                UIGraphics.BeginImageContextWithOptions(newSize, false, 2.0f);
            }


            else if (UIScreen.MainScreen.Scale == 3.0) //@3x iPhone 6p 7p 8p...
            {
                UIGraphics.BeginImageContextWithOptions(newSize, false, 3.0f);
            }

            else
            {
                UIGraphics.BeginImageContext(newSize);
            }

            sourceImage.Draw(new CGRect(0, 0, newSize.Width, newSize.Height));

            UIImage newImage = UIGraphics.GetImageFromCurrentImageContext();

            UIGraphics.EndImageContext();

            return newImage;

        }

    }
}