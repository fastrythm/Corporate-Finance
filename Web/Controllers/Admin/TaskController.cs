using CorporateAndFinance.Core.Helper.Structure;
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

namespace CorporateAndFinance.Web.Controllers.Admin
{
    [Authorize]
    public class TaskController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(TaskController));
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        private readonly IUserTaskManagement userTaskManagement;
        private readonly IUserTaskDetailManagement userTaskDetailManagement;
        private readonly IUserManagement userManagement;
        private readonly ICommunicationManagement comManagement;
        private readonly IUserDepartmentManagement userdepartmentManagement;

        public TaskController(IUserTaskManagement userTaskManagement, IUserTaskDetailManagement userTaskDetailManagement, IUserManagement userManagement, ICommunicationManagement comManagement, IUserDepartmentManagement userdepartmentManagement)
        {
            this.userTaskManagement = userTaskManagement;
            this.userTaskDetailManagement = userTaskDetailManagement;
            this.userManagement = userManagement;
            this.comManagement = comManagement;
            this.userdepartmentManagement = userdepartmentManagement;
        }


        public TaskController(ApplicationSignInManager signInManager, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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
            logger.Info("User Task Page Access");
            ViewBag.Title = "User Task";
            if (!PermissionControl.CheckPermission(UserAppPermissions.UserTask_View))
            {
                logger.Info("Don't have rights to access User Task Page");
                return RedirectToAction("Restricted", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("UserTaskList")]
        public ActionResult UserTaskList(DataTablesViewModel param, string type)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.UserTask_View))
                { return RedirectToAction("Restricted", "Home"); }

                if (string.IsNullOrEmpty(type))
                    type = User.Identity.GetUserId();

                logger.DebugFormat("Getting User Task List with Ticket Type [{0}]", type);


                UserTaskVM userTask = new UserTaskVM();
                userTask.DTObject = param;
                var userDepartments = userdepartmentManagement.GetAllUserDepartmentById(User.Identity.GetUserId());
                var list = userTaskManagement.GetTaskByCriteria(userTask, type, userDepartments);
                logger.DebugFormat("Successfully Retrieve User Task List Records [{1}] with Ticket Type [{0}]", type, list.Count());


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
                logger.DebugFormat("Deleting User Task With UserTaskID [{0}] ", id);

                if (!PermissionControl.CheckPermission(UserAppPermissions.UserTask_Delete))
                {
                    logger.Info("Don't have right to delete User Task record");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                UserTask userTask = userTaskManagement.GetUserTaskById(id);

                userTask.IsDeleted = true;
                if (userTaskManagement.Delete(userTask))
                {
                    userTaskManagement.SaveUserTask();
                    logger.Info("User Task record Successfully Deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
                }
                else
                {
                    logger.Info("User Task record not deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }


        [Route("AddEdit")]
        public ActionResult AddEdit(int id)
        {
            ViewBag.Title = "Add/Update New Task";
            var task = userTaskManagement.GetUserTaskById(id);
            UserTaskAndDetailVM userTask = new UserTaskAndDetailVM();
            if (task != null)
            {
                userTask = ConvertToViewModel(task);
                userTask.UserTaskDetails = userTaskDetailManagement.GetActiveTaskDetailsByTaskId(id);
            }
           // userTask.Users = userManagement.GetAllUsers();

            var userDepartments = userdepartmentManagement.GetAllUserDepartmentById(User.Identity.GetUserId());
            userTask.Users = userManagement.GetAllUsersByDepartments(userDepartments);

            return PartialView("_AddEditUserTask", userTask);
        }

        private UserTaskAndDetailVM ConvertToViewModel(UserTask task)
        {
            return new UserTaskAndDetailVM
            {
                UserTaskID = task.UserTaskID,
                Title = task.Title,
                Description = task.Description,
                Type = task.Type,
                Priority = task.Priority,
                DueDate = task.DueDate,
                CreatedBy = task.CreatedBy,
                CreatedOn = task.CreatedOn,
                LastModified = task.LastModified,
                IsDeleted = task.IsDeleted
            };
        }

        private UserTask ConvertToModel(UserTaskAndDetailVM taskVM)
        {
            return new UserTask
            {
                UserTaskID = taskVM.UserTaskID,
                Title = taskVM.Title,
                Description = taskVM.Description,
                Type = taskVM.Type,
                Priority = taskVM.Priority,
                DueDate = taskVM.DueDate,
            };
        }


        [Route("AddEdit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddEdit(UserTaskAndDetailVM model)
        {
            try
            {

              

                if (ModelState.IsValid)
                {
                    UserTask userTask = ConvertToModel(model);
                    userTask.CreatedBy = new Guid(User.Identity.GetUserId());
                    if (model.UserTaskID == 0)
                    {   
                        logger.DebugFormat("Add User Task with Title [{0}],  Type [{1}],  Priority [{2}], CreatedBy [{3}],  DueDate [{4}], Description [{5}]  ",
                    model.Title, model.Type, model.Priority, model.CreatedBy, model.DueDate,model.Description);

                        if (!PermissionControl.CheckPermission(UserAppPermissions.UserTask_Add))
                        {
                            logger.Info("Don't have rights to add  User Task");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        if (userTaskManagement.Add(userTask))
                        {
                            userTaskManagement.SaveUserTask();
                            logger.Info("Successfully Saved User Task");
                            AddUpdateUserTaskDetail(model.UserTaskDetails, userTask.UserTaskID);
                            SendUserTaskEmail(userTask.UserTaskID);
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "User Task"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            return Json(new { Message = string.Format("Validation Failded", "User Task"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                    else
                    {
                        logger.DebugFormat("Update User Task with Title [{0}],  Type [{1}],  Priority [{2}], CreatedBy [{3}],  DueDate [{4}], Description [{5}], UserTaskID [{6}]  ",
                   model.Title, model.Type, model.Priority, model.CreatedBy, model.DueDate, model.Description, model.UserTaskID);
                        if (!PermissionControl.CheckPermission(UserAppPermissions.UserTask_Edit))
                        {
                            logger.Info("Don't have rights to update  User Task");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        var task = userTaskManagement.GetUserTaskById(model.UserTaskID);

                        task.Title = userTask.Title;
                        task.Description = userTask.Description;
                        task.Type = userTask.Type;
                        task.Priority = userTask.Priority;
                        task.DueDate = userTask.DueDate;

                        if (userTaskManagement.Update(task))
                        {
                            userTaskManagement.SaveUserTask();
                            logger.Info("Successfully Updated User Task");
                            AddUpdateUserTaskDetail(model.UserTaskDetails, model.UserTaskID);
                            SendUserTaskEmail(model.UserTaskID);
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPDATE_SUCCESS, "User Task"), MessageClass = MessageClass.Success, Response = true });

                        }
                        else
                        {
                            return Json(new { Message = string.Format("Validation Failded", "User Task"), MessageClass = MessageClass.Error, Response = false });
                        }

                    }
                }
                else
                {
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "User Task"), MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }


        private void AddUpdateUserTaskDetail(UserTaskDetail userTaskDetail, long taskId)
        {
            logger.DebugFormat("Add User Task Details with FromUserID [{0}],  ToUserID [{1}],  Status [{2}], Remarks [{3}],  taskId [{4}]  ",
                   userTaskDetail.FromUserID, userTaskDetail.ToUserID, userTaskDetail.Status, userTaskDetail.Remarks, taskId);

            var previousDetail = userTaskDetailManagement.GetActiveTaskDetailsByTaskId(taskId);
            bool IsDetailNotSame = true;



            if (previousDetail != null)
            {
                if (userTaskDetail.ToUserID != previousDetail.ToUserID)
                    userTaskDetail.FromUserID = new Guid(User.Identity.GetUserId());
                else
                    userTaskDetail.FromUserID = previousDetail.FromUserID;

                if ((previousDetail.FromUserID == userTaskDetail.FromUserID) && (previousDetail.ToUserID == userTaskDetail.ToUserID) && (previousDetail.Remarks == userTaskDetail.Remarks) && (previousDetail.Status == userTaskDetail.Status))
                {
                    logger.Info("User Task Details are already same in database.");
                    IsDetailNotSame = false;
                }
            }
            else
                userTaskDetail.FromUserID = new Guid(User.Identity.GetUserId());


            if (IsDetailNotSame)
            {
                logger.DebugFormat("Deleting User Task Details with Task ID [{0}]", taskId);
                userTaskDetailManagement.Delete(taskId);
                logger.DebugFormat("Successfully deleted User Task Details with Task ID [{0}]", taskId);
                userTaskDetail.UserTaskID = taskId;
                userTaskDetail.ChangeBy = new Guid(User.Identity.GetUserId());
                userTaskDetailManagement.Add(userTaskDetail);
                userTaskDetailManagement.SaveUserTaskDetail();

                logger.DebugFormat("Successfully Saved New User Task Details with Task ID [{0}]", taskId);
            }
        }

        private void SendUserTaskEmail(long taskId)
        {
            try
            {
                logger.DebugFormat("Sending User Task Email with Task ID [{0}]", taskId);
                UserTaskEmailVM taskDetail = userTaskManagement.GetUserTaskEmailDetails(taskId);

                if (taskDetail == null)
                {
                    logger.DebugFormat("No Task Found With ID [{0}]", taskId);
                    return;
                }
                string styleSheet = System.IO.File.ReadAllText(Server.MapPath("~/Themes/finance-1/css/emailstyle.css"));
                taskDetail.StyleSheet = styleSheet;

                var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/EmailTemplates/UserTask.cshtml"));
                string template = System.IO.File.ReadAllText(viewsPath);

                string uniqueNumber = Guid.NewGuid().ToString();
                string body = Engine.Razor.RunCompile(template, string.Format("UserTaskEmail_{0}", uniqueNumber), typeof(UserTaskEmailVM), taskDetail);

                if (taskDetail != null && taskDetail.UserTaskDetails.Count > 0)
                {
                    UserTaskDetailVM activeTaskDetail = taskDetail.UserTaskDetails.FirstOrDefault(x => x.IsActive == true);
                    comManagement.Recipient = activeTaskDetail.ToUserEmail;
                    comManagement.Subject = string.Format("Ticket {0} - {1} ", taskDetail.TicketNumber, taskDetail.Title);
                    comManagement.Body = body;

                    List<string> rcc = new List<string>();
                    rcc.Add(activeTaskDetail.FromUserEmail);

                    logger.DebugFormat("Getting user with roles [{0}]", UserRoles.Manager);
                    var role = RoleManager.Roles.Where(x => x.Name.Equals(UserRoles.Manager)).FirstOrDefault();
                   
                    var userDepartments = userdepartmentManagement.GetAllUserDepartmentById(User.Identity.GetUserId());
                    if (userDepartments != null && userDepartments.Count() > 0)
                    {
                        var primaryDepartment = userDepartments.Where(x => x.IsPrimary).SingleOrDefault();
                        var users = userManagement.GetAllUsersByRoleAndDepartment(role.Id, primaryDepartment.DepartmentID);
                          logger.DebugFormat("Total [{0}] users found with roles [{1}]", UserRoles.Manager, users.Count());


                           if (users.Count() > 0)
                            foreach (var user in users)
                            {
                                if (!user.Email.Equals(activeTaskDetail.FromUserEmail) && !user.Email.Equals(activeTaskDetail.ToUserEmail))
                                    rcc.Add(user.Email);
                            }

                        comManagement.RecipientCC = rcc;
                        comManagement.HeaderImage = Server.MapPath("~/Themes/finance-1/img/logo.png");

                        Async.Do(() => comManagement.SendEmail());
                        logger.DebugFormat("Email Successfully Send");
                    }
                }
            }
            catch(Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
            }
        }
    }
}