using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Services;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMovieService _movieService;
        private readonly ICustomerService _customerService;


        public AdminController(IOrderService orderService, IMovieService movieService, ICustomerService customerService)
        {
            _orderService = orderService;
            _movieService = movieService;
            _customerService = customerService;
        }
        public IActionResult Index()
        {
            return View();
        }

        // ----------------------------- ORDER ACTIONS ------------------------------

        public IActionResult Orders(string customerName, DateTime? startDate, DateTime? endDate)
        {
            var orders = _orderService.GetFilteredOrders(customerName, startDate, endDate);
            return View(orders);
        }

        [HttpGet("Admin/Orders/{email}")]
        public IActionResult OrdersByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var customerOrders = _customerService.GetOrdersByCustomerEmail(email);

            if (customerOrders == null)
                return NotFound($"No orders found for customer with email: {email}");


            return View(customerOrders);

        }

        // ----------------------------- MOVIES ACTIONS ------------------------------

        public IActionResult GetAllMovies()
        {
            return View(_movieService.GetAllMovies());
        }

        [HttpGet]
        public IActionResult CreateMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieService.Create(movie);
                return RedirectToAction("MovieSuccess");
            }
            return View();
        }

        public IActionResult MovieSuccess()
        {
            return View();
        }

        // Handle the delete confirmation
        [HttpPost]
        public IActionResult DeleteMovie(int id)
        {
            _movieService.DeleteMovie(id);

            // Redirect to the movie list page after deletion
            return RedirectToAction("GetAllMovies");
        }

        [HttpGet]
        public IActionResult EditMovie(int id)
        {
            // Get the movie by ID from the database
            var movie = _movieService.GetDetails(id);

            // If the movie doesn't exist, return a NotFound result
            if (movie == null)
            {
                return NotFound();
            }

            // Pass the movie object to the view
            return View(movie);
        }

        [HttpPost]
        public IActionResult EditMovie(int id, Movie movie)
        {
            // Ensure that the model is valid
            if (ModelState.IsValid)
            {
                // Call the service to update the movie
                bool updateSuccessful = _movieService.EditMovie(id, movie);

                if (!updateSuccessful)
                {
                    // Movie not found, return a NotFound result
                    return NotFound();
                }

                // Redirect to the movie list page after saving
                return RedirectToAction("GetAllMovies");
            }

            // If the model is invalid, return the form view again with validation errors
            return View(movie);
        }


        // ----------------------------- CUSTOMER ACTIONS ------------------------------

        public IActionResult GetAllCustomers()
        {
            return View(_customerService.GetAllCustomers());
        }
        
        public IActionResult EditCustomer(int id)
        {
            var customer = _customerService.GetDetails(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult EditCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                bool updateSuccesful = _customerService.UpdateCustomer(id, customer);

                if (!updateSuccesful)
                {
                    return NotFound();
                }

                return RedirectToAction("GetAllCustomers");
            }
            
            return View(customer);            
        }

        public IActionResult DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);

            return RedirectToAction("GetAllCustomers");
        }
    }
}
