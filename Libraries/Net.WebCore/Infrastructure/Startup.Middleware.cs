using Microsoft.AspNetCore.Builder;
using Net.WebCore.Infrastructure.Middleware;
using Net.Core.Configuration;
using Net.Core.Infrastructure;

namespace Net.WebCore.Infrastructure
{
    public class MiddlewareStartup : IAppBuilderStartup
    {
        public int Order => 400;

        public void Configure(IApplicationBuilder app)
        {
            var appSetting = Singleton<AppSettings>.Instance;
            var _securityConfig = appSetting.Get<SecurityConfig>();

            // use middleware to check token JWT
            app.UseMiddleware<AuthenticationMiddleware>(new TokenOptions
            {
                Path = _securityConfig.PathLogin,
                Expiration = TimeSpan.FromDays(_securityConfig.ExpireDate)
            });
        }
    }
}
