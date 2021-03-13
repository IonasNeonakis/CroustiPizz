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
            
            // Supprimer ça et décommenter le bas
            User = new UserProfileResponse
            {
                Email = "samir.toularhmine@etu.univ-orleans.fr", FirstName = "Samir", LastName = "Toularhmine nom", PhoneNumber = "0769303486"
            };
            
            /*
            IUserApiService service = DependencyService.Get<IUserApiService>();
            Response<UserProfileResponse> response = await service.ViewUser();
            Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
            
            if (response.IsSuccess)
            {
                Console.WriteLine($"Appel HTTP : {response.Data.FirstName}");
                User = new UserProfileResponse()
                {
                    Email = response.Data.Email, FirstName = response.Data.FirstName, LastName = response.Data.LastName,
                    PhoneNumber = response.Data.PhoneNumber
                };
                User = response.Data;
            }
            else
            {
                Console.Write("    NE MARCHE PAS    ");
            }
            */
        }
    }
    
}