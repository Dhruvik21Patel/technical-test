namespace PartialSchoolManagement.Entities.DTOModels.Response
{
    using System.Collections.Generic;
    public class PaginationResultDTO<T>
    {
        public IEnumerable<T>? Data { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }
    }
}