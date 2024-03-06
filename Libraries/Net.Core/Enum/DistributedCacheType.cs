using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core.Enum
{
    public enum DistributedCacheType
    {
        [EnumMember(Value ="memory")]
        Memory,

        [EnumMember(Value = "redis")]
        Redis
    }
}
