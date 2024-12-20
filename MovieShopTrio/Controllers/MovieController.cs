using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
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



        [HttpGet]
        public IActionResult Details(int id)
        {
            var movieDetails = _movieService.GetDetails(id);
            if (movieDetails == null)
            {

                return NotFound();
            }
            else
            {
                return View(movieDetails);

            };


        }
    }
}