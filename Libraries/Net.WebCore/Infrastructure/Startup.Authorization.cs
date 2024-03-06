using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net.WebCore.Authorization.Policies;
using Net.WebCore.Authorization.Requirements;
using Net.Core.Infrastructure;

namespace Net.WebCore.Infrastructure
{
    public class AuthorizationStartup : IServiceStartup
    {
        public int Order => 300;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add Authorization with JWT Token
            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, (policy) =>
                {
                    policy.Requirements.Add(new AuthorizationSchemeRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, ValidSchemeAuthorizationPolicy>();
        }
    }

    public class AuthorizationStartupApp : IAppBuilderStartup
    {
        public int Order => 300;

        public void Configure(IApplicationBuilder app)
        {
            // use Authorization
            app.UseAuthorization();
        }
    }
}
