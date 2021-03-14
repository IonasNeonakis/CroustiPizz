using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos.Accounts;
using CroustiPizz.Mobile.Extensions;
using CroustiPizz.Mobile.Pages;
using Storm.Mvvm;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Storm.Mvvm.Services;

namespace CroustiPizz.Mobile.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public ICommand EditPasswordCommand { get; }
        public ICommand EditMailCommand { get; }
        public ICommand EditPhoneNumberCommand { get; }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        private string _newMail;
        public string NewMail
        {
            get => _newMail;
            set => SetProperty(ref _newMail, value);
        }
        
        private string _newPhoneNumber;
        public string NewPhoneNumber
        {
            get => _newPhoneNumber;
            set => SetProperty(ref _newPhoneNumber, value);
        }

        private UserProfileResponse _user;
        public UserProfileResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public ProfileViewModel()
        {
            EditPasswordCommand = new Command(EditPasswordAction);
            EditMailCommand = new Command(EditMailAction);
            EditPhoneNumberCommand = new Command(EditPhoneNumberAction);
            
            // supprimer ça et decommenter le bas
            _user = new UserProfileResponse
            {
                Email = "samir.toularhmine@etu.univ-orleans.fr", FirstName = "Samir", LastName = "Toularhmine",
                PhoneNumber = "0769303486"
            };
            
            NewPassword = "************";
            NewMail = _user.Email;
            NewPhoneNumber = _user.PhoneNumber;
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            
            /*
            IUserApiService service = DependencyService.Get<IUserApiService>();
            Response<UserProfileResponse> response = await service.GetUser();

            if (response.IsSuccess)
            {
                User = new UserProfileResponse()
                {
                    Email = response.Data.Email, FirstName = response.Data.FirstName, LastName = response.Data.LastName,
                    PhoneNumber = response.Data.PhoneNumber
                };
                User = response.Data;
            }
            else
            {
                //@TODO gestion d'erreur
            }
            */
        }
        
        private async void EditPasswordAction()
        {
            PopupPage popup = new EditPasswordPopup();
            
            MessagingCenter.Subscribe<PasswordPayload>(this, "EditPasswordPopup", (value) =>
            {
                NewPassword = value.NewPassword;
                string oldPassword = value.CurrentPassword;
                MessagingCenter.Unsubscribe<PasswordPayload>(this, "EditPasswordPopup");
            });
            
            await PopupNavigation.Instance.PushAsync(popup);
        }
        
        private async void EditMailAction()
        {
            PopupPage popup = new EditMailPopup();
            
            MessagingCenter.Subscribe<String>(this, "EditMailPopup", (value) =>
            {
                NewMail = value;
                MessagingCenter.Unsubscribe<String>(this, "EditMailPopup");
            });
            
            await PopupNavigation.Instance.PushAsync(popup);
        }
        
        private async void EditPhoneNumberAction()
        {
            PopupPage popup = new EditPhoneNumberPopup();
            
            MessagingCenter.Subscribe<String>(this, "EditPhoneNumberPopup", (value) =>
            {
                NewPhoneNumber = value;
                MessagingCenter.Unsubscribe<String>(this, "EditPhoneNumberPopup");
            });
            
            await PopupNavigation.Instance.PushAsync(popup);
        }
    }
}