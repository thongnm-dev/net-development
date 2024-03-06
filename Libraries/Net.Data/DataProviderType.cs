using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Net.Data
{
    public enum DataProviderType
    {

        [EnumMember(Value = "")]
        Unknow,

        [EnumMember(Value = "sqlserver")]
        SqlServer,

        [EnumMember(Value = "pgsql")]
        PostgreSQL

    }
}
