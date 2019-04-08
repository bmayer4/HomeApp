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
        public int Bed { get; set; } = 1;
        public int Bath { get; set; } = 1;
        public int MinPrice { get; set; } = 1;
        public int MaxPrice { get; set; } = 10000000;
        public string OrderBy { get; set; }
        public string SearchQuery { get; set; }
    }
}