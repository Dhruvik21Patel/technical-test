public class ApiResult<T>
        where T : class
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public bool IsValid
    {
        get
        {
            IEnumerable<T>? enumerable = this.Data as IEnumerable<T>;

            if (enumerable != null && enumerable.Any())
            {
                return true;
            }

            return this.Data != null;
        }
    }
    public bool IsSuccessStatusCode
    {
        get
        {
            return this.Code >= 200 && this.Code <= 299;
        }
    }
}