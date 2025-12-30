namespace ProductManagement.Entites.Response
{
    public class ApiResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
