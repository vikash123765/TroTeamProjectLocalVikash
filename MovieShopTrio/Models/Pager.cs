namespace MovieShopTrio.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public Pager(int totalItems, int currentPage, int pageSize = 10)
        {
            TotalItems = totalItems;
            CurrentPage = currentPage < 1 ? 1 : currentPage;
            PageSize = pageSize;

            StartPage = CurrentPage - 5;
            EndPage = CurrentPage + 4;

            if (StartPage < 1)
            {
                EndPage -= (StartPage - 1);
                StartPage = 1;
            }

            if (EndPage > TotalPages)
            {
                EndPage = TotalPages;
                if (EndPage > 10)
                {
                    StartPage = EndPage - 9;
                }
            }
        }
    }
}
