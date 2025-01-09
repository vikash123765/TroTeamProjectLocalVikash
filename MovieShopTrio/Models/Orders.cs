using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MovieShopTrio.Models
{
    public class Orders
    {
        
            [Key]
            public int OrderID { get; set; }

            [Required]
            public DateTime OrderDate { get; set; }

            [Required]
            [MaxLength(500)]
            public string OrderDetails { get; set; }
        
    }

    public class MostExpenisveOrderModel
    {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public decimal TotalOrderValue { get; set; }
    }
}
