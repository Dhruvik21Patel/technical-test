namespace ProductManagement.Common.Exceptions
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ModelStateException : Exception
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;

        public Dictionary<string, string> Messages { get; }

        public Dictionary<string, object>? Metadata { get; set; }

        public ModelStateException(ModelStateDictionary modelState, Dictionary<string, object>? metadata = null)
            : base("Invalid model state")
        {
            Messages = ExtractMessages(modelState);
            Metadata = metadata;
        }

        private static Dictionary<string, string> ExtractMessages(ModelStateDictionary modelState)
        {
            var result = new Dictionary<string, string>();

            if (modelState == null || modelState.IsValid)
                return result;

            foreach (var entry in modelState)
            {
                string? field = entry.Key;
                string? error = entry.Value?.Errors?.FirstOrDefault()?.ErrorMessage;

                if (!string.IsNullOrWhiteSpace(error))
                {
                    string camelField = char.ToLowerInvariant(field[0]) + field.Substring(1);
                    result[camelField] = error;
                }
            }

            return result;
        }
    }
}