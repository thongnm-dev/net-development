using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net.Caching;
using Net.Core.Configuration;
using Net.Core.Enum;
using Net.Core.Infrastructure;

namespace Net.WebCore.Infrastructure
{
    public class Startup : IServiceStartup
    {
        public int Order => 600;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = Singleton<AppSettings>.Instance;
            var apiConfig = appSettings.Get<ApiConfig>();

            if (apiConfig.EnableCache)
            {
                switch (apiConfig.DistributedCacheType)
                {
                    case DistributedCacheType.Memory:
                        services.AddDistributedMemoryCache();
                        services.AddScoped<IStaticCacheManager, MemoryCacheService>();
                        break;
                    case DistributedCacheType.Redis:
                        var redisCacheConfig = appSettings.Get<RedisCacheConfig>();
                        services.AddStackExchangeRedisCache(options =>
                        {
                            options.Configuration = redisCacheConfig.ConnectionString;
                        });

                        services.AddScoped<IStaticCacheManager, RedisCacheService>();
                        break;
                }
            }
        }
    }
}
