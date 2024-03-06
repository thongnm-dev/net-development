using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.WebCore.Authorization.Attributes
{
    public sealed class AuthorizePermissionAttribute : TypeFilterAttribute
    {
        #region Properties

        public string PermissionName { get; }

        public bool IgnoreFilter { get; }

        #endregion

        #region Ctor
        public AuthorizePermissionAttribute(string PermissionName, bool ignore = false) : base(typeof(AuthorizePermissionFilter))
        {
            this.PermissionName = PermissionName;
            IgnoreFilter = ignore;
            Arguments = new object[] { PermissionName, ignore };
        }
        #endregion
    }
}
