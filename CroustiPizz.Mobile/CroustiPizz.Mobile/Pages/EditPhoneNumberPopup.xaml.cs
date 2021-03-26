using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPhoneNumberPopup : PopupPage
    {
        public EditPhoneNumberPopup()
        {
            InitializeComponent();
        }

        private async void OnConfirmClicked(object sender, EventArgs args)
        {
            if (NewPhoneNumber.Text != null)
            {
                MessagingCenter.Send(NewPhoneNumber.Text, "EditPhoneNumberPopup");
            }

            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}