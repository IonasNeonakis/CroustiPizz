using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMailPopup : PopupPage
    {
        public EditMailPopup()
        {
            InitializeComponent();
        }

        private async void OnConfirmClicked(object sender, EventArgs args)
        {
            MessagingCenter.Send(NewMailAddress.Text, "EditMailPopup");
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}