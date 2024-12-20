using MovieShopTrio.Database;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Services
{
    public class HomeService:IHomeService
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private readonly MovieDbContext _db;

       
        public HomeService(MovieDbContext db, IHttpContextAccessor iHttpContextAccessor)
        {

            _db =db;
            _IHttpContextAccessor = iHttpContextAccessor;
        }
        //public List<Movie> MostPopular()
        //{

        //}

        public List<Movie> Newest5()
        {
            var new5 = _db.Movies.Take(5).ToList().OrderByDescending(m => m.ReleaseYear).ToList();
            return new5;
        }

        public List<Movie> oldest5()
        {
            var old5 = _db.Movies.Take(5).ToList().OrderBy(x => x.ReleaseYear).ToList();
            return old5;
        }

        public List<Movie> Cheapest5()
        {
            var cheapest = _db.Movies.Take(5).ToList().OrderBy(c => c.Price).ToList();
            return cheapest;
        }

        //public List<Movie> TopCustomer()
        //{

        //}
    }
}
