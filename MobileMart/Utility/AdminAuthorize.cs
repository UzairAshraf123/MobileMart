using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobileMart.App.Admin.Utility
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        public AdminAuthorize(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"].ToString();
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //if user is authenticated but not authorized
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Admin", action = "AdminLogin", returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) }));
            }
            else
            {
                //if user is not authenticated
                base.HandleUnauthorizedRequest(filterContext);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Admin", action = "AdminLogin", returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) }));
            }


            //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = "AdminLogin" }));
        }

    }

    public class ShopAuthorize : AuthorizeAttribute
    {
        public ShopAuthorize(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"].ToString();
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //if user is authenticated but not authorized
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "ShopOwner", action = "Login", returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) }));
            }
            else
            {
                //if user is not authenticated
                base.HandleUnauthorizedRequest(filterContext);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "ShopOwner", action = "Login", returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) }));
            }


            //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = "AdminLogin" }));
        }

    }
    public class CustomerAuthorize : AuthorizeAttribute
    {
        public CustomerAuthorize(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"].ToString();
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //if user is authenticated but not authorized
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "RegisterAndLogin", returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) }));
            }
            else
            {
                //if user is not authenticated
                base.HandleUnauthorizedRequest(filterContext);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "RegisterAndLogin", returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) }));
            }


            //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = "AdminLogin" }));
        }

    }
}
