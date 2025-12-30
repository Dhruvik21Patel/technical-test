namespace ProductManagement.Entities.Request
{
    public class TokenDTO
    {

        public string? Token { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public int? UserId { get; set; }
    }
}