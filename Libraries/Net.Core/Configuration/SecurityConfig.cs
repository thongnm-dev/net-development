namespace Net.Core.Configuration
{
    public class SecurityConfig : IConfig
    {
        public string CorsPolicyKey { get; set; } = "AppReviewService";

        public string SecurityKey { get; set; } = "Token@Security@1029384756!@#";

        public int ExpireDate { get; set; } = 1;

        public string PathLogin { get; set; } = "/auth/signin";

        public string Algorithm { get; set; } = "HmacSha512";
    }
}
