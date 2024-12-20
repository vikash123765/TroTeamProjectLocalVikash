using Microsoft.EntityFrameworkCore;
using MovieShopTrio.Controllers;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services.Interfaces;
using Newtonsoft.Json;


namespace MovieShopTrio.Services
{
    public class OrderService : IOrderService
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
            Console.WriteLine($"AddToCart called with movieId: {movieId}");

            var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == movieId);
            if (movie == null)
            {
                Console.WriteLine($"Movie with ID {movieId} not found.");
                return;
            }

            Console.WriteLine($"Movie found: {movie.Title}");

            var cart = GetCart();
            Console.WriteLine($"Current cart count: {cart.Count}");

            var existingCartItem = cart.FirstOrDefault(c => c.Movie.Id == movieId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
                Console.WriteLine($"Updated quantity for movie: {movie.Title}");
            }
            else
            {
                cart.Add(new CartItemViewModel
                {
                    Movie = new MovieViewModel
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Price = movie.Price
                    },
                    Quantity = 1
                });
                Console.WriteLine($"Added new movie to cart: {movie.Title}");
            }

            var serializedCart = JsonConvert.SerializeObject(cart);
            _httpContextAccessor.HttpContext.Session.SetString("Cart", serializedCart);
            Console.WriteLine("Cart updated successfully.");
        }

        // Remove a movie from the cart
        public void RemoveFromCart(int movieId)
        {
            Console.WriteLine($"RemoveFromCart called with movieId: {movieId}");

            var cart = GetCart();
            var cartItemToRemove = cart.FirstOrDefault(c => c.Movie.Id == movieId);

            if (cartItemToRemove != null)
            {
                cart.Remove(cartItemToRemove);
                Console.WriteLine($"Movie removed: {cartItemToRemove.Movie.Title}");
            }

            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            Console.WriteLine("Cart updated after removal.");
        }

        public bool Checkout(string email)
        {
            // Check if the customer exists
            var customer = _dbContext.Customers.FirstOrDefault(c => c.EmailAddress == email);

            if (customer != null)
            {
                // Customer exists, place the order immediately
                PlaceOrder(customer.Id);
                return true; // Indicates existing customer and successful order placement
            }

            return false; // Indicates customer doesn't exist
        }

        public bool PlaceOrder(int customerId)
        {
            // Retrieve cart items from session
            var cartString = _httpContextAccessor.HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cartString))
            {
                throw new InvalidOperationException("Cart is empty.");
            }

            var cartItems = JsonConvert.DeserializeObject<List<CartItemViewModel>>(cartString);

            // Create the order and populate OrderRows
            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = customerId,
                OrderRows = cartItems.Select(item => new OrderRow
                {
                    MovieId = item.Movie.Id,
                    Price = item.Movie.Price,
                }).ToList()
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            // Clear the cart session
            _httpContextAccessor.HttpContext.Session.Remove("Cart");

            return true; // Indicates successful order placement
        }
        public int RegisterCustomer(Customer customer)
        {
            // Save the customer in the database
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return customer.Id; // Return the new customer ID
        }
    }

}