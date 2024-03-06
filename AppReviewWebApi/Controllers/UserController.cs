using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net.WebCore.Controller;

namespace AppReviewWebApi.Controllers
{
    [Authorize]
    public class UserController : ApiBaseController
    {
        [HttpGet]
        public Task Get()
        {
            return Json();
        }
    }
}
