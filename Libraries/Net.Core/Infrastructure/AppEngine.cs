using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net.Assembly;
using Net.Core.AutoMapper;

namespace Net.Core.Infrastructure
{
    internal partial class AppEngine : IEngine
    {
        #region Porperties

        public virtual IServiceProvider ServiceProvider { get; protected set; }
        #endregion

        #region Configurate
        /// <summary>
        /// Config serice collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //register engine
            services.AddSingleton<IEngine>(this);

            var typeFinder = Singleton<ITypeFinder>.Instance;

            var startupConfigurations = typeFinder.FindClassesOfType<IServiceStartup>();

            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Select(startup => (IServiceStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup?.Order);

            //configure services
            foreach (var instance in instances)
                instance?.ConfigureServices(services, configuration);

            services.AddSingleton(services);

            //register mapper configurations
            AddAutoMapper();
        }

        /// <summary>
        /// Config applicationbuilder
        /// </summary>
        /// <param name="application"></param>
        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            ServiceProvider = application.ApplicationServices;

            //find startup configurations provided by other assemblies
            var typeFinder = Singleton<ITypeFinder>.Instance;
            var startupConfigurations = typeFinder.FindClassesOfType<IAppBuilderStartup>();

            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Select(startup => (IAppBuilderStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup?.Order);

            //configure request pipeline
            foreach (var instance in instances)
                instance?.Configure(application);
        }
        #endregion

        #region AutoMapper configure

        protected virtual void AddAutoMapper()
        {
            //find mapper configurations provided by other assemblies
            var typeFinder = Singleton<ITypeFinder>.Instance;
            var mapperConfigurations = typeFinder.FindClassesOfType<IOrderedMapperProfile>();

            //create and sort instances of mapper configurations
            var instances = mapperConfigurations
                .Select(mapperConfiguration => (IOrderedMapperProfile)Activator.CreateInstance(mapperConfiguration))
                .OrderBy(mapperConfiguration => mapperConfiguration?.Order);

            //create AutoMapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                Parallel.ForEach(instances, (instance) =>
                {
                    cfg.AddProfile(instance?.GetType());
                });
            });

            //register
            AutoMapperConfiguration.Init(config);
        }
        #endregion

        #region Functionality

        public T Resolve<T>(IServiceScope? scope) where T : class
        {
            return (T)Resolve(typeof(T), scope);
        }

        public object Resolve(Type type, IServiceScope? scope)
        {
            return GetServiceProvider(scope)?.GetService(type);
        }
        #endregion

        #region Self function

        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected IServiceProvider GetServiceProvider(IServiceScope? scope)
        {
            if (scope == null)
            {
                var accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
                var context = accessor?.HttpContext;
                return context?.RequestServices ?? ServiceProvider;
            }
            return scope.ServiceProvider;
        }

        #endregion
    }
}
