using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieShopTrio.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MovieDbContext _db;
        private readonly ICustomerService _customerService;

        public CustomerController(MovieDbContext db, ICustomerService customerService)
        {
            _db = db;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View(_customerService.GetAllCustomers());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(customer);
                return RedirectToAction("CustomerAdded");
            }
            return View();
        }

        public IActionResult CustomerAdded()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(int id, Customer customer)
        {
            _customerService.UpdateCustomer(id, customer);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);

            return RedirectToAction("Index");
        }

        [HttpGet("Customer/Orders/{email}")]
        public IActionResult Orders(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var customerOrders = _customerService.GetOrdersByCustomerEmail(email);

            if (customerOrders == null)
                return NotFound($"No orders found for customer with email: {email}");


            return View(customerOrders);

        }
    }
}
