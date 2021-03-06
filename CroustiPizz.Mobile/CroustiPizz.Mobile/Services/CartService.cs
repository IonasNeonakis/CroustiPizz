using System.Collections.Generic;


namespace CroustiPizz.Mobile.Services
{
    /// <summary>
    /// Service pour la gestion du panier
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Récupèrer le panier
        /// </summary>
        Dictionary<long, Dictionary<long, int>> GetCart(); 

        /// <summary>
        /// Ajouter un certain nombre de pizzas à un shop
        /// </summary>
        void AddToCart(long shopId, long pizzaId, int quantity);
        /// <summary>
        /// Mettre à jour un certain nombre de pizzas à un shop
        /// </summary>
        void UpdateFromCart(long shopId, long pizzaId, int quantity);
        /// <summary>
        /// Supprimes toute une pizza d'un panier
        /// </summary>
        void RemoveAllFromCart(long shopId, long pizzaId);
        /// <summary>
        /// Vide le panier d'une pizerria
        /// </summary>
        void EmptyCart(long shopId);
        /// <summary>
        /// Retourne la liste du contenu du panier par exemple une liste de 3fois la même pizza [2,2,2,]
        /// </summary>
        List<long> GetListId(long shopId);
        /// <summary>
        /// Retourne le nombre d'éléments dans le shop
        /// </summary>
        int NumberOfItems(long shopId);
    }

    public class CartService : ICartService
    {
        private Dictionary<long, Dictionary<long, int>> _cart;


        public CartService()
        {
            _cart = new Dictionary<long, Dictionary<long, int>>();
        }


        // A appeler uniquement depuis la liste des pizzas !!!
        public Dictionary<long, Dictionary<long, int>> GetCart()
        {
            return _cart;
        }

        public void AddToCart(long shopId, long pizzaId, int quantity)
        {
            if (_cart.ContainsKey(shopId)) // le panier pour ce restaurant existe
            {
                if (_cart[shopId].ContainsKey(pizzaId)) // il y'a déjà cette pizza dans le panier
                {
                    _cart[shopId][pizzaId] += quantity;
                }
                else // aucune pizza avec cet id dans le panier
                {
                    if (quantity > 0)
                    {
                        _cart[shopId][pizzaId] = quantity;
                    }
                }
            }
            else // le panier pour ce restaurant n'existe pas
            {
                _cart[shopId] = new Dictionary<long, int>();
                _cart[shopId][pizzaId] = quantity;
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
                _cart[shopId][pizzaId] = quantity;
            }
        }

        // croix à côté d'une pizza dans le panier
        public void RemoveAllFromCart(long shopId, long pizzaId)
        {
            _cart[shopId].Remove(pizzaId);
        }

        // Vider tout le panier !!!
        public void EmptyCart(long shopId)
        {
            _cart.Remove(shopId);
        }

        public List<long> GetListId(long shopId)
        {
            List<long> list = new List<long>();

            foreach (KeyValuePair<long, int> keyValuePair in _cart[shopId])
            {
                for (int i = 0; i < keyValuePair.Value; i++)
                {
                    list.Add(keyValuePair.Key);
                }
            }

            return list;
        }

        public int NumberOfItems(long shopId)
        {
            int total = 0;
            if (_cart.ContainsKey(shopId))
            {
                foreach (KeyValuePair<long, int> keyValuePair in _cart[shopId])
                {
                    total += keyValuePair.Value;
                }
            }


            return total;
        }
    }
}