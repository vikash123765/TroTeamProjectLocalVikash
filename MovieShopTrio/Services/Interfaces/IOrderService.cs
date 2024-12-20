using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Models;

namespace MovieShopTrio.Services.Interfaces
{
    public interface IOrderService
    {
        List<CartItemViewModel> GetCart();

        void AddToCart(int movieId); // Add a movie to the cart
        void RemoveFromCart(int movieId); // Remove a movie from the cart

        bool Checkout(string email);

        bool PlaceOrder(int customerId);

        public int RegisterCustomer(Customer customer);
    }
}
