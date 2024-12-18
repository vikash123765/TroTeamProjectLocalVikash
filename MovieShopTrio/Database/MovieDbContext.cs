using Microsoft.EntityFrameworkCore;
using MovieShopTrio.Database;

namespace MovieShopTrio.Database
{
    public class MovieDbContext:DbContext
    {
        public virtual DbSet<Customer> Customers {get; set;}
        public virtual DbSet<Movie> Movies {get; set;}
        public virtual DbSet<Order> Orders {get; set;}

        public virtual DbSet<OrderRow> OrderRows {get; set;}

        public MovieDbContext()
        {

        }
        public MovieDbContext(DbContextOptions<MovieDbContext> options):base(options)
        {

        }
    }

   
}
