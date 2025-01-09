using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Models;

namespace MovieShopTrio.Services.Interfaces
{
    public interface IOrderService
    {
        List<CartItemViewModel> GetCart();

        void AddToCart(int movieId); // Add a movie to the cart
        public void RemoveOneFromCart(int movieId);
        public IEnumerable<dynamic> GetFilteredOrders(string customerName, DateTime? startDate, DateTime? endDate);

        bool Checkout(string email);

        public void IncreaseQuantity(int movieId);

        bool PlaceOrder(int customerId);

        public int RegisterCustomer(Customer customer);
    }
}
