namespace MovieShopTrio.Models
{
    public class CartItemViewModel
    {
        public int Id { get; set; } 
        public int Quantity { get; set; }
        public MovieViewModel Movie { get; set; }
    }

}
