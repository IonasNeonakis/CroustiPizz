using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.Services
{
    public interface IApiService
    {
        Task<TResponse> Get<TResponse>(string url);
        
        Task<TResponse> Post<TResponse, TRequest>(string url, TRequest body);

    }
    
    public class ApiService : IApiService
    {
	    private string ACCESSTOKEN = "tokenAremplace";
	    private string REFRESHSTOKEN = "tokenAremplace";
	    private const string HOST = "https://pizza.julienmialon.ovh/";
        private readonly HttpClient _client = new HttpClient();
        
        public async Task<TResponse> Get<TResponse>(string url)
        {
	        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, HOST + url);

	        HttpResponseMessage response = await _client.SendAsync(request);

	        string content = await response.Content.ReadAsStringAsync();

	        return JsonConvert.DeserializeObject<TResponse>(content);
        }

        //@TODO verifeir que cette fonction fonctionne
        public async Task<TResponse> Post<TResponse, TRequest>(string url, TRequest body)
        {
	        
	        StringContent contentBody = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
	        HttpRequestMessage request = new HttpRequestMessage
	        {
		        Method = HttpMethod.Post,
		        RequestUri = new Uri(HOST + url),
		        Content = contentBody
	        };
	        request.Headers.Add("Authorization", "Bearer "+ ACCESSTOKEN);

	        HttpResponseMessage response = await _client.SendAsync(request);

	        string contentResponse = await response.Content.ReadAsStringAsync();

	        return JsonConvert.DeserializeObject<TResponse>(contentResponse);
        }
    }
}