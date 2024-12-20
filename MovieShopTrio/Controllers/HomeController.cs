using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieDbContext _db;
        private readonly IHomeService _homeService;

        
       
        public HomeController(ILogger<HomeController> logger, MovieDbContext db, IHomeService homeService)
        {
            _logger = logger;
            _db = db;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            QueriesViewModel obj = new QueriesViewModel();
           /* obj.MostPopular = _homeService.MostPopular()*/;
            obj.Newest5 = _homeService.Newest5();
            obj.Oldest5 = _homeService.oldest5();
            obj.Cheapest5 = _homeService.Cheapest5();
           /* obj.TopCustomer = _homeService.TopCustomer()*/;
            return View(obj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
