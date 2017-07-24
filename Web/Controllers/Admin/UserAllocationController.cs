using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Helper;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Web;
using RazorEngine.Templating;
using RazorEngine;
using CorporateAndFinance.Service.Implementation;

namespace CorporateAndFinance.Web.Controllers.Admin
{
    public class UserAllocationController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(RequisitionController));
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly IRequisitionManagement requisitionManagement;
        private readonly IUserAllocationManagement userAllocationManagement;
        private readonly IRequisitionApprovalManagement requisitionApprovalManagement;
        private readonly IUserManagement userManagement;
     
        private readonly IUserDepartmentManagement userdepartmentManagement;
        private readonly IDepartmentManagement departmentManagement;

        public UserAllocationController(IRequisitionManagement requisitionManagement, IUserAllocationManagement userAllocationManagement, IRequisitionApprovalManagement requisitionApprovalManagement, IUserManagement userManagement,  IUserDepartmentManagement userdepartmentManagement, IDepartmentManagement departmentManagement)
        {
            this.requisitionManagement = requisitionManagement;
            this.userAllocationManagement = userAllocationManagement;
            this.requisitionApprovalManagement = requisitionApprovalManagement;
            this.userManagement = userManagement;
         
            this.userdepartmentManagement = userdepartmentManagement;
            this.departmentManagement = departmentManagement;
        }


        public UserAllocationController(ApplicationSignInManager signInManager, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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
            logger.Info("User Allocation Page Access");
            ViewBag.Title = "User Allocation";
            if (!PermissionControl.CheckPermission(UserAppPermissions.UserAllocation_View))
            {
                logger.Info("Don't have rights to access User Allocation Page");
                return RedirectToAction("Restricted", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("UserAllocationList")]
        public ActionResult UserAllocationList(DataTablesViewModel param, string fromDate, string toDate,string type,string allocationType)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.UserAllocation_View))
                { return RedirectToAction("Restricted", "Home"); }

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(fromDate))
                    frdate = DateTime.Parse(fromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(toDate))
                    tdate = DateTime.Parse(toDate);


                logger.DebugFormat("Getting User Allocation List with From Date [{0}] and To Date [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());

                IEnumerable<UserDepartment> userDepartments;
                bool IsAdmin = false;
                if (User.IsInRole("Admin"))
                {
                    IsAdmin = true;
                }

                var appUser = UserManager.FindById(User.Identity.GetUserId());
                userDepartments = userdepartmentManagement.GetAllUserDepartmentById(appUser.Id);


                UserAllocationVM allocation = new UserAllocationVM();
                allocation.DTObject = param;
                var list = userAllocationManagement.GetAllUserAllocationByParam(allocation, frdate, tdate, userDepartments, IsAdmin, type, allocationType);
                logger.DebugFormat("Successfully Retrieve  User Allocation List Records [{2}] with From Date [{0}] and To Date [1]", frdate.ToShortDateString(), tdate.ToShortDateString(), list.Count());

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = list.Select(i => i.DTObject.TotalRecordsCount).FirstOrDefault(),
                    iTotalDisplayRecords = list.Select(i => i.DTObject.TotalRecordsCount).FirstOrDefault(), // Filtered Count
                    aaData = list
                });

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        [Route("Operation")]
        [HttpPost]
        public JsonResult Operation(string id_type,string comments)
        {
            try
            {
                var values = id_type.Split('_');
                logger.DebugFormat(" User Allocation updated with  With ID [{0}] and action [{1}] ", values[0], values[1]);

                if (!PermissionControl.CheckPermission(UserAppPermissions.UserAllocation_Approve_Reject))
                {
                    logger.Info("Don't have right to Approve/Reject User Allocation record");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                UserAllocation req = userAllocationManagement.GetUserAllocation(Convert.ToInt64(values[0]));
                if(req == null)
                {
                    return Json(new { Message = "User Allocation not found", MessageClass = MessageClass.Error, Response = false });
                }

                string selectedType = Convert.ToInt32(values[1]) == 1 ? RequestStatus.Approved : RequestStatus.Rejected;
                if (req.UserID != null && selectedType == RequestStatus.Rejected) // If user is not null and operation is rejected
                {
                    req.Comments = comments;
                    DeActiveUserGroupAllocation(req);
                }
                if (selectedType == RequestStatus.Approved)
                {
                    req.IsActive = true;
                    req.Status = RequestStatus.PartialApproved;
                }
                else
                {
                    req.IsActive = true;
                    req.Status = RequestStatus.Rejected;
                }
              
                req.ModifiedBy =new Guid( User.Identity.GetUserId());
                req.Comments = comments;
                     

                if (userAllocationManagement.Update(req))
                {
                    userAllocationManagement.SaveUserAllocation();
                    logger.InfoFormat("User Allocation record Successfully {0}", selectedType);

                    if (req.RequisitionID.HasValue)
                    UpdateRequisitionStatus(req, selectedType);

                    if (req.UserID != null && selectedType == RequestStatus.Approved) // If user is not null and operation is Approved
                    {
                        DeActivatedPreviousAllocation(req, selectedType);
                    }
                    string msg = string.Format("User Allocation record Successfully {0}", selectedType);
                    return Json(new { Message = msg , MessageClass = MessageClass.Success, Response = true });
                }
                else
                {
                    string msg = string.Format("User Allocation record not {0}", selectedType);
                    logger.InfoFormat("User Allocation record not {0}", selectedType);
                    return Json(new { Message = msg, MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        private void DeActivatedPreviousAllocation(UserAllocation req,string selectedType)
        {
            if (req.UserID != null && !req.RequisitionID.HasValue && selectedType == RequestStatus.Approved)
            {
                var allocation = userAllocationManagement.GetUserAllocationByGroupNumber(req.GroupNumber);
                var deActiveAllocation = allocation.Where(x => !x.IsActive).ToList();

                if (deActiveAllocation != null && deActiveAllocation.Count == 0)
                {
                    var activeUserAllocation = userAllocationManagement.GetActiveUserAllocationExceptGroupNumber(req.UserID, req.GroupNumber);
                    foreach(var allocate in activeUserAllocation)
                    {
                        allocate.IsActive = false;
                        allocate.Status = RequestStatus.Closed;
                        allocate.ModifiedBy = new Guid(User.Identity.GetUserId());
                        userAllocationManagement.Update(allocate);
                    }


                    var groupAllocation = userAllocationManagement.GetUserAllocationByGroupNumber(req.GroupNumber);
                    foreach (var allocate in groupAllocation)
                    {
                        allocate.IsActive = true;
                        allocate.Status = RequestStatus.Approved;
                        allocate.ModifiedBy = new Guid(User.Identity.GetUserId());
                        userAllocationManagement.Update(allocate);
                    }

                    userAllocationManagement.SaveUserAllocation();

                    SendUserAllocationEmailsToRequestCreator(req.GroupNumber, RequestStatus.Approved, req.Comments);
                }

            }
            
        }

        private void UpdateRequisitionStatus(UserAllocation req,string  type)
        {
            var requisition = requisitionManagement.GetRequisition(Convert.ToInt64(req.RequisitionID));

            if (req.User == null && req.RequisitionID.HasValue && type == RequestStatus.Approved)
            {
                var allocation = userAllocationManagement.GetUserAllocationsByRequisition(Convert.ToInt64(req.RequisitionID));
                var deActiveAllocation = allocation.Where(x => !x.IsActive).ToList();

                if(deActiveAllocation != null  && deActiveAllocation.Count == 0)
                {
                    requisition.Status = RequisitionStatus.Level1_Approved;
                    requisitionManagement.Update(requisition);
                    requisitionManagement.SaveRequisition();

                   
                    foreach (var allocate in allocation)
                    {
                        allocate.IsActive = true;
                        allocate.Status = RequestStatus.Approved;
                        allocate.ModifiedBy = new Guid(User.Identity.GetUserId());
                        userAllocationManagement.Update(allocate);
                    }
                    userAllocationManagement.SaveUserAllocation();

                    RequisitionStatusEmailToUser(Convert.ToInt64(req.RequisitionID), RequisitionStatus.Level1_Approved, req.Comments);
                    SendRequisitionEmailsToLevel2Department(Convert.ToInt64(req.RequisitionID));
                }

            }
            else if (req.User == null && req.RequisitionID.HasValue)
            {
                requisition.Status = RequisitionStatus.Level1_Rejected;
                requisitionManagement.Update(requisition);
                requisitionManagement.SaveRequisition();

                var allocation = userAllocationManagement.GetUserAllocationsByRequisition(Convert.ToInt64(req.RequisitionID));
                foreach (var allocate in allocation)
                {
                    allocate.IsActive = false;
                    allocate.Status = RequestStatus.Rejected;
                    allocate.ModifiedBy = new Guid(User.Identity.GetUserId());
                    userAllocationManagement.Update(allocate);
                }
                userAllocationManagement.SaveUserAllocation();

                RequisitionStatusEmailToUser(Convert.ToInt64(req.RequisitionID), RequisitionStatus.Level1_Rejected, req.Comments);
            }
        }


        private void RequisitionStatusEmailToUser(long requisitionId, string status, string comments)
        {
            try
            {
                logger.DebugFormat("Sending Requisition Email to request creator by RequisitionID [{0}]", requisitionId);
                RequisitionVM req = requisitionManagement.GetRequisitionCompleteInfoById(requisitionId);

                if (req == null)
                {
                    logger.DebugFormat("No Requisition Found With ID [{0}]", requisitionId);
                    return;
                }
                string styleSheet = System.IO.File.ReadAllText(Server.MapPath("~/Themes/finance-1/css/emailstyle.css"));
                req.StyleSheet = styleSheet;
                req.Status = status;
                req.Comments = comments;

                var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/EmailTemplates/RequisitionEmailToRequestCreator.cshtml"));
                string template = System.IO.File.ReadAllText(viewsPath);

                string uniqueNumber = Guid.NewGuid().ToString();
                string body = Engine.Razor.RunCompile(template, string.Format("RequisitionEmail_{0}", uniqueNumber), typeof(UserTaskEmailVM), req);
                var role = RoleManager.Roles.Where(x => x.Name.Equals(UserRoles.Manager)).FirstOrDefault();

                var user = UserManager.FindById(req.CreatedBy.ToString());
                if (user != null)
                {
                    ICommunicationManagement comManagement = new CommunicationManagement();
                    comManagement.Subject = string.Format("Requisition Request #. {0}  Status - {1} ", Utility.FormatedId("UR-", req.RequisitionID.ToString()), req.Status);
                    comManagement.Body = body;
                    comManagement.Recipient = user.Email;
                    comManagement.HeaderImage = Server.MapPath("~/Themes/finance-1/img/logo.png");
                    Async.Do(() => comManagement.SendEmail());
                    logger.DebugFormat("Email Successfully Send");
                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
            }
        }

        private void SendRequisitionEmailsToLevel2Department(long requisitionId)
        {
            try
            {
                logger.DebugFormat("Sending Requisition Email to level 2 departments by RequisitionID [{0}]", requisitionId);
                RequisitionVM req = requisitionManagement.GetRequisitionCompleteInfoById(requisitionId);

                if (req == null)
                {
                    logger.DebugFormat("No Requisition Found With ID [{0}]", requisitionId);
                    return;
                }
                string styleSheet = System.IO.File.ReadAllText(Server.MapPath("~/Themes/finance-1/css/emailstyle.css"));
                req.StyleSheet = styleSheet;

                var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/EmailTemplates/RequisitionEmailToDepartments.cshtml"));
                string template = System.IO.File.ReadAllText(viewsPath);

                string uniqueNumber = Guid.NewGuid().ToString();
                string body = Engine.Razor.RunCompile(template, string.Format("RequisitionEmail_{0}", uniqueNumber), typeof(UserTaskEmailVM), req);
                var role = RoleManager.Roles.Where(x => x.Name.Equals(UserRoles.Manager)).FirstOrDefault();


                var requisitionApprovalDepartments = requisitionApprovalManagement.GetAllRequisitionApprovalByRequisition(req.RequisitionID);

                if (requisitionApprovalDepartments != null && requisitionApprovalDepartments.Count() > 0)
                {
                    foreach (var approvalDept in requisitionApprovalDepartments)
                    {
                        var departManagers = userManagement.GetAllUsersByRoleAndDepartment(role.Id, approvalDept.DepartmentID);

                        if (departManagers != null && departManagers.Count() > 0)
                        {
                            foreach (var user in departManagers)
                            {
                                ICommunicationManagement comManagement = new CommunicationManagement();
                                comManagement.Subject = string.Format("New Requisition Request #. {0} - {1} ", Utility.FormatedId("UR-", req.RequisitionID.ToString()), req.JobTitle);
                                comManagement.Body = body;
                                comManagement.Recipient = user.Email;
                                comManagement.HeaderImage = Server.MapPath("~/Themes/finance-1/img/logo.png");
                                Async.Do(() => comManagement.SendEmail());
                                logger.DebugFormat("Email Successfully Send");
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
            }
        }


        private void SendUserAllocationEmailsToLevel1Departments(long groupNumber)
        {
            try
            {
                logger.DebugFormat("Sending User Allocation Email to departments by GroupNumber [{0}]", groupNumber);

                var userAllocation = userAllocationManagement.GetUserAllocationsByGroupNumber(groupNumber);

                if (userAllocation == null)
                {
                    logger.DebugFormat("No Allocation Found With Group Number  [{0}]", groupNumber);
                    return;
                }
                else if(userAllocation.Count() == 0)
                {
                    logger.DebugFormat("No Allocation Found With Group Number  [{0}]", groupNumber);
                    return;
                }
                UserAllocationEmailVM emailVM = new UserAllocationEmailVM();

                string styleSheet = System.IO.File.ReadAllText(Server.MapPath("~/Themes/finance-1/css/emailstyle.css"));
                emailVM.StyleSheet = styleSheet;
                emailVM.UserAllocations = userAllocation;

                var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/EmailTemplates/UserAllocationEmailToDepartments.cshtml"));
                string template = System.IO.File.ReadAllText(viewsPath);

                string uniqueNumber = Guid.NewGuid().ToString();
                string body = Engine.Razor.RunCompile(template, string.Format("UserAllocationEmail_{0}", uniqueNumber), typeof(UserAllocationEmailVM), emailVM);
                var role = RoleManager.Roles.Where(x => x.Name.Equals(UserRoles.Manager)).FirstOrDefault();

                if (userAllocation != null && userAllocation.Count() > 0)
                {
                    foreach (var allocation in userAllocation)
                    {
                        var departManagers = userManagement.GetAllUsersByRoleAndDepartment(role.Id, allocation.DepartmentID);

                        if (departManagers != null && departManagers.Count() > 0)
                        {
                            foreach (var user in departManagers)
                            {
                                ICommunicationManagement comManagement = new CommunicationManagement();
                                comManagement.Subject = string.Format("User Re Allocation Request ");
                                comManagement.Body = body;
                                comManagement.Recipient = user.Email;
                                comManagement.HeaderImage = Server.MapPath("~/Themes/finance-1/img/logo.png");
                                Async.Do(() => comManagement.SendEmail());
                                logger.DebugFormat("Email Successfully Send");
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
            }
        }


        private void SendUserAllocationEmailsToRequestCreator(long groupNumber,string status,string comments)
        {
            try
            {
                logger.DebugFormat("Sending User Allocation Email to request creator by GroupNumber [{0}]", groupNumber);

                var userAllocation = userAllocationManagement.GetUserAllocationsByGroupNumber(groupNumber);

                if (userAllocation == null)
                {
                    logger.DebugFormat("No Allocation Found With Group Number  [{0}]", groupNumber);
                    return;
                }
                else if (userAllocation.Count() == 0)
                {
                    logger.DebugFormat("No Allocation Found With Group Number  [{0}]", groupNumber);
                    return;
                }
                UserAllocationEmailVM emailVM = new UserAllocationEmailVM();

                string styleSheet = System.IO.File.ReadAllText(Server.MapPath("~/Themes/finance-1/css/emailstyle.css"));
                emailVM.StyleSheet = styleSheet;
                emailVM.UserAllocations = userAllocation;
                emailVM.Status = status;
                emailVM.Comments = comments;

                var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/EmailTemplates/UserAllocationEmailToRequestCreator.cshtml"));
                string template = System.IO.File.ReadAllText(viewsPath);

                string uniqueNumber = Guid.NewGuid().ToString();
                string body = Engine.Razor.RunCompile(template, string.Format("UserAllocationEmail_{0}", uniqueNumber), typeof(UserAllocationEmailVM), emailVM);
               
                var user = UserManager.FindById(userAllocation[0].CreatedBy.ToString());
                if (user != null)
                {
                    ICommunicationManagement comManagement = new CommunicationManagement();
                    comManagement.Subject = string.Format("User Re-Allocation Request Status");
                    comManagement.Body = body;
                    comManagement.Recipient = user.Email;
                    comManagement.HeaderImage = Server.MapPath("~/Themes/finance-1/img/logo.png");
                    Async.Do(() => comManagement.SendEmail());
                    logger.DebugFormat("Email Successfully Send");
                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
            }
        }


        private void DeActiveUserGroupAllocation(UserAllocation req)
        {
            var allocation = userAllocationManagement.GetUserAllocationByGroupNumber(req.GroupNumber);
            foreach(var allocate in allocation)
            {
                allocate.IsActive = false;
                allocate.ModifiedBy = new Guid(User.Identity.GetUserId());
                allocate.Status = RequestStatus.Rejected;
                allocate.Comments = req.Comments;
                userAllocationManagement.Update(allocate);
            }

            SendUserAllocationEmailsToRequestCreator(req.GroupNumber, RequestStatus.Rejected, req.Comments);
        }


        [Route("AddEdit")]
        public ActionResult AddEdit()
        {
            ViewBag.Title = "Add/Update User Allocation";
            ViewBag.IsAddEdit = true;

            if (!PermissionControl.CheckPermission(UserAppPermissions.UserAllocation_Add) && !PermissionControl.CheckPermission(UserAppPermissions.UserAllocation_Edit))
            {
                logger.Info("Don't have rights to add new Requisition");
                return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
            }
            var AdminUser = RoleManager.Roles.Where(x => x.Name.Equals(UserRoles.Admin)).SingleOrDefault();
            var userList = UserManager.Users.Where(x => !x.IsDeleted && !(x.Roles.Select(y => y.RoleId).Contains(AdminUser.Id))).ToList();
           
            UserReAllocationVM allocationVM = new UserReAllocationVM();
            allocationVM.UserList = userList;
            
            return PartialView("_AddUpdateAllocation", allocationVM);
        }


        [Route("View")]
        public ActionResult View(string userId,long groupNumber)
        {
            ViewBag.Title = "Add/Update User Allocation";
            ViewBag.IsAddEdit = false;

            if (!PermissionControl.CheckPermission(UserAppPermissions.UserAllocation_Add) && !PermissionControl.CheckPermission(UserAppPermissions.UserAllocation_Edit))
            {
                logger.Info("Don't have rights to add new Requisition");
                return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
            }
            var AdminUser = RoleManager.Roles.Where(x => x.Name.Equals(UserRoles.Admin)).SingleOrDefault();
            var userList = UserManager.Users.Where(x => !x.IsDeleted && !(x.Roles.Select(y => y.RoleId).Contains(AdminUser.Id))).ToList();

            UserReAllocationVM allocationVM = new UserReAllocationVM();
            allocationVM.UserList = userList;
            allocationVM.UserId =  userId;
             
            var userAllocatedDepartments = userAllocationManagement.GetUserAllocationByGroupNumber(groupNumber);
            if(userAllocatedDepartments != null && userAllocatedDepartments.Count() > 0 )
            {
                var departments = departmentManagement.GetAllDepartments();
                foreach (var userDept in userAllocatedDepartments)
                {
                    userDept.Departments = departments;

                }
                allocationVM.UserAllocatedDepartments = userAllocatedDepartments;
            }

            return PartialView("_AddUpdateAllocation", allocationVM);
        }

        [Route("AddEdit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddEdit(UserReAllocationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string deparmentError = ValidateUserDeparments(model);
                    if (!string.IsNullOrEmpty(deparmentError))
                    {
                        return Json(new { Message = deparmentError, MessageClass = MessageClass.Error, Response = false });
                    }

                    bool isHundredPercent = ValidateAllocationPercentage(model);
                    if (!isHundredPercent)
                    {
                        return Json(new { Message = "Total user department allocation must equal to 100%", MessageClass = MessageClass.Error, Response = false });
                    }

                    if (!PermissionControl.CheckPermission(UserAppPermissions.UserAllocation_Add) && !PermissionControl.CheckPermission(UserAppPermissions.UserAllocation_Edit))
                    {
                        logger.Info("Don't have rights to add update user allocation");
                        return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                    }

                    var userPendingAllocation = userAllocationManagement.GetUserPendingAllocationsByUserId(model.UserId);
                    if(userPendingAllocation != null && userPendingAllocation.Count() > 0 )
                    {
                        logger.Info("user have already applied for user allocation and its in pending.");
                        return Json(new { Message = "you have already applied for this user allocation and its in pending.", MessageClass = MessageClass.Error, Response = false });
                    }

                    var user = UserManager.FindById(User.Identity.GetUserId());
                    var userDepartment = userdepartmentManagement.GetUserPrimaryDepartmentById(model.UserId);

                    if(userDepartment == null)
                    {
                        logger.InfoFormat("Selected User with ID [{0}] has no department assigned yet", model.UserId);
                        return Json(new { Message = "Selected User have no department assigned. first add his department to perform user allocation opertion.", MessageClass = MessageClass.Error, Response = false });
                    }

                    var requesteduserDepartment = userdepartmentManagement.GetUserPrimaryDepartmentById(User.Identity.GetUserId());

                    if (requesteduserDepartment == null)
                    {
                        logger.InfoFormat("User Allocation requested User with ID [{0}] has no department assigned him", model.UserId);
                        return Json(new { Message = "User Allocation requested User has no department assigned. first add his department to perform user allocation opertion.", MessageClass = MessageClass.Error, Response = false });
                    }

                    long groupNumber = DateTime.Now.Ticks;
                    for (int i=0; i< model.SelectedDepartment.Length;i++)
                    {
                        UserAllocation userAllocate = new UserAllocation();
                        userAllocate.UserID = model.UserId;
                        userAllocate.StartDate = DateTime.Now;
                        userAllocate.DepartmentID = model.SelectedDepartment[i];
                        userAllocate.Percentage = model.SelectedDepartmentPercentage[i];
                        userAllocate.RequestedDepartmentID = requesteduserDepartment.DepartmentID;
                        userAllocate.Status = RequestStatus.Pending;
                        userAllocate.CreatedBy = new Guid(User.Identity.GetUserId());
                        userAllocate.IsActive = false;
                        userAllocate.GroupNumber = groupNumber;
                        userAllocate.UserDepartmentID = userDepartment.DepartmentID;
                        userAllocationManagement.Add(userAllocate);
                    }

                    userAllocationManagement.SaveUserAllocation();

                    SendUserAllocationEmailsToLevel1Departments(groupNumber);
                    logger.Info("Successfully Saved User Allocation ");
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "User Allocation"), MessageClass = MessageClass.Success, Response = true });
                }
                else
                {
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "User Allocation"), MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        public void AllocateUserInDepartment(long requisitionID, long departmentId, decimal percentage, UserAllocation allocate, long requestedDepartmentId)
        {
            if (allocate == null)
            {
                UserAllocation userAllocate = new UserAllocation();
                userAllocate.RequisitionID = requisitionID;
                userAllocate.StartDate = DateTime.Now;
                userAllocate.DepartmentID = departmentId;
                userAllocate.Percentage = percentage;
                userAllocate.RequestedDepartmentID = requestedDepartmentId;
                userAllocate.Status = RequestStatus.Pending;
                userAllocate.CreatedBy = new Guid(User.Identity.GetUserId());
                userAllocate.IsActive = false;
                userAllocationManagement.Add(userAllocate);
            }
            else
            {
                allocate.StartDate = DateTime.Now;
                allocate.Percentage = percentage;
                allocate.IsActive = false;
                allocate.Status = RequestStatus.Pending;
                allocate.Comments = string.Empty;
                allocate.ModifiedBy = new Guid(User.Identity.GetUserId());
                userAllocationManagement.Update(allocate);
            }

        }

        private string ValidateUserDeparments(UserReAllocationVM model)
        {
            try
            {
                if (model.SelectedDepartment != null && model.SelectedDepartment.Count() > 0)
                {
                  
                    for (int i = 0; i < model.SelectedDepartmentPercentage.Length; i++)
                    {
                        if (Convert.ToDecimal(model.SelectedDepartmentPercentage[i]) == 0)
                            return "Allocation percentage cannot be 0 value.";
                    }

                    var isDuplicateDeptExist = model.SelectedDepartment.GroupBy(n => n).Any(g => g.Count() > 1);
                    if (isDuplicateDeptExist)
                        return "Duplicate department exist in list.";
                }
                else
                {
                    return "Atlease one user allocation department must be selected.";
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Error occured try later.";
            }
        }

        private bool ValidateAllocationPercentage(UserReAllocationVM model)
        {
            bool isHundredPer = true;
            decimal totalPercentage = 0;
            if (model.SelectedDepartmentPercentage != null)
            {
                try
                {
                    for (int i = 0; i < model.SelectedDepartmentPercentage.Length; i++)
                    {
                        totalPercentage += Convert.ToDecimal(model.SelectedDepartmentPercentage[i]);
                    }

                    if (totalPercentage != 100)
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return isHundredPer;
        }

        [Route("GetAllUserAllocation")]
        public ActionResult GetAllUserAllocation(string userId)
        {
           
            var userAllocatedDepartments = userAllocationManagement.GetUserAllocationsByUserId(userId);
            var department = departmentManagement.GetAllDepartments();
            if (userAllocatedDepartments != null)
            {
                foreach (var userDept in userAllocatedDepartments)
                {
                    userDept.Departments = department;

                }
            }
         
            return PartialView("_UserAllocatedDepartments", userAllocatedDepartments);
        }
    }
}