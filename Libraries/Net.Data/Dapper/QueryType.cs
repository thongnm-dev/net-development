using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Net.Data.Dapper
{
    public enum QueryType
    {
        /// <summary>
        /// Execute SQL with command Text
        /// </summary>
        [EnumMember(Value = "sql")]
        NATIVE,

        /// <summary>
        /// Execute with store procedure
        /// </summary>
        [EnumMember(Value = "proc")]
        PROCEDURE
    }
}
