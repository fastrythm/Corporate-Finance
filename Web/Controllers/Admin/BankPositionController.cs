using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Implementation;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
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
            if (!PermissionControl.CheckPermission(UserAppPermissions.BankPosition_View))
            { return RedirectToAction("Restricted", "Home"); }

            return View();
            
        }

        
        [HttpPost]
        [Route("BankPositionList")]
        public ActionResult BankPositionList(string fromDate)
        {
            if (!PermissionControl.CheckPermission(UserAppPermissions.BankPosition_View))
            { return RedirectToAction("Restricted", "Home"); }

            DateTime frdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fromDate))
                frdate = DateTime.Parse(fromDate);

            var jsonObj = bankPositionManagement.GetAllBankPositionByParam(frdate);
             return Json(new
            {
                aaData = jsonObj
            });
        }

        
        [Route("AddEdit")]
        public ActionResult AddEdit(long id,string number,long cbid,string date)
        {

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

                if (ModelState.IsValid)
                {
                    if (model.CompanyBankPositionID == 0)
                    {
                        if (!PermissionControl.CheckPermission(UserAppPermissions.BankPosition_Add))
                        {
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        if (bankPositionManagement.Add(model))
                        {
                            bankPositionManagement.SaveBankPosition();
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "Bank Position"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "Bank Position"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                    else
                    {
                        if (!PermissionControl.CheckPermission(UserAppPermissions.BankPosition_Edit))
                        {
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        if (bankPositionManagement.Update(model))
                        {
                            bankPositionManagement.SaveBankPosition();
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPDATE_SUCCESS, "Bank Position"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
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
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

    }
}