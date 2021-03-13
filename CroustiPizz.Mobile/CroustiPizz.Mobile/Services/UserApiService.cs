using System.Threading.Tasks;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Accounts;
using CroustiPizz.Mobile.Dtos.Authentications;
using CroustiPizz.Mobile.Dtos.Authentications.Credentials;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.Services
{
    public interface IUserApiService
    {
        
        Task<Response<UserProfileResponse>> ViewUser();

        Task<Response<LoginResponse>> LoginUser(LoginWithCredentialsRequest loginWithCredentialsRequest);
    }

    
    public class UserApiService : IUserApiService
    {
        private readonly IApiService _apiService;

        public UserApiService()
        {
            _apiService = DependencyService.Get<IApiService>();
        }

        public async Task<Response<UserProfileResponse>> ViewUser()
        {
            return await _apiService.Get<Response<UserProfileResponse>>(Urls.USER_PROFILE);
        }

        public async Task<Response<LoginResponse>> LoginUser(LoginWithCredentialsRequest credentials)
        {
            return await _apiService.Post<Response<LoginResponse>, LoginWithCredentialsRequest>(Urls.LOGIN_WITH_CREDENTIALS, credentials);
        }
    }
}