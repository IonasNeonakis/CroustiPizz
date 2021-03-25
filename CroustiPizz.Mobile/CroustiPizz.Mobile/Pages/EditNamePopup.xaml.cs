using System;
using CroustiPizz.Mobile.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNamePopup : PopupPage
    {
        public EditNamePopup()
        {
            InitializeComponent();
        }

        private async void OnConfirmClicked(object sender, EventArgs args)
        {
            if (NewFirstName.Text != null &&  NewLastName.Text != null)
            {
                MessagingCenter.Send(new IdentityPayload(NewFirstName.Text, NewLastName.Text), "EditNamePopup");
            }
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}