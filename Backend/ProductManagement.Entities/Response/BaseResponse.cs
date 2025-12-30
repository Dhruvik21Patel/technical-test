namespace ProductManagement.Entites.Response
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
