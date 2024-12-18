using MovieShopTrio.Controllers;
using MovieShopTrio.Database;
using MovieShopTrio.Services.Interfaces;

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



    }
}
