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
        Task<Response<UserProfileResponse>> GetUser();

        Task<Response<LoginResponse>> LoginUser(LoginWithCredentialsRequest utilisateur);

        Task<Response<LoginResponse>> RegisterUser(CreateUserRequest utilisateur);

        Task<Response> ChangePassword(SetPasswordRequest data);

        Task<Response<SetUserProfileRequest>> UpdateUser(SetUserProfileRequest data);
    }


    public class UserApiService : IUserApiService
    {
        private readonly IApiService _apiService;

        public UserApiService()
        {
            _apiService = DependencyService.Get<IApiService>();
        }

        public async Task<Response<UserProfileResponse>> GetUser()
        {
            return await _apiService.Get<Response<UserProfileResponse>>(Urls.USER_PROFILE);
        }

        public async Task<Response<LoginResponse>> LoginUser(LoginWithCredentialsRequest credentials)
        {
            Response<LoginResponse> task =
                await _apiService.Post<Response<LoginResponse>, LoginWithCredentialsRequest>(
                    Urls.LOGIN_WITH_CREDENTIALS, credentials);
            if (task.IsSuccess)
            {
                saveLoginData(task);
            }

            return task;
        }

        public async Task<Response<LoginResponse>> RegisterUser(CreateUserRequest credentials)
        {
            Response<LoginResponse> task =
                await _apiService.Post<Response<LoginResponse>, CreateUserRequest>(Urls.CREATE_USER, credentials);
            if (task.IsSuccess)
            {
                saveLoginData(task);
            }

            return task;
        }


        public async Task<Response> ChangePassword(SetPasswordRequest data)
        {
            return await _apiService.Patch<Response, SetPasswordRequest>(Urls.SET_PASSWORD, data);
        }


        public async Task<Response<SetUserProfileRequest>> UpdateUser(SetUserProfileRequest data)
        {
            return await _apiService.Patch<Response<SetUserProfileRequest>, SetUserProfileRequest>(
                Urls.SET_USER_PROFILE, data);
        }

        private async void saveLoginData(Response<LoginResponse> task)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int tempsActuel = (int) t.TotalSeconds;
            await SecureStorage.SetAsync(Constantes.ACCESS_TOKEN, task.Data.AccessToken);
            await SecureStorage.SetAsync(Constantes.REFRESH_TOKEN, task.Data.RefreshToken);
            await SecureStorage.SetAsync(Constantes.EXPIRES_AT, (tempsActuel + task.Data.ExpiresIn).ToString());
        }
    }
}