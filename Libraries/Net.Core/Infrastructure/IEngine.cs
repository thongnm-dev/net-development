using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Infrastructure
{
    public interface IEngine
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        public void ConfigureRequestPipeline(IApplicationBuilder application);

        T Resolve<T>(IServiceScope? scope = null) where T : class;
    }
}
