
using System;
using System.Threading.Tasks;
using CroustiPizz.Mobile.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();

            if (BindingContext is ProfileViewModel profileViewModel)
            {
                await profileViewModel.OnResume();
            }
        }
    }
}