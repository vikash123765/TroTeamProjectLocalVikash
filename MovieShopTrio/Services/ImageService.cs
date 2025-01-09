using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using MovieShopTrio.Database;

namespace MovieShopTrio.Services
{
    public class ImageService
    {
        private readonly IConfiguration _configuration;
        private readonly MovieDbContext _db;
        private readonly string _apiKey;

        public ImageService(IConfiguration configuration, MovieDbContext db)
        {
            _configuration = configuration;
            _db = db;
            _apiKey = _configuration["TMDB:ApiKey"];
        }

        public async Task UpdatePosterPathsAsync()
        {
            var httpClient = new HttpClient();
            var movies = await _db.Movies.ToListAsync();

            foreach (var movie in movies)
            {
                try
                {
                    var (posterUrl, backdrop) = await GetPosterUrlAsync(httpClient, movie.Title, movie.ReleaseYear);
                    if (posterUrl != null || backdrop != null)
                    {
                        movie.URLImage = posterUrl;
                        movie.BackdropImage = backdrop;
                        _db.Movies.Update(movie);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading a movie {movie.Title}: {ex.Message}");
                }
            }

            await _db.SaveChangesAsync();
            Console.WriteLine("Poster paths updated successfully");
        }

        private async Task<(string?, string?)> GetPosterUrlAsync(HttpClient httpClient, string title, int releaseYear)
        {
            var query = $"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(title)}&year={releaseYear}";
            var response = await httpClient.GetStringAsync(query);
            var json = JObject.Parse(response);
            var results = json["results"] as JArray;

            if (results == null || !results.Any())
                return (null, null);

            var backdrop = results.First["backdrop_path"]?.ToString();

            var posterPath = results.First["poster_path"]?.ToString();
            return (posterPath != null ? $"https://image.tmdb.org/t/p/w500{posterPath}" : null, backdrop != null ? $"https://image.tmdb.org/t/p/w500{backdrop}" : null);
        }
    }
}