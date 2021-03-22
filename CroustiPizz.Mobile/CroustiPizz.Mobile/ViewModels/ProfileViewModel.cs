using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Accounts;
using CroustiPizz.Mobile.Dtos.Authentications.Credentials;
using CroustiPizz.Mobile.Extensions;
using CroustiPizz.Mobile.Interfaces;
using CroustiPizz.Mobile.Pages;
using CroustiPizz.Mobile.Services;
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
        public ICommand SaveProfileInformationCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand CloseProfileCommand { get; }
        public ICommand EditNameCommand { get; }

        private string _currentPassword;

        public string CurrentPassword
        {
            get => _currentPassword;
            set => SetProperty(ref _currentPassword, value);
        }

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
        
        private string _newFirstName;

        public string NewFirstName
        {
            get => _newFirstName;
            set => SetProperty(ref _newFirstName, value);
        }
        
        private string _newLastName;

        public string NewLastName
        {
            get => _newLastName;
            set => SetProperty(ref _newLastName, value);
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
            SaveProfileInformationCommand = new Command(SaveProfileInformationAction);
            LogoutCommand = new Command(LogoutAction);
            CloseProfileCommand = new Command(CloseProfileAction);
            EditNameCommand = new Command(EditNameAction);
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            
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
                NewFirstName = User.FirstName;
                NewLastName = User.LastName;
                NewMail = User.Email;
                NewPassword = "***************";
                NewPhoneNumber = User.PhoneNumber;
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert( "Erreur dans les nouveaux champs entrés" );

            }
        }

        private async void EditPasswordAction()
        {
            PopupPage popup = new EditPasswordPopup();

            MessagingCenter.Subscribe<PasswordPayload>(this, "EditPasswordPopup", (value) =>
            {
                CurrentPassword = value.CurrentPassword;
                NewPassword = value.NewPassword;
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

        private async void SaveProfileInformationAction()
        {
            IUserApiService service = DependencyService.Get<IUserApiService>();
            
            if (CurrentPassword != null && NewPassword != null)
            {
                SetPasswordRequest passwordRequest = new SetPasswordRequest
                {
                    OldPassword = CurrentPassword,
                    NewPassword = NewPassword
                };
                Response reponsePassword = await service.ChangePassword(passwordRequest);
                
                if (!reponsePassword.IsSuccess)
                {
                    DependencyService.Get<IMessage>().LongAlert( "Probleme dans la sauvegarde de vos informations" );

                }
            }
            
            SetUserProfileRequest userProfileRequest =  new SetUserProfileRequest
            {
                Email = NewMail,
                PhoneNumber = NewPhoneNumber,
                FirstName = NewFirstName,
                LastName = NewLastName
            };
            
            
            Response<SetUserProfileRequest> reponseUser = await service.UpdateUser(userProfileRequest);

            if (!reponseUser.IsSuccess)
            {
                /* @TODO: Implémenter un message d'erreur */
                throw new NotImplementedException();
            }
        }

        private void LogoutAction()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            PopupNavigation.Instance.PopAllAsync();
            navigationService.PopAsync();
        }

        private void CloseProfileAction()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PopAsync();
        }
        
        private async void EditNameAction()
        {
            PopupPage popup = new EditNamePopup();

            MessagingCenter.Subscribe<IdentityPayload>(this, "EditNamePopup", (value) =>
            {
                NewFirstName = value.CurrentFirstName;
                NewLastName = value.CurrentLastName;
                MessagingCenter.Unsubscribe<IdentityPayload>(this, "EditNamePopup");
            });

            await PopupNavigation.Instance.PushAsync(popup);
        }
    }
}