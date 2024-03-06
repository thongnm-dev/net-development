using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core.Configuration
{
    public interface IConfig
    {
        string Name => GetType().Name;

        int GetOrder() => 1;
    }
}
