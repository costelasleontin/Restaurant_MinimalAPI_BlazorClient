using RestaurantsClient.Models;

namespace RestaurantsClient.Services
{
    public class CartManager
    {
        private Dictionary<int, List<MenuItem>> Cart { get; set; } = new();

        public List<MenuItem> GetMenuItemsForRestaurantById(int id) => Cart.GetValueOrDefault(id)!;
        public void AddMenuItemForRestaurantById(int id, MenuItem item)
        {
            List<MenuItem> items;
            if (Cart.GetValueOrDefault(id) != null)
            {
                items = Cart[id];
            }
            else
            {
                items = new List<MenuItem>();
            }
            if (items.Any(i=>i.Id==item.Id))
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Id == item.Id)
                    {
                        items[i].Quantity++;
                    }
                }
            }
            else
            {
                item.Quantity = 1;
                items.Add(item);
            }

            Cart[id] = items;
        }

        public decimal TotalCartValue(int id)
        {
            List<MenuItem> items;
            if (Cart.GetValueOrDefault(id) != null)
            {
                items = Cart[id];
                decimal sum = 0;
                foreach (var item in items)
                {
                    sum += (item.Quantity * item.Price);
                }
                return sum;
            }
            else return 0;

        }

        public void DeleteMenuItemForRestaurantById(int id, MenuItem item)
        {
            List<MenuItem> items;
            if (Cart.GetValueOrDefault(id) != null)
            {
                items = Cart[id];
                items.Remove(item);
                Cart[id] = items;
            }
        }

    }
}