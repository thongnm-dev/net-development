using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Core.Extensions;
using Newtonsoft.Json;

namespace Net.WebCore.JsonResult
{
    internal class RawJsonResult : ActionResult
    {
        #region Field
        [JsonProperty("response")]
        public ApiResponse DataResponse { get; set; } = new ApiResponse();

        [JsonProperty("status")]
        public int StatusCode { get; set; } = 200;
        #endregion

        #region Constructor

        public RawJsonResult()
        {

        }

        public RawJsonResult(dynamic iData)
        {
            DataResponse = new ApiResponse
            {
                Data = iData,
                Message = ApiStatus.SUCCESS.GetValueString()
            };
        }
        #endregion

        #region Utils
        public override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCode;

            return response.WriteAsJsonAsync(DataResponse);
        }
        #endregion

        #region Method

        public static async Task<IActionResult> Send()
        {
            var res = new RawJsonResult()
            {
                DataResponse = new ApiResponse()
                {
                    Message = ApiStatus.SUCCESS.GetValueString(),
                },
                StatusCode = 200
            };

            return await Task.FromResult(res);
        }

        public static async Task<IActionResult> Send(dynamic iData)
        {
            var res = new RawJsonResult()
            {
                DataResponse = new ApiResponse()
                {
                    Data = iData,
                    Message = ApiStatus.SUCCESS.GetValueString()
                },
                StatusCode = 200
            };

            return await Task.FromResult(res);
        }

        public static async Task<IActionResult> Error(dynamic iErrors, int iStatusCode = 400)
        {
            if (iErrors is IDictionary<string, string[]>)
            {
                return await BabRequest(iErrors as IDictionary<string, string[]>);
            }

            var res = new RawJsonResult()
            {
                DataResponse = new ApiResponse()
                {
                    Message = iErrors
                },
                StatusCode = iStatusCode
            };

            return await Task.FromResult(res);
        }

        #endregion

        #region Private

        private static async Task<IActionResult> BabRequest(IDictionary<string, string[]> iErrors)
        {
            var errors = iErrors.Where(x => x.Value != null && x.Value.Any())
                                    .Select(x => ErrorMapping(x))
                                    .ToList();

            var res = new RawJsonResult()
            {
                DataResponse = new ApiResponse()
                {
                    Error = errors,
                    Message = ApiStatus.BADREQUEST.GetValueString()
                },
                StatusCode = 400
            };

            return await Task.FromResult(res);
        }

        private static ErrorModel ErrorMapping(KeyValuePair<string, string[]> iError)
        {
            return new ErrorModel(iError.Key, iError.Value.FirstOrDefault());
        }

        #endregion

    }
}
