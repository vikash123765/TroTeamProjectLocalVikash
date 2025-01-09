using MovieShopTrio.Database;

namespace MovieShopTrio.Models
{
    public class CustomerOrdersViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public int OrderCount => Orders.Count;

        public decimal GetTotalCost(Order order)
        {
            return order.OrderRows.Sum(orderRow => orderRow.Price);
        }
    }
}
