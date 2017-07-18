using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Implementation;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private static ILog logger = LogManager.GetLogger(typeof(UserController));
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUserPermissionManagement userPermissionManagement;
        private readonly IDepartmentManagement departmentManagement;
        private readonly IUserDepartmentManagement userdepartmentManagement;
        private readonly IRequisitionManagement requisitionManagement;
        private readonly IUserAllocationManagement userAllocationManagement;
        public UserController(IUserPermissionManagement userPermissionManagement, IDepartmentManagement departmentManagement, IUserDepartmentManagement userdepartmentManagement, IRequisitionManagement requisitionManagement, IUserAllocationManagement userAllocationManagement)
        {
            this.userPermissionManagement = userPermissionManagement;
            this.departmentManagement = departmentManagement;
            this.userdepartmentManagement = userdepartmentManagement;
            this.requisitionManagement = requisitionManagement;
            this.userAllocationManagement = userAllocationManagement;

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
            logger.Info("User Listing Page Access");
            if (!PermissionControl.CheckPermission(UserAppPermissions.User_Add))
            {
                logger.Info("Don't have rights to access User Listing Page");
                return RedirectToAction("Restricted", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("UserList")]
        //  [HasPermission(UserAppPermissions.User_View)]
        public ActionResult UserList()
        {
            try
            {

                if (!PermissionControl.CheckPermission(UserAppPermissions.User_Add))
                {
                    return RedirectToAction("Restricted", "Home");
                }
                logger.DebugFormat("Getting User List ");
                var AdminUser = RoleManager.Roles.Where(x => x.Name.Equals(UserRoles.Admin)).SingleOrDefault();
                var jsonObj = UserManager.Users.Where(x => !x.IsDeleted && !(x.Roles.Select(y => y.RoleId).Contains(AdminUser.Id))).ToList();


                logger.DebugFormat("Successfully Retrieve User List Records [{0}]", jsonObj.Count());

                return Json(new
                {
                    aaData = jsonObj
                });
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        [Route("Delete")]
        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                logger.DebugFormat("Deleting User With UserID [{0}] ", id);

                if (!PermissionControl.CheckPermission(UserAppPermissions.User_Delete))
                {
                    logger.Info("Don't have right to delete User record");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                var user = UserManager.FindById(id);
                if (user == null)
                {
                    logger.DebugFormat("User not found With UserID [{0}] ", id);
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
                }

                user.IsDeleted = true;
                UserManager.Update(user);
                logger.Info("User record Successfully Deleted");
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
            }

            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        [Route("AddEdit")]
        public ActionResult AddEdit(string id)
        {
            ViewBag.Title = "Add/Update Users";

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var departments = departmentManagement.GetAllDepartments();
            var requisition = requisitionManagement.GetAllApprovedRequisition();
            var roles = RoleManager.Roles.Where(x => !x.Name.Equals("Admin")).ToList().Select(x => new Core.ViewModel.SelectListItem()
            {
                Text = x.Name,
                Value = x.Id
            });

            var user = new UserVM();
            user.RequisitionList = requisition;
            if (id != "0")
            {
                var appUser = UserManager.FindById(id);
                var userRoles = UserManager.GetRoles(id);
                IEnumerable<UserDepartment> userDepartments = userdepartmentManagement.GetAllUserDepartmentById(appUser.Id);

                user.Id = appUser.Id;
                user.FirstName = appUser.FirstName;
                user.LastName = appUser.LastName;
                user.Email = appUser.Email;
                user.EmployeeNumber = appUser.EmployeeNumber;
                user.Mobile = appUser.Mobile;
                user.Designation = appUser.Designation;
                user.UserPermissions = GetUserPermission(id);
                user.RequisitionID = appUser.RequisitionID;
                if (userDepartments != null)
                {
                    foreach (var userDept in userDepartments)
                    {
                        userDept.Departments = departments;
                        userDept.RolesList = roles;
                    }
                    user.UserDepartments = userDepartments;
                }

            }


            return PartialView("_AddEditUser", user);
        }

        [Route("AddDepartment")]
        public ActionResult AddDepartment()
        {
            UserDepartment userDepartment = new UserDepartment();
            userDepartment.Departments = departmentManagement.GetAllDepartments();
            userDepartment.RolesList = RoleManager.Roles.Where(x => !x.Name.Equals("Admin")).ToList().Select(x => new Core.ViewModel.SelectListItem()
            {
                Text = x.Name,
                Value = x.Id
            });

            return PartialView("_UserDepartment", userDepartment);
        }


        private List<UserAppPermissions> GetUserPermission(string id)
        {
            List<UserAppPermissions> appPerm = new List<UserAppPermissions>();
            var permissions = userPermissionManagement.GetAllPermissionByUserId(id);
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
                if (model.Id != null && model.Id != "0")
                    ModelState.Remove("Password");

                if (model.SelectedDepartment == null)
                {
                    return Json(new { Message = "Atlease one user department must be selected.", MessageClass = MessageClass.Error, Response = false });
                }
                else if (model.SelectedDepartment.Count() == 0)
                {
                    return Json(new { Message = "Atlease one user department must be selected.", MessageClass = MessageClass.Error, Response = false });
                }

                if (ModelState.IsValid)
                {
                    ApplicationUser user = null;
                    if (model.Id != null && model.Id != "0")
                    {
                        logger.DebugFormat("Update  User with FirstName [{0}],  LastName [{1}],  SelectedRoles [{2}],  Designation [{3}] UserID [{4}] ",
               model.FirstName, model.LastName, model.SelectedRoles, model.Designation, model.Id);

                       

                            if (!PermissionControl.CheckPermission(UserAppPermissions.User_Edit))
                        {
                            logger.Info("Don't have rights to update  User ");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        user = UserManager.FindById(model.Id);
                        if (user == null)
                        {
                            logger.DebugFormat("User not found With UserID [{0}] ", model.Id);
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "User"), MessageClass = MessageClass.Error, Response = false });
                        }

                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Email = model.Email;
                        user.UserName = model.Email;
                        if (!string.IsNullOrWhiteSpace(model.Password))
                            user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                        user.Mobile = model.Mobile;
                        user.EmployeeNumber = model.EmployeeNumber;
                        user.Designation = model.Designation;

                        if (model.RequisitionID.HasValue)
                        user.RequisitionID = model.RequisitionID;

                        //   user.DepartmentID = model.DepartmentID;
                        var isUpdate = UserManager.Update(user);
                        var userRoles = UserManager.GetRoles(model.Id);

                        if (isUpdate.Succeeded)
                        {
                            AddUpdateUserDepartments(model, user.Id);
                            UpdateUserPermission(model.UserPermissions, user.Id);

                            if (model.RequisitionID.HasValue)
                            UpdateUserAllocation(user.Id, user.RequisitionID);

                            logger.Info("Successfully Updated User Record");
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPDATE_SUCCESS, "User"), MessageClass = MessageClass.Success, Response = true });
                        }
                        logger.DebugFormat("User Not Updated Due to Error [{0}]", isUpdate.Errors);
                        return Json(new { Message = isUpdate.Errors, MessageClass = MessageClass.Error, Response = false });
                    }
                    else
                    {

                        logger.DebugFormat("Add New  User with FirstName [{0}],  LastName [{1}],  SelectedRoles [{2}],  Designation [{3}] ",
            model.FirstName, model.LastName, model.SelectedRoles, model.Designation);
                        if (!PermissionControl.CheckPermission(UserAppPermissions.User_Add))
                        {
                            logger.Info("Don't have rights to add  User ");
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
                        user.Designation = model.Designation;
                        user.RequisitionID = model.RequisitionID;

                        var isSaved = UserManager.Create(user, model.Password);

                        if (isSaved.Succeeded)
                        {
                            AddUpdateUserDepartments(model, user.Id);
                            UpdateUserPermission(model.UserPermissions, user.Id);
                            UpdateUserAllocation(user.Id, user.RequisitionID);
                            logger.DebugFormat("Successfully Create New User Record with UserID [{0}]", user.Id);
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "User"), MessageClass = MessageClass.Success, Response = true });
                        }
                        logger.DebugFormat("User Not Create Due to Error [{0}]", isSaved.Errors);
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
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        private void UpdateUserAllocation(string userId, long? requisitionID)
        {
            if (requisitionID.HasValue)
            {
                long reqId = Convert.ToInt64(requisitionID);
                var userAllocations = userAllocationManagement.GetUserAllocationsByRequisition(reqId);
                foreach(var allocation in userAllocations)
                {
                    UserAllocation userAllocate = new UserAllocation();
                    userAllocate.RequestedDepartmentID = allocation.RequestedDepartmentID;
                    userAllocate.DepartmentID = allocation.DepartmentID;
                    userAllocate.UserID = userId;
                    userAllocate.RequisitionID = allocation.RequisitionID;
                    userAllocate.StartDate = allocation.StartDate;
                    userAllocate.EndDate = allocation.EndDate;
                    userAllocate.IsActive = allocation.IsActive;
                    userAllocate.Percentage = allocation.Percentage;
                    userAllocate.CreatedBy = new Guid(User.Identity.GetUserId());
                    userAllocate.Status = allocation.Status;

                    userAllocationManagement.Add(userAllocate);
                }

                userAllocationManagement.SaveUserAllocation();
            }
        }

        private void UpdateUserPermission(List<UserAppPermissions> userAppPermissions, string userId)
        {
            try
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
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
            }
        }

        public bool AddUpdateUserDepartments(UserVM model, string userId)
        {
            try
            {

                IEnumerable<UserDepartment> userDepartments = userdepartmentManagement.GetAllUserDepartmentById(userId);
                // Update or inactive those department that are not selected in selected department list.
                foreach (UserDepartment dept in userDepartments)
                {
                    if (!model.SelectedDepartment.Contains(dept.DepartmentID))
                    {
                        dept.ModifiedBy = new Guid(User.Identity.GetUserId());
                        dept.LastModified = DateTime.Now;
                        dept.IsActive = false;
                        dept.IsPrimary = false;
                        dept.EndDate = DateTime.Now;
                        userdepartmentManagement.Update(dept);
                    }
                }

                // Update those department that are exist in selected in selected department list.
                int counter = 0;

                foreach (long deptId in model.SelectedDepartment)
                {
                    var dept = userDepartments.Where(x => x.DepartmentID == deptId).SingleOrDefault();
                    if (dept != null)
                    {

                        dept.ModifiedBy = new Guid(User.Identity.GetUserId());
                        dept.LastModified = DateTime.Now;
                        dept.IsPrimary = counter == 0;
                        string userRole = model.SelectedRoles[counter];
                        dept.RoleID = userRole;
                        userdepartmentManagement.Update(dept);
                    }
                    counter++;
                }

                // Add those department that are exist in selected in selected department list.
                counter = 0;
                foreach (long deptId in model.SelectedDepartment)
                {
                    var dept = userDepartments.Where(x => x.DepartmentID == deptId).SingleOrDefault();
                    if (dept == null)
                    {
                        dept = new UserDepartment();
                        dept.CreatedBy = new Guid(User.Identity.GetUserId());
                        dept.UserID = userId;
                        dept.IsPrimary = counter == 0;
                        dept.DepartmentID = deptId;
                        dept.StartDate = DateTime.Now;
                        dept.EndDate = null;

                        string userRole = model.SelectedRoles[counter];
                        dept.RoleID = userRole;
                        userdepartmentManagement.Add(dept);
                    }
                    counter++;
                }

                userdepartmentManagement.SaveUserDepartment();
                return true;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
}