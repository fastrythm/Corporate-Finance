
using CorporateAndFinance.Core.Helper.Structure;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace CorporateAndFinance.Web.Helper
{
    public class HasPermissionAttribute : ActionFilterAttribute
    {
        private UserAppPermissions _permission;
        
        public HasPermissionAttribute(UserAppPermissions permission)
        {
            this._permission = permission;
            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isauthorized = PermissionControl.CheckPermission(_permission);
           // if (!isauthorized)
             //   filterContext.Result = new HttpUnauthorizedResult(); // mark unauthorized

            // Only do something if we are about to give a HttpUnauthorizedResult and we are in AJAX mode.
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
               
                
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
               
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                                    { "action", "restricted" },
                                    { "controller", "home" } 
                                  }
                                  );
               
            }
        }

    }
}