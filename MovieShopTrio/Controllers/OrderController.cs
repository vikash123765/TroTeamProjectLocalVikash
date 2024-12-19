using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Controllers
{
    public class OrderController : Controller
    {
        public readonly MovieDbContext _db;
        public readonly IOrderService _orderService;

        public OrderController(MovieDbContext db, IOrderService orderService)
        {
            _db = db;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // View the cart and display all cart items with their quantities
        public IActionResult ViewCart()
        {
            var cartItems = _orderService.GetCart();  // Get all cart items (movie + quantity)
            return View(cartItems);  // Pass the cart items to the view
        }

        // Add a movie to the cart
        public IActionResult AddToCart(int movieId)
        {
            _orderService.AddToCart(movieId);  // Add the movie to the cart using the service
            return RedirectToAction("ViewCart");  // Redirect to the view cart page
        }

        // Remove a movie from the cart
        public IActionResult RemoveFromCart(int movieId)
        {
            _orderService.RemoveFromCart(movieId);  // Remove the movie from the cart using the service
            return RedirectToAction("ViewCart");  // Redirect to the view cart page
        }
        public IActionResult Checkout(List<int> productIds)
        {
            return View();
        }

        public IActionResult PlaceOrder(int customerId, List<int> productIds)
        {

            return View();
        }

        public IActionResult GetAllOrders()
        {

            return View();
        }
    }
}
