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
using Xamarin.Essentials;

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

        /// <summary>
        /// Depuis le onResume, on récupère les informations de l'utilisateur grâce à une requête vers l'API.
        /// Si on obtient les informations de l'utilisateur, on les affiche telles quelles dans le profil, à l'exception
        /// du mot de passe pour lequel on affiche des étoiles à la place.
        /// Si l'on arrive pas à obtenir les informations de l'utilisateur, on affiche un toast nous indiquant qu'il y a eut une erreur.
        /// </summary>
        /// <returns></returns>
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
                DependencyService.Get<IMessage>().LongAlert(Resources.AppResources.AlertNewlyFilledEntriesError);
            }
        }
        /// <summary>
        /// L'édition des informations se fait par le biais de popup contenant les champs nécessaires au changement voulu.
        /// On affiche la popup tout en s'abonnant à un canal ou on pourra recevoir un message contenant l'information modifiée
        /// et ainsi modifier cette information dans le view model avant de se désabonner du canal.
        /// Ce système est répété pour chaque popup de modification.
        /// </summary>
        private async void EditPasswordAction()
        {
            if (PopupNavigation.Instance.PopupStack.Count == 0)
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
        }

        private async void EditMailAction()
        {
            if (PopupNavigation.Instance.PopupStack.Count == 0)
            {
                PopupPage popup = new EditMailPopup();

                MessagingCenter.Subscribe<String>(this, "EditMailPopup", (value) =>
                {
                    NewMail = value;
                    MessagingCenter.Unsubscribe<String>(this, "EditMailPopup");
                });

                await PopupNavigation.Instance.PushAsync(popup);
            }
        }

        private async void EditPhoneNumberAction()
        {
            if (PopupNavigation.Instance.PopupStack.Count == 0)
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
        /// <summary>
        /// Cette méthode permet de sauvegarder les informations du profil actuellement modifiées.
        /// Deux requêtes séparées sont réalisées : 
        /// - une pour changer le mot de passe
        /// - une pour changer le reste des informations disponibles
        /// Si la requête s'est mal déroulée, on affiche un toast avec le message correspondant. 
        /// </summary>
        private async void SaveProfileInformationAction()
        {
            IUserApiService service = DependencyService.Get<IUserApiService>();
            Response reponsePassword = null;
            if (CurrentPassword != null && NewPassword != null)
            {
                SetPasswordRequest passwordRequest = new SetPasswordRequest
                {
                    OldPassword = CurrentPassword,
                    NewPassword = NewPassword
                };
                reponsePassword = await service.ChangePassword(passwordRequest);
            }

            SetUserProfileRequest userProfileRequest = new SetUserProfileRequest
            {
                Email = NewMail,
                PhoneNumber = NewPhoneNumber,
                FirstName = NewFirstName,
                LastName = NewLastName
            };


            Response<SetUserProfileRequest> reponseUser = await service.UpdateUser(userProfileRequest);


            if (reponseUser.IsSuccess && reponsePassword != null && reponsePassword.IsSuccess)
            {
                DependencyService.Get<IMessage>()
                    .LongAlert(Resources.AppResources.AlertProfileAndPasswordUpdateSuccess);
            }
            else if (reponseUser.IsSuccess && reponsePassword == null)
            {
                DependencyService.Get<IMessage>().LongAlert(Resources.AppResources.AlertProfileUpdateSuccess);
            }
            else if (reponseUser.IsSuccess && reponsePassword != null && !reponsePassword.IsSuccess)
            {
                DependencyService.Get<IMessage>()
                    .LongAlert(Resources.AppResources.AlertProfileUpdateSuccessButPasswordError +
                               reponsePassword.ErrorMessage);
            }
            else if (!reponseUser.IsSuccess && reponsePassword != null && reponsePassword.IsSuccess)
            {
                DependencyService.Get<IMessage>().LongAlert(Resources.AppResources.AlertPasswordUpdatedButProfileError +
                                                            reponseUser.ErrorMessage);
            }
            else if (!reponseUser.IsSuccess && reponsePassword != null && !reponsePassword.IsSuccess)
            {
                DependencyService.Get<IMessage>().LongAlert(Resources.AppResources.AlertPasswordAndProfileErrorPart1 +
                                                            reponsePassword.ErrorMessage +
                                                            Resources.AppResources.AlertPasswordAndProfileErrorPart2 +
                                                            reponseUser.ErrorMessage);
            }
        }

        private void LogoutAction()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            PopupNavigation.Instance.PopAllAsync();
            navigationService.PopAsync();
        }

        /// <summary>
        /// Lors de la déconnexion, on supprime toutes informations dans le local storage et on ferme l'application.
        /// </summary>
        private void CloseProfileAction()
        {
            SecureStorage.RemoveAll();
            Environment.Exit(0);
            /*          
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PopAsync();
            */
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

            if (PopupNavigation.Instance.PopupStack.Count == 0)
            {
                await PopupNavigation.Instance.PushAsync(popup);
            }
        }
    }
}