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
    }
}
