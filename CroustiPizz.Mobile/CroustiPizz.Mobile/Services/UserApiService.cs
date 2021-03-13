using System;
using System.Threading.Tasks;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Accounts;
using CroustiPizz.Mobile.Dtos.Authentications;
using CroustiPizz.Mobile.Dtos.Authentications.Credentials;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.Services
{

    public interface IUserApiService
    {
        
        Task<Response<UserProfileResponse>> ViewUser();

        Task<Response<LoginResponse>> LoginUser(LoginWithCredentialsRequest utilisateur);
        
        Task<Response<LoginResponse>> RegisterUser(CreateUserRequest utilisateur);
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
            Response<LoginResponse> task = await _apiService.Post<Response<LoginResponse>, LoginWithCredentialsRequest>(Urls.LOGIN_WITH_CREDENTIALS, credentials);
            if (task.IsSuccess)
            {
                await SecureStorage.SetAsync(Constantes.ACCESS_TOKEN, task.Data.AccessToken);
                await SecureStorage.SetAsync(Constantes.REFRESH_TOKEN, task.Data.RefreshToken);
                await SecureStorage.SetAsync(Constantes.EXPIRES_IN, task.Data.ExpiresIn.ToString());
            }
            return task;
        }

        public async Task<Response<LoginResponse>> RegisterUser(CreateUserRequest credentials)
        {
            Response<LoginResponse> task = await _apiService.Post<Response<LoginResponse>, CreateUserRequest>(Urls.CREATE_USER, credentials);
            if (task.IsSuccess)
            {
                await SecureStorage.SetAsync(Constantes.ACCESS_TOKEN, task.Data.AccessToken);
                await SecureStorage.SetAsync(Constantes.REFRESH_TOKEN, task.Data.RefreshToken);
                await SecureStorage.SetAsync(Constantes.EXPIRES_IN, task.Data.ExpiresIn.ToString());
            }
            return task;
        }
    }
}