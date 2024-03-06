using Microsoft.AspNetCore.Builder;
using Net.Core.Infrastructure;

namespace Net.WebCore.Infrastructure
{
    public class EndpointStartup : IAppBuilderStartup
    {
        public int Order => 1000;

        public void Configure(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
