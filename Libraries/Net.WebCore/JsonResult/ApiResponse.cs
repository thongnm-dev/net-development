using Newtonsoft.Json;

namespace Net.WebCore.JsonResult
{
    public record ApiResponse
    {
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("data")]
        public dynamic? Data { get; set; }

        [JsonProperty("error")]
        public dynamic? Error { get; set; }
    }
}
