using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public IActionResult GetAllMoviesPaginated(int page = 1, int pageSize = 10)
        {
            string query = ""; // Default to empty if no search query is provided
            int? choice = null; // Default to null if no choice is provided

            // Retrieve 'query' from the request query string, if present
            if (Request.Query.ContainsKey("query"))
            {
                query = Request.Query["query"];
            }

            // Retrieve 'choice' from the request query string, if present
            if (Request.Query.ContainsKey("choice"))
            {
                choice = int.TryParse(Request.Query["choice"], out int parsedChoice) ? parsedChoice : (int?)null;
            }
            // Retrieve 'page' from the request query string, if present
            if (Request.Query.ContainsKey("page"))
            {
                page = int.TryParse(Request.Query["page"], out int parsedPage) ? parsedPage : 1;
            }

            // Use the service to get paginated movies, including search and sort options
            var viewModel = _movieService.GetMovies(query, choice, page, pageSize);

            // Pass query and choice back in the model
            viewModel.Query = query;
            viewModel.Choice = choice;
            viewModel.CurrentPage = page;
            return View(viewModel);
        }



        [HttpPost]
        public IActionResult DeleteMovie(int id)
        {
            _movieService.DeleteMovie(id);
            return RedirectToAction("GetAllMovies");
        }
        public IActionResult GetAllMovies()
        {

            return View(_movieService.GetAllMovies());
        }
   
 

        [HttpGet]
        public IActionResult EditMovie(int id)
        {
            var movie = _movieService.GetDetails(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        public IActionResult EditMovie(int id, Movie movie)
        {
            if (ModelState.IsValid)
            {
                var updateSuccessful = _movieService.EditMovie(id, movie);

                if (!updateSuccessful)
                {
                    return NotFound();
                }

                return RedirectToAction("GetAllMovies");
            }

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

            return View(movieDetails);
        }


       

    }
}
