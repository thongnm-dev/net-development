using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Net.Core.Configuration;
using Net.Core.Infrastructure;
using AppReviewService.Users;
using System.Security.Claims;

namespace AppReviewService.Authentication
{
    internal class AuthenticationService : Net.Core.Authentication.IAuthenticationService
    {
        #region Variable
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserservice Userservice;

        #endregion

        public AuthenticationService(IHttpContextAccessor iHttpContextAccessor, IUserservice iUserservice)
        {
            Userservice = iUserservice;
            httpContextAccessor = iHttpContextAccessor;
        }

        public async Task SignInAsync(string username, string password)
        {
            // Get all config from appsettings
            var appSetting = Singleton<AppSettings>.Instance;
            var securityConfig = appSetting.Get<SecurityConfig>();
            var claims = new List<Claim>();

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, AuthenticationDefaults.AuthenticationScheme);

            // create user principal to authen
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(securityConfig.ExpireDate)
            };

            await httpContextAccessor.HttpContext?.SignInAsync(AuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);
        }

        public async Task SignOutAsync()
        {

            await httpContextAccessor.HttpContext?.SignOutAsync(AuthenticationDefaults.AuthenticationScheme);
        }
    }
}
