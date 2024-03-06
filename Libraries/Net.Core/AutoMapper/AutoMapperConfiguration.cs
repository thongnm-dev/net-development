using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration? MapperConfig { get; private set; }

        public static IMapper Mapper { get; private set; }

        public static void Init(MapperConfiguration mapperConfiguration)
        {
            MapperConfig = mapperConfiguration;

            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}
