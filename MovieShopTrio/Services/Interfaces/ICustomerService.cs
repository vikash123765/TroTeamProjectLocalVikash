using MovieShopTrio.Database;
using MovieShopTrio.Models;

namespace MovieShopTrio.Services.Interfaces
{
    public interface ICustomerService
    {
        public void CreateCustomer(Customer customer);
        public List<Customer> GetAllCustomers();
        public Customer GetDetails(int id);
        public bool UpdateCustomer(int id, Customer updatedCustomer);
        public void DeleteCustomer(int id);
        public CustomerOrdersViewModel GetOrdersByCustomerEmail(string email);
    }
}
