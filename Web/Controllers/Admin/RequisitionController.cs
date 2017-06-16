﻿using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Web;
using RazorEngine;
using RazorEngine.Templating;
using CorporateAndFinance.Service.Helper;
using log4net;
using System.Web.Script.Serialization;

namespace CorporateAndFinance.Web.Controllers.Admin
{
    [Authorize]
    public class RequisitionController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(RequisitionController));
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        private readonly IRequisitionManagement requisitionManagement;
        private readonly IUserAllocationManagement userAllocationManagement;
        private readonly IRequisitionApprovalManagement requisitionApprovalManagement;
        private readonly IUserManagement userManagement;
        private readonly ICommunicationManagement comManagement;
        private readonly IUserDepartmentManagement userdepartmentManagement;
        private readonly IDepartmentManagement departmentManagement;

        public RequisitionController(IRequisitionManagement requisitionManagement, IUserAllocationManagement userAllocationManagement, IRequisitionApprovalManagement requisitionApprovalManagement, IUserManagement userManagement, ICommunicationManagement comManagement, IUserDepartmentManagement userdepartmentManagement, IDepartmentManagement departmentManagement)
        {
            this.requisitionManagement = requisitionManagement;
            this.userAllocationManagement = userAllocationManagement;
            this.requisitionApprovalManagement = requisitionApprovalManagement;
            this.userManagement = userManagement;
            this.comManagement = comManagement;
            this.userdepartmentManagement = userdepartmentManagement;
            this.departmentManagement = departmentManagement;
        }


        public RequisitionController(ApplicationSignInManager signInManager, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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
            logger.Info("Requisition Page Access");
            ViewBag.Title = "Requisition";
            if (!PermissionControl.CheckPermission(UserAppPermissions.Requisition_View))
            {
                logger.Info("Don't have rights to access Requisition Page");
                return RedirectToAction("Restricted", "Home");
            }

            return View();
        }

 
        [HttpPost]
        [Route("RequisitionList")]
        public ActionResult RequisitionList(DataTablesViewModel param, string fromDate, string toDate)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.Requisition_View))
                { return RedirectToAction("Restricted", "Home"); }

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(fromDate))
                    frdate = DateTime.Parse(fromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(toDate))
                    tdate = DateTime.Parse(toDate);


                logger.DebugFormat("Getting Requisition List with From Date [{0}] and To Date [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());

                IEnumerable<UserDepartment> userDepartments;
                bool IsAdmin = false;
                if (User.IsInRole("Admin"))
                {
                    IsAdmin = true;
                }

                    var appUser =  UserManager.FindById(User.Identity.GetUserId());
                userDepartments = userdepartmentManagement.GetAllUserDepartmentById(appUser.Id);


                RequisitionVM requisition = new RequisitionVM();
                requisition.DTObject = param;
                var list = requisitionManagement.GetAllRequisitionByParam(requisition, frdate, tdate, userDepartments,IsAdmin);
                logger.DebugFormat("Successfully Retrieve  Requisition List Records [{2}] with From Date [{0}] and To Date [1]", frdate.ToShortDateString(), tdate.ToShortDateString(), list.Count());

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



        [Route("Delete")]
        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                logger.DebugFormat("Deleting Requisition With ID [{0}] ", id);

                if (!PermissionControl.CheckPermission(UserAppPermissions.Requisition_Delete))
                {
                    logger.Info("Don't have right to delete Requisition record");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                Requisition req = requisitionManagement.GetRequisition(id);

                req.IsDeleted = true;
                if (requisitionManagement.Update(req))
                {
                    requisitionManagement.SaveRequisition();
                    logger.Info("Requisition record Successfully Deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
                }
                else
                {
                    logger.Info("Requisition record not deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        [Route("AddDepartmentAllocation")]
        public ActionResult AddDepartmentAllocation()
        {
            UserAllocation userDepartment = new UserAllocation();
            userDepartment.Departments = departmentManagement.GetAllDepartments();
            return PartialView("_UserAllocatedDepartment", userDepartment);
        }

        [Route("AddEdit")]
        public ActionResult AddEdit(int id)
        {
            ViewBag.Title = "Add/Update New Requisition";
            bool isAdmin = false;
            if (User.IsInRole("Admin"))
            {
                isAdmin = true;
            }
            var departments = departmentManagement.GetAllDepartments();

            Requisition requisition = requisitionManagement.GetRequisition(id);
            if (requisition == null)
               requisition = new Requisition();
            requisition.UserDepartments =  userdepartmentManagement.GetAllUserDepartmentByUserId(User.Identity.GetUserId(), isAdmin);
            var userAllocatedDepartments = userAllocationManagement.GetUserAllocationsByRequisition(id);

            if (userAllocatedDepartments != null)
            {
                foreach (var userDept in userAllocatedDepartments)
                {
                    userDept.Departments = departments;
                    
                }
                requisition.UserAllocatedDepartments = userAllocatedDepartments;
            }

            return PartialView("_AddEditRequisition", requisition);
        }



        [Route("AddEdit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddEdit(Requisition model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string deparmentError = ValidateUserDeparments(model);
                    if(!string.IsNullOrEmpty(deparmentError))
                    {
                        return Json(new { Message = deparmentError, MessageClass = MessageClass.Error, Response = false });
                    }

                    bool isHundredPercent = ValidateAllocationPercentage(model);
                    if(!isHundredPercent)
                    {
                        return Json(new { Message = "Total user department allocation must equal to 100%", MessageClass = MessageClass.Error, Response = false });
                    }

                    if (model.RequisitionID == 0)
                    {
                        model.Status = RequisitionStatus.Level1_Pending;
                        string jsonObject = new JavaScriptSerializer().Serialize(model);
                        logger.DebugFormat("Add new Requisition with Parameters [{0}]", jsonObject);

                        if (!PermissionControl.CheckPermission(UserAppPermissions.Requisition_Add))
                        {
                            logger.Info("Don't have rights to add new Requisition");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }
                        model.CreatedBy = new Guid(User.Identity.GetUserId());
                      
                        if (requisitionManagement.Add(model))
                        {
                            requisitionManagement.SaveRequisition();
                            AddUpdateUserAllocation(model);
                            logger.Info("Successfully Saved New Requisition ");
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "Requisition"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            logger.Info("Requisition Not Saved");
                            return Json(new { Message = string.Format("Validation Failded", "Requisition"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                    else
                    {
                        string jsonObject = new JavaScriptSerializer().Serialize(model);
                        logger.DebugFormat("Updating existing Requisition with Parameters [{0}]", jsonObject);

                        if (!PermissionControl.CheckPermission(UserAppPermissions.Requisition_Edit))
                        {
                            logger.Info("Don't have rights to update Requisition");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        var req = requisitionManagement.GetRequisition(model.RequisitionID);
                        if (req.Status != RequisitionStatus.Level1_Rejected || req.Status != RequisitionStatus.Level2_Rejected)
                        {
                            return Json(new { Message = "Your requisition is in pending. you are have no rights to change.", MessageClass = MessageClass.Error, Response = false });
                        }

                        requisitionManagement.DeAttach(req);

                        if (requisitionManagement.Update(model))
                        {
                            requisitionManagement.SaveRequisition();
                            AddUpdateUserAllocation(model);
                            logger.Info("Successfully Updated Requisition ");
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "Requisition"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            logger.Info("Requisition Not Updated");
                            return Json(new { Message = string.Format("Validation Failded", "Requisition"), MessageClass = MessageClass.Error, Response = false });
                        }

                    }
                }
                else
                {
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "Requisition"), MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }



        private string ValidateUserDeparments(Requisition model)
        {
            try
            {
                if (model.SelectedDepartment != null)
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
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Error occured try later.";
            }
        }

        private bool ValidateAllocationPercentage(Requisition model)
        {
            bool isHundredPer = true;
            decimal totalPercentage = 0;
            if (model.SelectedDepartmentPercentage != null)
            {
                try
                {
                    for (int i=0;i<model.SelectedDepartmentPercentage.Length;i++)
                    {
                        totalPercentage += Convert.ToDecimal(model.SelectedDepartmentPercentage[i]);
                    }

                    if (totalPercentage != 100)
                        return false;
                    
                } catch (Exception ex)
                {
                    return false;
                }
            }
            return isHundredPer;
        }

        private void AddUpdateUserAllocation(Requisition model)
        {

            var userAllocations = userAllocationManagement.GetUserAllocationsByRequisition(model.RequisitionID);
            if(userAllocations != null && userAllocations.Count() > 0)
            {
                for (int i = 0; i < model.SelectedDepartment.Length; i++)
                {
                   var allocate =  userAllocations.Where(x => x.DepartmentID == model.SelectedDepartment[i]).FirstOrDefault();
                    if (allocate != null)
                    {
                        if (allocate.DepartmentID == model.SelectedDepartment[i] && allocate.Percentage != model.SelectedDepartmentPercentage[i])
                        {
                            AllocateUserInDepartment(model.RequisitionID, model.SelectedDepartment[i], model.SelectedDepartmentPercentage[i], allocate);
                        }
                    }
                    else
                    {
                        AllocateUserInDepartment(model.RequisitionID, model.SelectedDepartment[i], model.SelectedDepartmentPercentage[i], null);
                    }

                  
                }

                // Delete department that not exist in select list
                foreach(var allocate in userAllocations)
                {
                    if(!model.SelectedDepartment.Contains(allocate.DepartmentID))
                    {
                        userAllocationManagement.Delete(allocate);
                    }

                }
                userAllocationManagement.SaveUserAllocation();
            }
            else
            {
                for(int i=0;i<model.SelectedDepartment.Length;i++)
                {
                    AllocateUserInDepartment(model.RequisitionID, model.SelectedDepartment[i], model.SelectedDepartmentPercentage[i],null);
                }

                userAllocationManagement.SaveUserAllocation();
            }

        }

        public void AllocateUserInDepartment(long requisitionID,long departmentId,decimal percentage, UserAllocation allocate)
        {
            if (allocate == null)
            {
                UserAllocation userAllocate = new UserAllocation();
                userAllocate.RequisitionID = requisitionID;
                userAllocate.StartDate = DateTime.Now;
                userAllocate.DepartmentID = departmentId;
                userAllocate.Percentage = percentage;
                userAllocate.Status = RequestStatus.Pending;
                userAllocate.CreatedBy = new Guid(User.Identity.GetUserId());
                userAllocate.IsActive = true;
                userAllocationManagement.Add(userAllocate);
            }
            else
            {
                allocate.StartDate = DateTime.Now;
                allocate.Percentage = percentage;
                allocate.ModifiedBy = new Guid(User.Identity.GetUserId());
                userAllocationManagement.Update(allocate);
            }
           
            
        }


        //private void SendUserTaskEmail(long taskId)
        //{
        //    try
        //    {
        //        logger.DebugFormat("Sending User Task Email with Task ID [{0}]", taskId);
        //        UserTaskEmailVM taskDetail = userTaskManagement.GetUserTaskEmailDetails(taskId);

        //        if (taskDetail == null)
        //        {
        //            logger.DebugFormat("No Task Found With ID [{0}]", taskId);
        //            return;
        //        }
        //        string styleSheet = System.IO.File.ReadAllText(Server.MapPath("~/Themes/finance-1/css/emailstyle.css"));
        //        taskDetail.StyleSheet = styleSheet;

        //        var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/EmailTemplates/UserTask.cshtml"));
        //        string template = System.IO.File.ReadAllText(viewsPath);

        //        string uniqueNumber = Guid.NewGuid().ToString();
        //        string body = Engine.Razor.RunCompile(template, string.Format("UserTaskEmail_{0}", uniqueNumber), typeof(UserTaskEmailVM), taskDetail);

        //        if (taskDetail != null && taskDetail.UserTaskDetails.Count > 0)
        //        {
        //            UserTaskDetailVM activeTaskDetail = taskDetail.UserTaskDetails.FirstOrDefault(x => x.IsActive == true);
        //            comManagement.Recipient = activeTaskDetail.ToUserEmail;
        //            comManagement.Subject = string.Format("Ticket {0} - {1} ", taskDetail.TicketNumber, taskDetail.Title);
        //            comManagement.Body = body;

        //            List<string> rcc = new List<string>();
        //            rcc.Add(activeTaskDetail.FromUserEmail);
        //            logger.DebugFormat("Getting user with roles [{0}]", UserRoles.FinanceManager);

        //            var role = RoleManager.Roles.Where(x => x.Name.Equals(UserRoles.FinanceManager)).FirstOrDefault();
        //            var users = UserManager.Users.Where(x => !x.IsDeleted && x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();
        //            logger.DebugFormat("Total [{0}] users found with roles [{1}]", UserRoles.FinanceManager, users.Count());

        //            if (users.Count > 0)
        //                foreach (var user in users)
        //                {
        //                    if (!user.Email.Equals(activeTaskDetail.FromUserEmail) && !user.Email.Equals(activeTaskDetail.ToUserEmail))
        //                        rcc.Add(user.Email);
        //                }

        //            comManagement.RecipientCC = rcc;
        //            comManagement.HeaderImage = Server.MapPath("~/Themes/finance-1/img/logo.png");

        //            Async.Do(() => comManagement.SendEmail());
        //            logger.DebugFormat("Email Successfully Send");
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
        //    }
        //}

        public ActionResult Uploader()
        {

            if (!PermissionControl.CheckPermission(UserAppPermissions.Compliance_Edit) || !PermissionControl.CheckPermission(UserAppPermissions.Compliance_Add))
            {
                return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
            }

            string fname = string.Empty;
            string tempFname = string.Empty;
            string path = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];


                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            tempFname = fname;
                        }
                        else
                        {
                            fname = file.FileName;
                            tempFname = fname;
                        }

                        // Get the complete folder path and store the file inside it. 

                        string fileName = Path.GetFileName(fname);
                        string fileExtension = Path.GetExtension(fname);
                          path = string.Format("{0}{1}_{2}{3}", Server.MapPath("~/UploadFiles/"), fileName, DateTime.Now.Ticks.ToString(), fileExtension);

                     //   fname = Path.Combine(Server.MapPath("~/UploadFiles/"), fname);
                        file.SaveAs(path);
                    }
                    // Returns message that successfully uploaded  
                    return Json(path);

                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }
    }


}