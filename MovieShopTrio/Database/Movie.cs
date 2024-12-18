using System.ComponentModel.DataAnnotations;
namespace MovieShopTrio.Database
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Director { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
   
        
        public string Description { get; set; }

        public virtual ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
    }
}
