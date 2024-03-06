using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core.Infrastructure
{
    public  interface IAppBuilderStartup
    {
        public void Configure(IApplicationBuilder app);

        int Order { get; }
    }
}
