using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Controllers
{
    public class CustomerController : Controller
    {
        public readonly MovieDbContext _db;
        public readonly ICustomerService _customerService;

        public CustomerController(MovieDbContext db, ICustomerService customerService)
        {
            _db = db;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
