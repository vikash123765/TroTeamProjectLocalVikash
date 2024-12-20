using MovieShopTrio.Database;

namespace MovieShopTrio.Services.Interfaces
{
    public interface IHomeService
    {
        //public List<Movie> MostPopular();

        public List<Movie> Newest5();

        public List<Movie> oldest5();

        public List<Movie> Cheapest5();

        //public List<Movie> TopCustomer();
    }
}
