namespace ProductManagement.Entites.Request
{
    using ProductManagement.Common.Constants;

    public class PageRequest
    {
        private int _pageNumber = 1;
        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value <= 0 ? 1 : value; }
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value <= 0 || value >= 50 ? 50 : value; }
        }

        public string? SortBy { get; set; }

        public string SortOrder { get; set; } = SystemConstants.ASCENDING;

        public string? SearchKey { get; set; }

        public PageRequest(int pageNumber = 0, int pageSize = 0)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = (pageSize <= 0 || pageSize >= 50) ? 50 : pageSize;
        }

        public PageRequest() { }
    }
}
