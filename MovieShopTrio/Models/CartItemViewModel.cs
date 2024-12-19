using MovieShopTrio.Database;

namespace MovieShopTrio.Models
{
    public class CartItemViewModel
    {
        public Movie Movie { get; set; }  // Movie object
        public int Quantity { get; set; }  // Quantity of the movie in the cart
    }
}
