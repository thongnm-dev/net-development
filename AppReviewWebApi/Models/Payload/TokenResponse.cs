using Newtonsoft.Json;

namespace AppReviewWebApi.Models.Payload
{
    public class TokenResponse
    {
        [JsonProperty("access_token", Required = Required.Always)]
        public string AccessToken { get; init; } = "";

        [JsonProperty("token_type", Required = Required.Always)]
        public string TokenType { get; init; } = "Bearer";

        [JsonProperty("created_at_utc")]
        public DateTime CreatedAtUtc { get; init; }

        [JsonProperty("expires_at_utc")]
        public DateTime ExpiresAtUtc { get; init; }

        [JsonProperty("username")]
        public string Username { get; init; } = "";

        [JsonProperty("user_id")]
        public int UserId { get; init; }

        [JsonProperty("user_guid")]
        public Guid UserGuid { get; init; }
    }
}
