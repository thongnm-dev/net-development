using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Net.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.WebCore.Infrastructure.Configuration
{
    public static class ConfigureApplication
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            var rewriteOptions = new RewriteOptions().AddRewrite("api/token", "/token", true);
            application.UseRewriter(rewriteOptions);

            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        public static void StartEngine(this IApplicationBuilder application)
        {

        }
    }
}
