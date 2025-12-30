namespace ProductManagement.API.Helpers
{
    using ProductManagement.Common.Constants;
    using ProductManagement.Entities.Response;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net;

    public static class ResponseHelper
    {
        public static IActionResult CreatedResponse<T>(T? data, string message) where T : class
        {
            ApiResult<T>? result = new ApiResult<T>
            {
                Code = (int)HttpStatusCode.Created,
                Message = message,
                Data = data,
            };

            return new ObjectResult(result)
            {
                StatusCode = (int)HttpStatusCode.Created
            };
        }
        public static IActionResult SuccessResponse<T>(T? data, string message = SystemConstants.SUCCESS) where T : class
        {
            ApiResult<T> result = new()
            {
                Code = (int)HttpStatusCode.OK,
                Message = message,
                Data = data,
            };
            return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.OK };

        }
        public static IActionResult GetJsonStringResult<T>(T? data, HttpStatusCode statusCode = HttpStatusCode.OK, Exception? ex = null)
        where T : class
        {
            JsonSerializerSettings? settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string jsonData = JsonConvert.SerializeObject(data, settings);
            ApiResult<string> result = new ApiResult<string>
            {
                Data = jsonData,
                Code = (int)statusCode,
                Message = GetMessageByStatusCode(statusCode)
            };

            if (ex != null)
            {
                result.Message = ex.Message;
            }

            return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.OK };
        }

        public static IActionResult CreatePageResponse<T>(IEnumerable<T> data, int pageNumber, int pageSize, int totalPage, long totalRecords = 0) where T : class
        {
            PageResponse<T> PaginationResult = new(data, pageNumber, pageSize, totalPage, totalRecords);
            ApiResult<PageResponse<T>> result = new()
            {
                Code = (int)HttpStatusCode.OK,
                Message = GetMessageByStatusCode(HttpStatusCode.OK),
                Data = PaginationResult,
            };
            return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.OK };
        }


        private static string GetMessageByStatusCode(HttpStatusCode _statusCode)
        {
            switch (_statusCode)
            {
                case HttpStatusCode.OK:
                    return SystemConstants.HttpStatusMessageSuccess;
                case HttpStatusCode.Forbidden:
                    return SystemConstants.HttpStatusMessageUnauthorized;
                case HttpStatusCode.NotFound:
                    return SystemConstants.HttpStatusMessageRecordNotFound;
                default:
                    return _statusCode.ToString();
            }
        }

    }
}
