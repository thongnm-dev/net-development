using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core.Infrastructure
{
    public interface IServiceStartup
    {
        int Order { get; }

        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}
