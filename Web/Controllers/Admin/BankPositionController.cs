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
    public class BankPositionController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(BankPositionController));
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IBankPositionManagement bankPositionManagement;
 

        public BankPositionController(IBankPositionManagement bankPositionManagement)
        {
            this.bankPositionManagement = bankPositionManagement;
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
            logger.Info("Bank Position Page Access");
            ViewBag.Title = "Bank Position Listing";

            if (!PermissionControl.CheckPermission(UserAppPermissions.BankPosition_View))
            {
                logger.Info("Don't have rights to access  Bank Position Page");
                return RedirectToAction("Restricted", "Home");
            }

            return View();
            
        }

        
        [HttpPost]
        [Route("BankPositionList")]
        public ActionResult BankPositionList(string fromDate)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.BankPosition_View))
                { return RedirectToAction("Restricted", "Home"); }

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(fromDate))
                    frdate = DateTime.Parse(fromDate);


                logger.DebugFormat("Getting Bank Postion List with From Date [{0}]", frdate.ToShortDateString());


                var jsonObj = bankPositionManagement.GetAllBankPositionByParam(frdate);

                logger.DebugFormat("Retrieve Bank Postion [{0}] Records with From Date [{1}]", jsonObj.Count(),frdate.ToShortDateString());

                return Json(new
                {
                    aaData = jsonObj
                });
            }
            catch(Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        
        [Route("AddEdit")]
        public ActionResult AddEdit(long id,string number,long cbid,string date)
        {
            ViewBag.Title = "Add/Update Bank Position";

            var companyBankPosition = new CompanyBankPosition();
            if (id != 0)
            {
                companyBankPosition = bankPositionManagement.GetBankPosition(id);
                if (companyBankPosition != null)
                    companyBankPosition.AccountNumber = number;
            }
            else
            {
                companyBankPosition.AccountNumber = number;
                companyBankPosition.CompanyBankID = cbid;
                companyBankPosition.Date = DateTime.Parse(date);
            }

            return PartialView("_AddEditBankPosition", companyBankPosition);
        }


        [Route("AddEdit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddEdit(CompanyBankPosition model)
        {
            try
            {
                if (model.Date < DateTime.Now.AddDays(-1).Date)
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_RESTRICTED_UPDATE_RECORD, MessageClass = MessageClass.Error, Response = false });


                logger.DebugFormat("Add/Update Bank Postion Account No [{0}],  CompanyBankID [{1}],  CompanyBankPositionID [{2}], Date [{3}],  Amount [{4}]", model.AccountNumber, model.CompanyBankID,model.CompanyBankPositionID,model.Date,model.Amount);


                if (ModelState.IsValid)
                {
                    if (model.CompanyBankPositionID == 0)
                    {
                        if (!PermissionControl.CheckPermission(UserAppPermissions.BankPosition_Add))
                        {
                            logger.Info("Dont have Add permission.");

                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        if (bankPositionManagement.Add(model))
                        {
                            bankPositionManagement.SaveBankPosition();
                            logger.Info("Successfully Bank Position Saved.");
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "Bank Position"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            logger.Info("Bank Position Not Saved.");
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "Bank Position"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                    else
                    {
                        if (!PermissionControl.CheckPermission(UserAppPermissions.BankPosition_Edit))
                        {
                            logger.Info("Dont have Update permission.");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        if (bankPositionManagement.Update(model))
                        {
                            bankPositionManagement.SaveBankPosition();
                            logger.Info("Successfully Bank Position Updated.");
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPDATE_SUCCESS, "Bank Position"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            logger.Info("Bank Position Not Update.");
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPDATE_FAILED, "Bank Position"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                   
                }
                else
                {
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "Bank Position"), MessageClass = MessageClass.Error, Response = false });
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