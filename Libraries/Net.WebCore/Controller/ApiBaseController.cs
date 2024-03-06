using Microsoft.AspNetCore.Mvc;
using Net.WebCore.JsonResult;

namespace Net.WebCore.Controller
{
    [Route("api")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected async Task<IActionResult> Json()
        {
            return await RawJsonResult.Send();
        }

        protected async Task<IActionResult> Json<T>(T iResponse) where T : class
        {
            return await RawJsonResult.Send(iResponse);
        }

        protected async Task<IActionResult> Error()
        {
            return await RawJsonResult.Error("");
        }

        protected async Task<IActionResult> Error(dynamic error, int iStatusCode = 400)
        {
            return await RawJsonResult.Error(error, iStatusCode);
        }


    }
}
