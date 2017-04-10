using Microsoft.AspNet.Identity;
using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;
using CorporateAndFinance.Service.Implementation;
using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Web;
using System.Web.Mvc;

namespace CorporateAndFinance.Web.Helper
{
    public class PermissionControl
    {

         
        public static bool CheckPermission(UserAppPermissions permission)
        {
            bool result = false;

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return false;

            if (HttpContext.Current.User.IsInRole("Admin"))
              return true;

            if (HttpContext.Current.Session["PermissionList"] == null)
            {
               
                IEnumerable<UserPermission> userPermission =   GetInstance().GetAllPermissionByUserId(HttpContext.Current.User.Identity.GetUserId());

                HttpContext.Current.Session["PermissionList"] = userPermission;

                result = userPermission.Where(x=>x.PermissionID == Convert.ToInt64(permission)).Count() > 0;
            }
            else
            {
                var PermissionList = (IEnumerable<UserPermission>)HttpContext.Current.Session["PermissionList"];
                result = PermissionList.Where(x => x.PermissionID == Convert.ToInt64(permission)).Count() > 0;
            }

            return result;
        }


        public static IUserPermissionManagement GetInstance()
        {
            IDbFactory dbFactory = new DbFactory();
            IUserPermissionRepository permRepor = new UserPermissionRepository(dbFactory);
            IUnitOfWork uow = new UnitOfWork(dbFactory);
            return new UserPermissionManagement(permRepor, uow);
        }
 
    }

     
        
 
}