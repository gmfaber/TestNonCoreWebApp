using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestNonCoreWebApp.Helpers;
using System.Web.Security;

namespace TestNonCoreWebApp.CustomActionFilters
{
    public class IPWhitelistFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //HttpCookie authCookie =
            //  filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            //if (authCookie != null)
            //{
            //    FormsAuthenticationTicket authTicket =
            //           FormsAuthentication.Decrypt(authCookie.Value);
            //    var identity = new GenericIdentity(authTicket.Name, "Forms");
            //    var principal = new GenericPrincipal(identity, new string[] { authTicket.UserData });
            //    filterContext.HttpContext.User = principal;
            //}

            var Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var Action = filterContext.ActionDescriptor.ActionName;
            var User = filterContext.HttpContext.User;
            var IP = filterContext.HttpContext.Request.UserHostAddress;


            var isAccessAllowed = IPCacheManager.IPCache.Contains(IP);
            if (!isAccessAllowed)
            {
                //filterContext.Result = new HttpUnauthorizedResult("IP adddress not authorized");
                ViewDataDictionary viewData = new ViewDataDictionary();
                viewData.Add("Message", "IP address does not have sufficient privileges for this operation.");
                filterContext.Result = new ViewResult { ViewName = "Error", ViewData = viewData };
            }
        
    
        }
    }
}