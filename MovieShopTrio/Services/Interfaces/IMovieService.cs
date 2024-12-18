using Microsoft.AspNetCore.Mvc;
using MovieShopTrio.Database;

namespace MovieShopTrio.Services.Interfaces
{
    public interface IMovieService
    {
        public void Create(Movie movie);

        public List<Movie> GetAllMovies();

        public void DeleteMovie(int id);

	}
}
