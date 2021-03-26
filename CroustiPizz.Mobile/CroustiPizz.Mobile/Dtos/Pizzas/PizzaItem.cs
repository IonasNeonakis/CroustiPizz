using Newtonsoft.Json;
using Storm.Mvvm;

namespace CroustiPizz.Mobile.Dtos.Pizzas
{
    public class PizzaItem : NotifierBase
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("price")] public double Price { get; set; }

        [JsonProperty("out_of_stock")] public bool OutOfStock { get; set; }

        private int _quantite;

        public int Quantite
        {
            get => _quantite;
            set => SetProperty(ref _quantite, value);
        }

        public string Url { get; set; }
    }
}