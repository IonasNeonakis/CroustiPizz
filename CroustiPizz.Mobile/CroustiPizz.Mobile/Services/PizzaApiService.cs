using System.Collections.Generic;
using System.Threading.Tasks;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Pizzas;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.Services
{
    public interface IPizzaApiService
    {
        Task<Response<List<ShopItem>>> ListShops();

        Task<Response<List<PizzaItem>>> ListPizzas(long shopId);

        Task<Response<List<OrderItem>>> ListOrders();

        Task<Response<OrderItem>> OrderPizzas(long shopId, CreateOrderRequest body);
    }

    public class PizzaApiService : IPizzaApiService
    {
        private readonly IApiService _apiService;

        public PizzaApiService()
        {
            _apiService = DependencyService.Get<IApiService>();
        }

        public async Task<Response<List<ShopItem>>> ListShops()
        {
            return await _apiService.Get<Response<List<ShopItem>>>(Urls.LIST_SHOPS);
        }

        public async Task<Response<List<PizzaItem>>> ListPizzas(long shopId)
        {
            return await _apiService.Get<Response<List<PizzaItem>>>(Urls.LIST_PIZZA.Replace("{shopId}",
                shopId.ToString()));
        }

        public async Task<Response<List<OrderItem>>> ListOrders()
        {
            return await _apiService.Get<Response<List<OrderItem>>>(Urls.LIST_ORDERS);
        }

        public async Task<Response<OrderItem>> OrderPizzas(long shopId, CreateOrderRequest body)
        {
            return await _apiService.Post<Response<OrderItem>, CreateOrderRequest>(
                Urls.DO_ORDER.Replace("{shopId}", shopId.ToString()), body);
        }
    }
}