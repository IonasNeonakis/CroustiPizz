using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CroustiPizz.Mobile.Extensions;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPasswordPopup : PopupPage
    {
        public EditPasswordPopup()
        {
            InitializeComponent();
        }

        private async void OnConfirmClicked(object sender, EventArgs args)
        {
            MessagingCenter.Send<PasswordPayload>(new PasswordPayload(AncienMotDePasse.Text, NouveauMotDePasse.Text), "EditPasswordPopup");
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}