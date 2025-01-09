using System.Drawing.Printing;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieShopTrio.Controllers;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using MovieShopTrio.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieShopTrio.Services
{
    public class MovieService : IMovieService
    {

        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private readonly MovieDbContext _MoviedbContext;

        List<Movie> movieList = new List<Movie>();
        public MovieService(MovieDbContext moviedbContext, IHttpContextAccessor iHttpContextAccessor)
        {

            _MoviedbContext = moviedbContext;
            _IHttpContextAccessor = iHttpContextAccessor;
        }

        public void Create(Movie movie)
        {
            _MoviedbContext.Movies.Add(movie);
            _MoviedbContext.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movie = _MoviedbContext.Movies.FirstOrDefault(c => c.Id == id);

            _MoviedbContext.Movies.Remove(movie);
            _MoviedbContext.SaveChanges();

        }


        public List<Movie> GetAllMovies()
        {
            var movies = _MoviedbContext.Movies.ToList();

            var qSyntax = from m in _MoviedbContext.Movies select m;
            //var AllMoviesQuey= _MoviedbContext.Movies.FromSqlRaw("select  * from Movies where Price >100 ").ToList();

            var AllMoviesQuey = _MoviedbContext.Movies.FromSqlRaw("select  * from Movies").ToList();
            return movies;
        }

        public Movie GetDetails(int id)
        {
            var movie = _MoviedbContext.Movies.FirstOrDefault(c => c.Id == id);

            return movie;
        }

        public bool EditMovie(int id, Movie updatedMovie)
        {
            var existingMovie = _MoviedbContext.Movies.FirstOrDefault(c => c.Id == id);

            if (existingMovie == null)
            {
                return false;
            }


            existingMovie.Title = updatedMovie.Title;
            existingMovie.Director = updatedMovie.Director;
            existingMovie.Description = updatedMovie.Description;
            existingMovie.ReleaseYear = updatedMovie.ReleaseYear;
            existingMovie.Price = updatedMovie.Price;
            existingMovie.URLImage = updatedMovie.URLImage;



            _MoviedbContext.SaveChanges();

            return true;
        }

     



        public MoviePaginationViewModel GetMovies(string query, int? choice, int page, int pageSize)
        {
            // Start with all movies
            IQueryable<Movie> moviesQuery = _MoviedbContext.Movies.AsQueryable();

            // Apply filtering by query (if present)
            if (!string.IsNullOrEmpty(query))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(query));
            }

            // Apply sorting based on choice (if present)
            if (choice.HasValue)
            {
                switch (choice.Value)
                {
                    case 1: // Price (High to Low)
                        moviesQuery = moviesQuery.OrderByDescending(m => m.Price);
                        break;
                    case 2: // Price (Low to High)
                        moviesQuery = moviesQuery.OrderBy(m => m.Price);
                        break;
                    case 3: // Title (A to Z)
                        moviesQuery = moviesQuery.OrderBy(m => m.Title);
                        break;
                }
            }
            else
            {
                // Default sorting
                moviesQuery = moviesQuery.OrderBy(m => m.Title);
            }

            // Pagination
            int totalMovies = moviesQuery.Count();
            var movies = moviesQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new MoviePaginationViewModel
            {
                Movies = movies,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalMovies / (double)pageSize),
                Query = query,
                Choice = choice
            };
        }





    }
}