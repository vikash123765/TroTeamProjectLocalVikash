using MovieShopTrio.Database;
using MovieShopTrio.Models;

namespace MovieShopTrio.Services.Interfaces
{
    public interface IOrderService
    {
        //void PlaceOrder(int customerId, List<int> productIds);
        //List<Order> GetAllOrders();
        List<CartItemViewModel> GetCart();

        void AddToCart(int movieId); // Add a movie to the cart
        void RemoveFromCart(int movieId); // Remove a movie from the cart
    }
}

