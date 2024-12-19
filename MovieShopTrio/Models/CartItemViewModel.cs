using MovieShopTrio.Database;
using MovieShopTrio.Views.Movie;

namespace MovieShopTrio.Models
{
    public class CartItemViewModel
    {

        public MovieViewModel Movie { get; set; }  
        public int Quantity { get; set; }  // Quantity of the movie in the cart
    }
}
