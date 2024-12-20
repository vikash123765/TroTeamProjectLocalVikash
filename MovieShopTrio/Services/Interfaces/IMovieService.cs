using MovieShopTrio.Database;

namespace MovieShopTrio.Services.Interfaces
{
    public interface IMovieService
    {
        public void Create(Movie movie);

        public List<Movie> GetAllMovies();

        public void DeleteMovie(int id);

        public Movie GetDetails(int id);

        public bool EditMovie(int id, Movie movie);
    }
}
