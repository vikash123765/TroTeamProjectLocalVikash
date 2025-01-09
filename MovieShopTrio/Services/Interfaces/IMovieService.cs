using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;
using MovieShopTrio.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieShopTrio.Services.Interfaces
{
    public interface IMovieService
    {
        public void Create(Movie movie);

        public List<Movie> GetAllMovies();

        public void DeleteMovie(int id);

        public Movie GetDetails(int id);
    
       
        public bool EditMovie(int id, Movie movie);


    
            // Get paginated movies, with search query and sorting choice (nullable)
          public  MoviePaginationViewModel GetMovies(string query, int? choice, int page, int pageSize);

            // Other service methods, like sorting/filtering, can go here
      


    }
}
