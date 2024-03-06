using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Net.Core.Infrastructure;

namespace Net.WebCore.Infrastructure
{
    public class ErrorHandlerStartup : IAppBuilderStartup
    {
        public int Order => 10;

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;
                await context.Response.WriteAsJsonAsync(new { error = exception?.Message });
            }));
        }
    }
}
