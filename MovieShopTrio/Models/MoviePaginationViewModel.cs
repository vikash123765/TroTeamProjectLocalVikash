using MovieShopTrio.Database;

namespace MovieShopTrio.Models
{


    public class MoviePaginationViewModel
    {
        public List<Movie> Movies { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Query { get; set; } // To store the search query
        public int? Choice { get; set; } // To store the sorting choice
    }


}
