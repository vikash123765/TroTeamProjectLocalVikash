using System.ComponentModel.DataAnnotations;

namespace MovieShopTrio.Database
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
    }
}
