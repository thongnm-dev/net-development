using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Net.WebCore.Authorization.Requirements;

namespace Net.WebCore.Authorization.Policies
{
    public class ValidSchemeAuthorizationPolicy : AuthorizationHandler<AuthorizationSchemeRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidSchemeAuthorizationPolicy(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
              AuthorizationHandlerContext context
            , AuthorizationSchemeRequirement requirement)
        {
            if (requirement.IsValid(_httpContextAccessor.HttpContext?.Request.Headers))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
