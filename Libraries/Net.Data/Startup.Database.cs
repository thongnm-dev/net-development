using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net.Core.Configuration;
using Net.Core.Enum;
using Net.Core.Infrastructure;
using Net.Data.Dapper;
using Net.Data.Repository;
using System.Data;
using System.Data.Common;

namespace Net.Data
{
    public class DbStartup : IServiceStartup
    {
        public int Order => 2000;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = Singleton<AppSettings>.Instance;

            var dbConfig = appSettings.Get<DatabaseConfig>();

            // Database context for EntityFramework
            services.AddDbContext<AppDbContext>(options =>
            {
                if (DatabasePrivder.SqlServer.Equals(dbConfig.DataProvider))
                {
                    options.UseSqlServer(dbConfig.ConnectionString);
                }
            });

            // While Dapper Enable
            if (appSettings.Get<ApiConfig>().EnableDapper)
            {
                services.AddTransient<IDbConnection>(options => options.GetService<AppDbContext>().Database.GetDbConnection());

                // Add dependence Dapper Context
                services.AddScoped<IDapperContext, DapperContext>();
            }

            // config UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Config Repository Query with EntityFramework
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        }
    }
}
