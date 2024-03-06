namespace Net.WebCore.Infrastructure.Middleware
{
    public class TokenOptions
    {
        public string Path { get; set; } = "";

        public TimeSpan Expiration { get; set; }

    }
}
