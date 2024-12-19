using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieShopTrio.Controllers;
using MovieShopTrio.Database;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Services
{
    public class MovieService: IMovieService
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

        public Movie  GetDetails(int id)
        {
            var movie = _MoviedbContext.Movies.FirstOrDefault(c => c.Id == id);

            return movie;
        }

        public bool EditMovie(int id, Movie updatedMovie)
        {
            var existingMovie = _MoviedbContext.Movies.FirstOrDefault(c => c.Id == id);

            if (existingMovie == null)
            {
                return false; // Movie not found
            }

            // Update the movie with the new values
            existingMovie.Title = updatedMovie.Title;
            existingMovie.Director = updatedMovie.Director;
            existingMovie.Description = updatedMovie.Description;
            existingMovie.ReleaseYear = updatedMovie.ReleaseYear;
            existingMovie.Price = updatedMovie.Price;

            // Save changes to the database
            _MoviedbContext.SaveChanges();

            return true; // Movie updated successfully
        }



    }
}
