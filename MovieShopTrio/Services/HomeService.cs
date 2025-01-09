using Microsoft.EntityFrameworkCore;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private readonly MovieDbContext _db;


        public HomeService(MovieDbContext db, IHttpContextAccessor iHttpContextAccessor)
        {

            _db = db;
            _IHttpContextAccessor = iHttpContextAccessor;
        }
        public List<Movie> MostPopular()
        {
            var MostPopularmovie = _db.Movies
            .OrderByDescending(x => x.OrderRows.Count()).Take(5).ToList();
            return MostPopularmovie;
        }


        public List<Movie> Newest5()
        {
            var new5 = _db.Movies.Take(5).OrderByDescending(m => m.ReleaseYear).Take(5).ToList();
            return new5;
        }

        public List<Movie> oldest5()
        {
            var old5 = _db.Movies.OrderBy(x => x.ReleaseYear).Take(5).ToList();
            return old5;
        }

        public List<Movie> Cheapest5()
        {
            var cheapest = _db.Movies.ToList().OrderBy(c => c.Price).Take(5).ToList();
            return cheapest;
        }


        public MostExpenisveOrderModel MostExpensiveOrder()
        {
            var mostExpensiveOrder = _db.Orders.Include(o => o.OrderRows)
                .GroupBy(order => order.Id).Select(group => new MostExpenisveOrderModel
                {
                    CustomerId = group.Key,
                    CustomerName= group.FirstOrDefault().Customer.Name,
                    TotalOrderValue = group.Sum(order => order.OrderRows.Sum(row => row.Price))
                }).ToList()
                .OrderByDescending(x => x.TotalOrderValue)
                .FirstOrDefault();
       
            if (mostExpensiveOrder != null)
            {
            return mostExpensiveOrder;
            }
            return null;
        }
    }
}

