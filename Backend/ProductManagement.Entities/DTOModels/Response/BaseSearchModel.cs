namespace ProductManagement.Entities.DTOModels.Response
{
    public abstract class BaseSearchModel<T> : SortingModel
    {
        public int PageNo { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public int PageSize { get; set; } = 20;
        public int TotalRecordCount { get; set; }
        public int? NextPage { get; set; }
        public int? PrevPage { get; set; }
        public IList<T> Records { get; set; } = new List<T>();
        public int? Offset
        {
            get
            {
                return (PageNo - 1) * PageSize;
            }
        }


    }

    public abstract class SortingModel
    {
        public bool? SortBy { get; set; }
        public string? SortColumn { get; set; } = string.Empty;
    }
}
