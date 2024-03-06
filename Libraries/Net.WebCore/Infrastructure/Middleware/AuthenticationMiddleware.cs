using Microsoft.AspNetCore.Http;
using Net.Core.Infrastructure;
using System.Net;
using Net.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net.Core.Authentication;

namespace Net.WebCore.Infrastructure.Middleware
{
    public class AuthenticationMiddleware
    {
        #region Variable
        private readonly RequestDelegate _next;
        private readonly TokenOptions _tokenOptions;

        #endregion
        public AuthenticationMiddleware(RequestDelegate next, TokenOptions tokenOptions)
        {
            _next = next;
            _tokenOptions = tokenOptions;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // if request is login path then next (doesn't check valid token)
            if (context.Request.Path.Equals(new PathString(_tokenOptions.Path)))
            {
                await _next(context);
                return;
            }

            // The request is swagger then next
            if (context.Request.Path.ToString().Contains("swagger"))
            {
                await _next(context);
                return;
            }

            var _api = Singleton<AppSettings>.Instance.Get<ApiConfig>();

            // Get token from header when request has been send
            var token = context.Request.Headers.Authorization.FirstOrDefault<String>();

            // If token is not blank then it's will check valid
            if (!String.IsNullOrWhiteSpace(token))
            {
               var _tokenService = context.RequestServices.GetService<ITokenProviderService>();

                if (!await _tokenService.IsValidTokenAsync(token))
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsJsonAsync(new { message = "Authentication Fail" });
                    return;
                }
            }

            await _next(context);
        }
    }
}
