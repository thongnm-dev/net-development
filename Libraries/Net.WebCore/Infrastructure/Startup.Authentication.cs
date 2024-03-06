using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Net.Core.Configuration;
using Net.Core.Infrastructure;
using System.Net;
using System.Text;

namespace Net.WebCore.Infrastructure
{
    public class AuthenticationStartup : IServiceStartup
    {
        public int Order => 200;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // add authentication
            var appSetting = Singleton<AppSettings>.Instance;
            var _securityConfig = appSetting.Get<SecurityConfig>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.SaveToken = true;
                    bearer.RequireHttpsMetadata = false;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityConfig.SecurityKey)),
                        ClockSkew = TimeSpan.FromDays(_securityConfig.ExpireDate)
                    };

                    bearer.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            if (c.Exception is SecurityTokenExpiredException)
                            {
                                c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                c.Response.ContentType = "application/json";
                                return c.Response.WriteAsync("Your Token Has Expired");
                            }
                            else
                            {
                                c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                c.Response.ContentType = "application/json";
                                return c.Response.WriteAsync("An unhandled error has occurred.");
                            }
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";
                                return context.Response.WriteAsync("You are not Authorized.");
                            }

                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsync("You are not authorized to access this resource.");
                        },
                    };
                });
        }
    }

    public class AuthenticationStartupApp : IAppBuilderStartup
    {
        public int Order => 200;

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
