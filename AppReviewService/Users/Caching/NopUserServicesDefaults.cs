using AppReviewDomain.Users;
using Net.Caching;

namespace AppReviewService.Users.Caching
{
    internal static class NopUserServicesDefaults
    {
        public static CacheKey UserByEmail => new("AppReview.user.byemail.{0}");

        public static CacheKey UserByUserName => new("AppReview.user.byusername.{0}");

        public static CacheKey AllCacheKey => new("AppReview.user.all");
    }
}
