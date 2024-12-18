using MovieShopTrio.Controllers;
using MovieShopTrio.Database;
using MovieShopTrio.Services.Interfaces;

namespace MovieShopTrio.Services
{
    public class OrderService: IOrderService
    {
 
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private readonly MovieDbContext _MoviedbContext;

        List<Order> orderList = new List<Order>();
        public OrderService(MovieDbContext moviedbContext, IHttpContextAccessor iHttpContextAccessor)
        {

            _MoviedbContext = moviedbContext;
            _IHttpContextAccessor = iHttpContextAccessor;
        }




    }
}
