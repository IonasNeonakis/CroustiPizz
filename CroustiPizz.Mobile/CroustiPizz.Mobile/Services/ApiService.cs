using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CroustiPizz.Mobile.Dtos.Authentications;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.Services
{
    public interface IApiService
    {
        Task<TResponse> Get<TResponse>(string url);

        Task<TResponse> Post<TResponse, TRequest>(string url, TRequest body);

        Task<TResponse> Patch<TResponse, TRequest>(string url, TRequest body);
    }

    public class ApiService : IApiService
    {
        private const string HOST = "https://pizza.julienmialon.ovh/";
        private readonly HttpClient _client = new HttpClient();

        public async Task<TResponse> Get<TResponse>(string url)
        {
            RefreshifNeeded();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, HOST + url);
            string accessToken = await SecureStorage.GetAsync(Constantes.ACCESS_TOKEN);

            request.Headers.Add("Authorization", "Bearer " + accessToken);
            try
            {
                HttpResponseMessage response = await _client.SendAsync(request);

                string content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(content);
            }catch (Exception)
            {
                HttpResponseMessage erreur = new HttpResponseMessage(HttpStatusCode.RequestedRangeNotSatisfiable);
                string contentResponse = await erreur.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(contentResponse);
            }
        }

        public async Task<TResponse> Post<TResponse, TRequest>(string url, TRequest body)
        {
            if (url != Urls.LOGIN_WITH_CREDENTIALS && url != Urls.CREATE_USER)
            {
                RefreshifNeeded();
            }


            StringContent contentBody =
                new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(HOST + url),
                Content = contentBody
            };
            string accessToken = await SecureStorage.GetAsync(Constantes.ACCESS_TOKEN);

            request.Headers.Add("Authorization", "Bearer " + accessToken);

            try
            {
                HttpResponseMessage response = await _client.SendAsync(request);

                string contentResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(contentResponse);
            }catch (Exception)
            {
                HttpResponseMessage erreur = new HttpResponseMessage(HttpStatusCode.RequestedRangeNotSatisfiable);
                string contentResponse = await erreur.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(contentResponse);
            }
        }

        public async Task<TResponse> Patch<TResponse, TRequest>(string url, TRequest body)
        {
            RefreshifNeeded();
            StringContent contentBody =
                new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage

            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(HOST + url),
                Content = contentBody
            };
            string accessToken = await SecureStorage.GetAsync(Constantes.ACCESS_TOKEN);

            request.Headers.Add("Authorization", "Bearer " + accessToken);
            try
            {
                HttpResponseMessage response = await _client.SendAsync(request);
                string contentResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(contentResponse);
            }
            catch (Exception)
            {
                HttpResponseMessage erreur = new HttpResponseMessage(HttpStatusCode.RequestedRangeNotSatisfiable);
                string contentResponse = await erreur.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(contentResponse);
            }
        }

        private async void RefreshifNeeded()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int tempsActuel = (int) t.TotalSeconds;
            int dateexpiration = Convert.ToInt32(await SecureStorage.GetAsync(Constantes.EXPIRES_AT));
            if (tempsActuel >= dateexpiration)
            {
                string refreshToken = await SecureStorage.GetAsync(Constantes.REFRESH_TOKEN);
                RefreshRequest body = new RefreshRequest
                {
                    ClientId = Constantes.CLIENT_ID,
                    ClientSecret = Constantes.CLIENT_ID,
                    RefreshToken = refreshToken
                };

                string url = Urls.REFRESH_TOKEN;

                StringContent contentBody =
                    new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(HOST + url),
                    Content = contentBody
                };

                try
                {
                    HttpResponseMessage response = await _client.SendAsync(request);


                    string contentResponse = await response.Content.ReadAsStringAsync();

                    LoginResponse responseFinale = JsonConvert.DeserializeObject<LoginResponse>(contentResponse);

                    await SecureStorage.SetAsync(Constantes.ACCESS_TOKEN, responseFinale.AccessToken);
                    await SecureStorage.SetAsync(Constantes.REFRESH_TOKEN, responseFinale.RefreshToken);
                    await SecureStorage.SetAsync(Constantes.EXPIRES_AT,
                        (tempsActuel + responseFinale.ExpiresIn).ToString());
                }catch (Exception)
                {
                    
                }
            }
        }
    }
}