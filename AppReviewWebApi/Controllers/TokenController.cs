using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Net.WebCore.Controller;
using Net.WebCore.JsonResult;
using AppReviewWebApi.Models.Payload;
using AppReviewWebApi.Models.Request;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace AppReviewWebApi.Controllers
{
    [AllowAnonymous]
    public class TokenController : ApiBaseController
    {
        #region Variable
        private readonly IValidator<LoginModel> loginValidator;
        #endregion

        #region CTor
        public TokenController(IValidator<LoginModel> iLoginValidator)
        {
            loginValidator = iLoginValidator;
        }
        #endregion

        [HttpPost]
        [Route("auth-token", Name = "RequestToken")]
        [ProducesResponseType(typeof(TokenResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] LoginModel model)
        {
            var _validateResult = loginValidator.Validate(model);

            if (!_validateResult.IsValid)
            {
                _validateResult.AddToModelState(ModelState);
                return BadRequest();
            }
            var res = new TokenResponse()
            {

            };
            return await Json<TokenResponse>(res);
        }

        [HttpPost]
        [Route("guest-token", Name = "GuestToken")]
        [ProducesResponseType(typeof(TokenResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateGuest()
        {
            var res = new TokenResponse()
            {

            };
            return await Json<TokenResponse>(res);
        }

        [HttpPost]
        [Route("check-token", Name = "CheckToken")]
        public async Task<IActionResult> CheckToken()
        {
            return await Json();
        }
    }
}
