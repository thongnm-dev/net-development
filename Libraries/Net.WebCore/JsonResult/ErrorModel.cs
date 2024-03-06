using Newtonsoft.Json;

namespace Net.WebCore.JsonResult
{
    public record ErrorModel
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public ErrorModel(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
