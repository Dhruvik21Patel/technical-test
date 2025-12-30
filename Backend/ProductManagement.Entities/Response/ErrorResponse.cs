using System.Text.Json.Serialization;

namespace ProductManagement.Entites.Response
{

    public class ErrorResponse<T>
    {
        public int StatusCode { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Messages { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? ModelStateErrors { get; set; }

        public ErrorResponse(int statusCode, List<string>? messages = null, Dictionary<string, string>? modelStateErrors = null)
        {
            StatusCode = statusCode;
            Messages = messages;
            ModelStateErrors = modelStateErrors;
        }
    }
}
