using MovieShopTrio.Controllers;
using MovieShopTrio.Database;
using MovieShopTrio.Services.Interfaces;
using System.Net;

namespace MovieShopTrio.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private readonly MovieDbContext _MoviedbContext;

        List<Customer> customerList = new List<Customer>();
        public CustomerService(MovieDbContext moviedbContext, IHttpContextAccessor iHttpContextAccessor)
        {

            _MoviedbContext = moviedbContext;
            _IHttpContextAccessor = iHttpContextAccessor;
        }

        public void CreateCustomer(Customer customer)
        {
            _MoviedbContext.Customers.Add(customer);
            _MoviedbContext.SaveChanges();
        }

        public List<Customer> GetAllCustomers()
        {
            customerList = _MoviedbContext.Customers.OrderBy(c => c.Id).ToList();
            return customerList;
        }
        public bool UpdateCustomer(int id, Customer updatedCustomer)
        {
            if(updatedCustomer == null) throw new ArgumentNullException(nameof(updatedCustomer));

            var customer = _MoviedbContext.Customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customer.Name = updatedCustomer.Name;
                customer.BillingAddress = updatedCustomer.BillingAddress;
                customer.BillingCity = updatedCustomer.BillingCity;
                customer.BillingZip = updatedCustomer.BillingZip;
                customer.DeliveryAddress = updatedCustomer.DeliveryAddress;
                customer.DeliveryCity = updatedCustomer.DeliveryCity;
                customer.DeliveryZip = updatedCustomer.DeliveryZip;
                customer.EmailAddress = updatedCustomer.EmailAddress;
                customer.PhoneNumber = updatedCustomer.PhoneNumber;

                _MoviedbContext.SaveChanges();

                return true;
            }
            else
            {
                return false; 
            }
        }

        public void DeleteCustomer(int id)
        {
            var customer = _MoviedbContext.Customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                _MoviedbContext.Customers.Remove(customer);
            }
            _MoviedbContext.SaveChanges();
        }
    }
}
