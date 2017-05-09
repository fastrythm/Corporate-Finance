using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Implementation;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
using log4net;
using Microsoft.AspNet.Identity;
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
    public class PettyCashController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(PettyCashController));
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IPettyCashManagement pettyCashManagement;


        public PettyCashController(IPettyCashManagement pettyCashManagement)
        {
            this.pettyCashManagement = pettyCashManagement;
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




        // GET: PettyCash
        public ActionResult Index()
        {
            ViewBag.Title = "Petty Cash Listing";
            logger.Info("Petty Cash Page Access");
            if (!PermissionControl.CheckPermission(UserAppPermissions.PettyCash_View))
            {
                logger.Info("Don't have rights to access  Petty Cash Page");
                return RedirectToAction("Restricted", "Home");
            }

            var user = UserManager.FindById(User.Identity.GetUserId());
            return View();

        }


        [HttpPost]
        [Route("PettyCashList")]
        public ActionResult PettyCashList(DataTablesViewModel param, string fromDate, string toDate)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.PettyCash_View))
                { return RedirectToAction("Restricted", "Home"); }

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(fromDate))
                    frdate = DateTime.Parse(fromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(toDate))
                    tdate = DateTime.Parse(toDate);


                logger.DebugFormat("Getting Petty Cash List with From Date [{0}] and To Date [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());

                PettyCash pettyCash = new PettyCash();
                pettyCash.DTObject = param;
                var list = pettyCashManagement.GetAllPettyCashByParam(pettyCash, frdate, tdate);
                logger.DebugFormat("Successfully Retrieve  Petty Cash List Records [{2}] with From Date [{0}] and To Date [1]", frdate.ToShortDateString(), tdate.ToShortDateString(), list.Count());

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

        [HttpPost]
        [Route("GetOpeningClosingBalance")]
        public JsonResult GetOpeningClosingBalance(string fromDate, string toDate)
        {
            try
            {
                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(fromDate))
                    frdate = DateTime.Parse(fromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(toDate))
                    tdate = DateTime.Parse(toDate);

                logger.DebugFormat("Getting Opening Closing Balance in Petty Cash with From Date [{0}] and To Ddate [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());


                PettyCashOpenCloseBalance pettyCashOpenCloseBalance = pettyCashManagement.GetOpeningClosingBalance(frdate, tdate);

                logger.DebugFormat("Successfully Retrieve  Opening Closing Balance From Date [{0}] and To Ddate [{1}] Opening Balance [{2}] Closing Balance [{2}]", frdate.ToShortDateString(), tdate.ToShortDateString(), pettyCashOpenCloseBalance.OpeningBalance, pettyCashOpenCloseBalance.ClosingBalance);

                return Json(pettyCashOpenCloseBalance);
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
                logger.DebugFormat("Deleting Petty Cash With PettyCashID [{0}] ", id);

                if (!PermissionControl.CheckPermission(UserAppPermissions.PettyCash_Delete))
                {
                    logger.Info("Don't have right to delete Petty Cash record");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                PettyCash pettyCash = pettyCashManagement.GetPettyCash(id);


                if (pettyCash.TransactionDate.Date < DateTime.Now.AddDays(-1).Date)
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_RECORD, MessageClass = MessageClass.Error, Response = false });


                pettyCash.IsDeleted = true;
                if (pettyCashManagement.Delete(pettyCash))
                {
                    pettyCashManagement.SavePettyCash();
                    logger.Info("Petty Cash record Successfully Deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
                }
                else
                {
                    logger.Info("Petty Cash record not deleted");
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
            ViewBag.Title = "Add/Update New Petty Cash";

            var pettyCash = pettyCashManagement.GetPettyCash(id);
            return PartialView("_AddEditPettyCash", pettyCash);
        }


        [Route("AddEdit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddEdit(PettyCash model)
        {
            try
            {
                logger.DebugFormat("Add Petty Cash with TransactionDate [{0}],  TransactionType [{1}],  ChartOfAccountTitle [{2}], Amount [{3}],  Description [{4}]  ",
                       model.TransactionDate, model.TransactionType, model.ChartOfAccountTitle, model.Amount, model.Description);
                if (!PermissionControl.CheckPermission(UserAppPermissions.PettyCash_Add))
                {
                    logger.Info("Don't have rights to add  Petty Cash");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                if (ModelState.IsValid)
                {
                    model.UserID = new Guid(User.Identity.GetUserId());
                    if (pettyCashManagement.Add(model))
                    {
                        pettyCashManagement.SavePettyCash();
                        logger.Info("Successfully Saved Petty Cash ");
                        return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "PettyCash"), MessageClass = MessageClass.Success, Response = true });
                    }
                    else
                    {
                        logger.Info("Petty Cash Not Saved");
                        return Json(new { Message = string.Format("Validation Failded", "PettyCash"), MessageClass = MessageClass.Error, Response = false });
                    }
                }
                else
                {
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "PettyCash"), MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

    }
}