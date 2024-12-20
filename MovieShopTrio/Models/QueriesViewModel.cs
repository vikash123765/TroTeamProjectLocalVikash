using MovieShopTrio.Database;

namespace MovieShopTrio.Models
{
    public class QueriesViewModel
    {
        public List<Movie> MostPopular { get; set; } = new List<Movie>();
        public List<Movie> Newest5 { get; set; } = new List<Movie>();

        public List<Movie> Oldest5 { get; set; } = new List<Movie>();

        public List<Movie> Cheapest5 { get; set; } = new List<Movie>();

        public List<Movie> TopCustomer { get; set; } = new List<Movie>();
    }
}
