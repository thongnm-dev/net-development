using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Net.Core.Authentication;
using System.Net;

namespace Net.WebCore.Authorization.Attributes
{
    class AuthorizePermissionFilter : IAsyncAuthorizationFilter
    {
        #region Variable
        private readonly string permission = "";
        private readonly bool ignoreFilter = false;

        private readonly IAuthenticationService authenticationService;

        #endregion

        #region Constructor

        public AuthorizePermissionFilter(string permissionSystemName, bool ignore = false, IAuthenticationService authenticationService)
        {
            permission = permissionSystemName;
            ignoreFilter = ignore;
            this.authenticationService = authenticationService;
        }

        #endregion

        #region Functionality

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await AuthorizePermissionAsync(context);
        }

        #endregion

        #region Self Func

        private async Task AuthorizePermissionAsync(AuthorizationFilterContext context)
        {
            var actionFilter = context.ActionDescriptor.FilterDescriptors
                            .Where(filter => FilterScope.Action.Equals(filter.Scope))
                            .Select(filter => filter.Filter)
                            .OfType<AuthorizePermissionAttribute>()
                            .Where(auth => auth.PermissionName.Equals(permission))
                            .FirstOrDefault();

            if (actionFilter is not null && actionFilter.IgnoreFilter)
                return;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await context.HttpContext.Response.WriteAsJsonAsync(new { message = "Request Forbiden" });
        }
        #endregion
    }
}
