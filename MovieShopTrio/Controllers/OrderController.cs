using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Controllers
{
    public class OrderController : Controller
    {
        private readonly MovieDbContext _db;
        private readonly IOrderService _orderService;

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

        // Checkout action
        public IActionResult Checkout(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            // Delegate the logic to the service layer
            var isExistingCustomer = _orderService.Checkout(email);

            if (isExistingCustomer)
            {
                // Redirect to OrderPlaced view
                return RedirectToAction("OrderPlaced");
            }

            // Redirect to CheckoutRegistration if the customer doesn't exist
            return RedirectToAction("Registration", "Order", new { email });
        }

        // Place Order action for customer registration
        [HttpPost]
        public IActionResult PlaceOrder(int customerId)
        {
            var isOrderPlaced = _orderService.PlaceOrder(customerId);

            if (isOrderPlaced)
            {
                return RedirectToAction("OrderPlaced");
            }

            return StatusCode(500, "Failed to place order.");
        }

        // Order success page
        public IActionResult OrderPlaced()
        {
            return View();
        }

        // Registration form
        public IActionResult Registration(string email)
        {
            var model = new Customer
            {
                EmailAddress = email // Pre-fill the email
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult RegisterCustomerAndPlaceOrder(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Step 1: Register the customer
                var customerId = _orderService.RegisterCustomer(customer);

                // Step 2: Place the order using the customer ID
                _orderService.PlaceOrder(customerId);

                // Step 3: Redirect to a confirmation view (Order Placed successfully)
                return RedirectToAction("OrderPlaced");
            }

            // If model validation fails, stay on the registration page
            return View();
        }


    }

}
