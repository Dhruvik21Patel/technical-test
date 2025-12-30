namespace ProductManagement.Entities.Response
{
    public class PageResponse<T>
    {
        public PageResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalPage, long totalRecords)
        {
            Result = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPage = totalPage;
            TotalRecords = totalRecords;
        }

        public IEnumerable<T> Result { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPage { get; set; }

        public long TotalRecords { get; set; }
    }
}
