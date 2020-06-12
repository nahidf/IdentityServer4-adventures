using System;
using System.Web;
using System.Web.Mvc;

namespace Net4MvcClient.Infrastructure
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            //Intercept results where person is authenticated but still doesn't have permissions
            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("http://localhost:49816/Home/Unauthorized");
                return;
            }

            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}