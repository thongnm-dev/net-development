using Net.WebCore.Model;

namespace AppReviewWebApi.Models.Request
{
    public record LoginModel : BaseModel
    {
        public string UserName { get; set; } = "";

        public string Password { get; set; } = "";

        public bool RememberMe { get; set; }
    }
}
