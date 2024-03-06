using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core.AutoMapper
{
    public interface IOrderedMapperProfile
    {
        int Order { get; }
    }
}
