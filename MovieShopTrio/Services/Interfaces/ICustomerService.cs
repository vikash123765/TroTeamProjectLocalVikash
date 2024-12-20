using MovieShopTrio.Database;

namespace MovieShopTrio.Services.Interfaces
{
    public interface ICustomerService
    {
        public void CreateCustomer(Customer customer);
        public List<Customer> GetAllCustomers();
        public bool UpdateCustomer(int id, Customer updatedCustomer);
        public void DeleteCustomer(int id);
    }
}
