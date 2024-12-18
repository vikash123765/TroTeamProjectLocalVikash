using System.ComponentModel.DataAnnotations;

namespace MovieShopTrio.Database
{
    public class OrderRow
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
