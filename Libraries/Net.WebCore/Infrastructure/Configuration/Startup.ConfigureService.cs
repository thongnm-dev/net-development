using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Net.Assembly;
using Net.Core.Configuration;
using Net.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Net.WebCore.Infrastructure.Configuration
{
    public static class ConfigureService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public static async void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // add accessor to HttpContext
            services.AddHttpContextAccessor();

            // register dependence typefinder
            var typeFinder = new TypeFinder();

            // seperate instance to storage
            Singleton<ITypeFinder>.Instance = typeFinder;

            services.AddSingleton<ITypeFinder>(typeFinder);

            // create instance and store to dictionary as such as singleton
            var configs = new List<IConfig>();

            Parallel.ForEach(typeFinder.FindClassesOfType<IConfig>().Where(type => type != null).ToList(), (configType) =>
            {
                var instance = (IConfig)Activator.CreateInstance(configType);
                if (instance != null)
                {
                    configs.Add(instance);
                    configuration.GetSection(instance.Name).Bind(instance, options => options.BindNonPublicProperties = true);
                }
            });


            // create new instance appsetting
            var appsettings = new AppSettings(configs);
            Singleton<AppSettings>.Instance = appsettings;

            services.AddSingleton(appsettings);

            // Create engine and configure service provider
            var engine = EngineContext.Create();

            // Config all nescessary dependencice for services provider
            engine.ConfigureServices(services, configuration);
        }
    }
}
