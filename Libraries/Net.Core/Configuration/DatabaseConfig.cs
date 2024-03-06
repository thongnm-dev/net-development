using Net.Core.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Net.Core.Configuration
{
    public class DatabaseConfig : IConfig
    {
        public string ConnectionString { get; set; } = string.Empty;

        [JsonConverter(typeof(StringEnumConverter))]
        public DatabasePrivder DataProvider { get; set; } = DatabasePrivder.SqlServer;
    }
}