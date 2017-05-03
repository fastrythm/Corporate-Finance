using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Implementation;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web;

namespace CorporateAndFinance.Web.Controllers.Admin
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUserPermissionManagement userPermissionManagement;

        public UserController(IUserPermissionManagement userPermissionManagement)
        {
            this.userPermissionManagement = userPermissionManagement;
        }


        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Users Listing";

            if (!PermissionControl.CheckPermission(UserAppPermissions.User_Add))
            { return RedirectToAction("Restricted", "Home"); }

            return View();
        }

        [HttpPost]
        [Route("UserList")]
      //  [HasPermission(UserAppPermissions.User_View)]
        public ActionResult UserList()
        {

            if (!PermissionControl.CheckPermission(UserAppPermissions.User_Add))
            {
                return RedirectToAction("Restricted", "Home");
            }
             
            var jsonObj  =  UserManager.Users.Where(x=>x.IsDeleted == false).ToList();
          
            return Json(new
            {
                aaData = jsonObj
            });
        }

        [Route("Delete")]
        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.User_Delete))
                {
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                var user = UserManager.FindById(id);
                if (user == null)
                {
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
                }

                user.IsDeleted = true;
                UserManager.Update(user);

                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
            }

            catch (Exception)
            {
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        [Route("AddEdit")]
        public ActionResult AddEdit(string id)
        {
            ViewBag.Title = "Add/Update Users";

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var user = new UserVM();
            if (id != "0")
            {
                var   appUser = UserManager.FindById(id);
                var userRoles = UserManager.GetRoles(id);

                user.Id = appUser.Id;
                user.FirstName = appUser.FirstName;
                user.LastName = appUser.LastName;
                user.Email = appUser.Email;
                user.EmployeeNumber = appUser.EmployeeNumber;
                user.Mobile = appUser.Mobile;
                user.Department = appUser.Department;
                user.Designation = appUser.Designation;
                user.UserPermissions = GetUserPermission(id);
                user.RolesList = RoleManager.Roles.ToList().Select(x => new Core.ViewModel.SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                });
            }
            else
            {
                user.RolesList = user.RolesList = RoleManager.Roles.ToList().Select(x => new Core.ViewModel.SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Name
                });
            }

            return PartialView("_AddEditUser", user);
        }

        private List<UserAppPermissions> GetUserPermission(string id)
        {
            List<UserAppPermissions> appPerm = new List<UserAppPermissions>();
            var permissions =  userPermissionManagement.GetAllPermissionByUserId(id);
            foreach (var userPerm in permissions)
            {
                UserAppPermissions perm = (UserAppPermissions)userPerm.PermissionID;
                appPerm.Add(perm);
            }

            return appPerm;
        }

        [Route("AddEdit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddEdit(UserVM model)
        {
            try
            {
                if(model.Id != null && model.Id != "0")
                    ModelState.Remove("Password");
                if (ModelState.IsValid)
                {
                    ApplicationUser user = null;
                    if (model.Id != null && model.Id != "0")
                    {

                        if (!PermissionControl.CheckPermission(UserAppPermissions.User_Edit))
                        {
                           return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        user = UserManager.FindById(model.Id);
                        if(user == null)
                        {
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "User"), MessageClass = MessageClass.Error, Response = false });
                        }

                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Email = model.Email;
                        user.UserName = model.Email;
                        if(!string.IsNullOrWhiteSpace(model.Password))
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                        user.Mobile = model.Mobile;
                        user.EmployeeNumber = model.EmployeeNumber;
                        user.Department = model.Department;
                        user.Designation = model.Designation;
                        var isUpdate = UserManager.Update(user);
                        var userRoles = UserManager.GetRoles(model.Id);
                        
                        if (isUpdate.Succeeded)
                        {
                            UserManager.RemoveFromRoles(user.Id, userRoles.ToArray<string>());
                            if (model.SelectedRoles != null)
                            {
                                var result = UserManager.AddToRoles(model.Id, model.SelectedRoles.ToArray<string>());
                            }

                            UpdateUserPermission(model.UserPermissions, user.Id);
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPDATE_SUCCESS, "User"), MessageClass = MessageClass.Success, Response = true });
                        }

                        return Json(new { Message = isUpdate.Errors, MessageClass = MessageClass.Error, Response = false });
                    }
                    else
                    {

                        if (!PermissionControl.CheckPermission(UserAppPermissions.User_Add))
                        {
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        user = new ApplicationUser();
                      // model.Id = Guid.NewGuid().ToString();
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Email = model.Email;
                        user.UserName = model.Email;
                        user.Mobile = model.Mobile;
                        user.EmployeeNumber = model.EmployeeNumber;
                        user.Department = model.Department;
                        user.Designation = model.Designation;

                       var isSaved = UserManager.Create(user, model.Password);

                        if (isSaved.Succeeded)
                        {
                            if (model.SelectedRoles != null && model.SelectedRoles.Count() > 0)
                            {
                                var result = UserManager.AddToRoles(user.Id, model.SelectedRoles.ToArray<string>());
                            }

                            UpdateUserPermission(model.UserPermissions, user.Id);
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "User"), MessageClass = MessageClass.Success, Response = true });
                        }

                        return Json(new { Message = isSaved.Errors, MessageClass = MessageClass.Error, Response = false });
                    }
                 
                }
                else
                {
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "User"), MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }


        private void UpdateUserPermission(List<UserAppPermissions> userAppPermissions,string userId)
        {

            userPermissionManagement.DeleteAllByUserId(userId);
            if (userAppPermissions != null)
            {
                foreach (UserAppPermissions appPermission in userAppPermissions)
                {
                    UserPermission userPerm = new UserPermission() { UserID = userId, PermissionID = ((int)appPermission) };
                    userPermissionManagement.Add(userPerm);
                }
                userPermissionManagement.SaveUserPermissions();
            }
           
        }
    }
}