using Microsoft.EntityFrameworkCore;
using MovieShopTrio.Controllers;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services.Interfaces;
using Newtonsoft.Json;

namespace MovieShopTrio.Services
{
    public class OrderService: IOrderService
    {
 
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MovieDbContext _dbContext;

    
        public OrderService(MovieDbContext moviedbContext, IHttpContextAccessor iHttpContextAccessor)
        {

            _dbContext = moviedbContext;
            _httpContextAccessor = iHttpContextAccessor;
        }
        public List<CartItemViewModel> GetCart()
        {
            // Retrieve the cart from session as a string
            var cartString = _httpContextAccessor.HttpContext.Session.GetString("Cart");

            // If no cart exists, create a new list and return it
            if (string.IsNullOrEmpty(cartString))
            {
                var newCart = new List<CartItemViewModel>(); // Create an empty cart
                _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(newCart)); // Save the empty cart to session
                return newCart; // Return the empty cart
            }

            // Deserialize the cart string back to a list of CartItemViewModel
            return JsonConvert.DeserializeObject<List<CartItemViewModel>>(cartString);
        }


        public void AddToCart(int movieId)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == movieId);
            if (movie == null)
            {
                return; // Movie not found, do nothing
            }

            var cart = GetCart();
            var existingCartItem = cart.FirstOrDefault(c => c.Movie.Id == movieId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItemViewModel { Movie = movie, Quantity = 1 });
            }

            // Save the updated cart back to the session
            var serializedCart = JsonConvert.SerializeObject(cart);
            _httpContextAccessor.HttpContext.Session.SetString("Cart", serializedCart);

            // Log the session to check if it's being saved correctly
            Console.WriteLine("Cart Saved: " + serializedCart);  // Log the cart data
        }

        // Remove a movie from the cart
        public void RemoveFromCart(int movieId)
        {
            var cart = GetCart(); // Get the current cart from the session

            // Find the cart item with the given movieId
            var cartItemToRemove = cart.FirstOrDefault(c => c.Movie.Id == movieId);

            if (cartItemToRemove != null)
            {
                // Remove the movie from the cart
                cart.Remove(cartItemToRemove);
            }

            // Save the updated cart back to the session
            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
    }
}