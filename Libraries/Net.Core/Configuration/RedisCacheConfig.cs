using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core.Configuration
{
    public class RedisCacheConfig : IConfig
    {
        public string ConnectionString { get; set; } = String.Empty;
    }
}
