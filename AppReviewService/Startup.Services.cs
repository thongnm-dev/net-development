using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net.Core.Infrastructure;
using AppReviewService.Users;
using AppReviewService.Authentication.Impl;
using Net.Core.Authentication;

namespace AppReviewService
{
    public class ServiceStartup : IServiceStartup
    {
        public int Order => 3000;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserservice, Userservice>();
            services.AddScoped<ITokenProviderService, TokenProviderService>();
        }
    }
}
