using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
using log4net;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web;

namespace CorporateAndFinance.Web.Controllers.Admin
{
    [Authorize]
    public class UserExpenseController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(BankPositionController));
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUserExpenseManagement userExpenseManagement;


        public UserExpenseController(IUserExpenseManagement userExpenseManagement)
        {
            this.userExpenseManagement = userExpenseManagement;
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




        // GET: User Expense
        public ActionResult Index()
        {
            logger.Info("User Expense Page Access");
            ViewBag.Title = "User Expense Listing";

            if (!PermissionControl.CheckPermission(UserAppPermissions.UserExpense_View))
            {
                logger.Info("Don't have rights to access  User Expense Page");
                return RedirectToAction("Restricted", "Home");
            }

            return View();

        }

        [Route("UploadNew")]

        public ActionResult UploadNew()
        {
            ViewBag.Title = "Upload New User Expense Sheet";

            if (!PermissionControl.CheckPermission(UserAppPermissions.UserExpense_Add))
            {
                return RedirectToAction("Restricted", "Home");
            }

            return PartialView("_UploadUserExpense");
        }

        [Route("Delete")]
        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                logger.DebugFormat("Deleting User Expense With CardExpenseID [{0}] ", id);


                if (!PermissionControl.CheckPermission(UserAppPermissions.UserExpense_Delete))
                {
                    logger.Info("Don't have right to delete User Expense record");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                UserExpense userExpense = userExpenseManagement.GetUserExpense(id);

                userExpense.IsActive = false;
                if (userExpenseManagement.Delete(userExpense))
                {
                    userExpenseManagement.SaveUserExpense();
                    logger.Info("User Expense record Successfully Deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
                }
                else
                {
                    logger.Info("User Expense record Not Deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }


        [HttpPost]
        [Route("UserExpenseList")]

        public ActionResult UserExpenseList(DataTablesViewModel param, string fromDate, string toDate)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.UserExpense_View))
                { return RedirectToAction("Restricted", "Home"); }

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(fromDate))
                    frdate = DateTime.Parse(fromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(toDate))
                    tdate = DateTime.Parse(toDate);

                logger.DebugFormat("Getting Users Expense List with From Date [{0}] and To date [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());


                UserExpenseVM userExpenseVM = new UserExpenseVM();
                userExpenseVM.DTObject = param;
                var list = userExpenseManagement.GetAllUserExpensesByParam(userExpenseVM, frdate, tdate);

                logger.DebugFormat("Successfully Retrieve Users Expense List Records [{2}] with From Date [{0}] and To date [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString(), list.Count());

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
    }
}