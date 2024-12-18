using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Services;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly MovieDbContext _db;

        public MovieController(IMovieService movieService, MovieDbContext db)
        {
            _db = db;
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Create(Movie movie)
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

        public IActionResult GetAllMovies()
        {

            return View(_movieService.GetAllMovies());
        }


		// Handle the delete confirmation
		[HttpPost]

		public IActionResult DeleteMovie(int id)
		{
			_movieService.DeleteMovie(id);

            // Redirect to the movie list page after deletion
            return RedirectToAction("GetAllMovies");

        }
	}
}
