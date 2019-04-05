namespace HomeApp.API.Helpers
{
    public class HomeParams
    {
        public int CurrentPage { get; set; } = 1;
        private const int MaxPageSize = 20;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}