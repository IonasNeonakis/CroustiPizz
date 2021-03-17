using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CroustiPizz.Mobile.Services
{
    public class CartService
    {
        private Dictionary<long, Dictionary<long, int>> _cart;
        public Dictionary<long, Dictionary<long, int>> Cart
        {
            get => _cart;
            set => _cart = value;
        }

        public CartService()
        {
            Cart = new Dictionary<long, Dictionary<long, int>>();
        }

        // A appeler uniquement depuis la liste des pizzas !!!
        public void AddToCart(long shopId, long pizzaId, int quantity)
        {
            if (Cart.ContainsKey(shopId)) // le panier pour ce restaurant existe
            {
                if (Cart[shopId].ContainsKey(pizzaId)) // il y'a déjà cette pizza dans le panier
                {
                    Cart[shopId][pizzaId] += quantity;
                }
                else // aucune pizza avec cet id dans le panier
                {
                    Cart[shopId][pizzaId] = quantity;
                }
            }
            else // le panier pour ce restaurant n'existe pas
            {
                Cart[shopId] = new Dictionary<long, int>();
                Cart[shopId][pizzaId] = quantity;
            }
        }

        // A appeler uniquement depuis la vue Panier !!!
        public void UpdateFromCart(long shopId, long pizzaId, int quantity)
        {
            if (quantity == 0)
            {
                RemoveAllFromCart(shopId, pizzaId);
            }
            else
            {
                Cart[shopId][pizzaId] = quantity;
            }
        }

        // croix à côté d'une pizza dans le panier
        public void RemoveAllFromCart(long shopId, long pizzaId)
        {
            Cart[shopId].Remove(pizzaId);
        }

        // Vider tout le panier !!!
        public void EmptyCart(long shopId)
        {
            Cart.Remove(shopId);
        }
        
        
    }
}