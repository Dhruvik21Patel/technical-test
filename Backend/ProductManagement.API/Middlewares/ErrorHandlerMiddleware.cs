namespace ProductManagement.API.Middlewares
{
    using ProductManagement.BusinessLayer.Extensions;
    using ProductManagement.Common.Exceptions;
    using ProductManagement.Entites.Response;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Net;
    using static ProductManagement.Common.Constants.ErrorMessages;

    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            //GenerateErrorResponse() will return the response of type ApiResponseDTO statuscode, message, content
            ErrorResponse<object> response = GenerateErrorResponse(context, ex);

            //It is used to convert the response in swagger into camel case we can return response instead of jsonresponse but it will return DTO
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string jsonResponse = JsonConvert.SerializeObject(response, serializerSettings);
            await context.Response.WriteAsync(jsonResponse);
        }

        private ErrorResponse<object> GenerateErrorResponse(HttpContext context, Exception ex)
        {
            int httpStatusCode = (int)HttpStatusCode.InternalServerError;

            List<string> generalMessages = new();
            Dictionary<string, string> modelStateMessages = new();


            void AddStatusCodeAndMessage(int statusCode, List<string> messages)
            {
                httpStatusCode = statusCode;
                generalMessages.AddRange(messages);
            }

            void AddStatusCodeAndMessageModalState(int statusCode, Dictionary<string, string> messages)
            {
                httpStatusCode = statusCode;
                modelStateMessages = messages;
            }

            switch (ex)
            {
                case ModelStateException exception:
                    AddStatusCodeAndMessageModalState(HttpStatusCode.BadRequest.GetValue(), exception.Messages);
                    break;
                case CustomException customException:
                    AddStatusCodeAndMessage(customException.StatusCode, customException.Messages);
                    break;
                default:
                    AddStatusCodeAndMessage(HttpStatusCode.InternalServerError.GetValue(), new List<string>() { ExceptionMessage.INTERNAL_SERVER });
                    break;
            }
            //for showing same statuscode as of apiresponse in swagger
            context.Response.StatusCode = httpStatusCode;

            //In ApiResponseDTO we have constructor which set status code and message but for success we have to set manually
            return new ErrorResponse<object>(
    httpStatusCode,
    generalMessages.Any() ? generalMessages : new List<string>(),
    modelStateMessages.Any() ? modelStateMessages : null
);
        }
    }
}
