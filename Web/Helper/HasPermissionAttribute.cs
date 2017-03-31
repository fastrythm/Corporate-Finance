 
using System.Web.Mvc;

namespace CorporateAndFinance.Web.Helper
{
    public class HasPermissionAttribute : ActionFilterAttribute
    {
        private string _permission;
        private string _page;

        public HasPermissionAttribute(string permission,string page = "")
        {
            this._permission = permission;
            this._page = page;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (!CHECK_IF_USER_OR_ROLE_HAS_PERMISSION(_page,_permission))
            {
                // If this user does not have the required permission then redirect to login page
                //  var url = new UrlHelper(filterContext.RequestContext);
                //   var loginUrl = url.Content("/Home/Restricted");
                //  filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
        }
    }
}