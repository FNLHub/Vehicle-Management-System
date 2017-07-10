using DotNetCasClient.Security;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Voyager2._0.Helpers
{

    public class CosAuth : AuthorizeAttribute
    {
        private const string _tokenCookieName = "Voyager";

        /// <summary>
        /// Checks if the user is Authorized or not.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>bool</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");

            // Make sure the user is authenticated.
            var userIsAuthenticated = httpContext.User.Identity.IsAuthenticated;
            return userIsAuthenticated;
        }
    }
}