using System;
using System.Threading.Tasks;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Accounts;
using CroustiPizz.Mobile.Services;
using Newtonsoft.Json.Linq;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class ProfileViewModel: ViewModelBase
    {

        private UserProfileResponse _user;

        public UserProfileResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


        public override async Task OnResume()
        {
            
            await base.OnResume();

            /*
            // supprimer ça et decommenter le bas
            User = new UserProfileResponse
            {
                Email = "Email", FirstName = "Premier nom", LastName = "Dernier nom", PhoneNumber = "0602223277"
            };*/



            IUserApiService service = DependencyService.Get<IUserApiService>();

            Response<UserProfileResponse> response = await service.ViewUser();
            
            
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
        }
    }
    
}