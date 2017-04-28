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
    public class PettyCashController : Controller
    {
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
            if (!PermissionControl.CheckPermission(UserAppPermissions.PettyCash_View))
            { return RedirectToAction("Restricted", "Home"); }

            var user = UserManager.FindById(User.Identity.GetUserId());
            return View();
            
        }

        
        [HttpPost]
        [Route("PettyCashList")]
        public ActionResult PettyCashList(DataTablesViewModel param,string fromDate,string toDate )
        {
            if (!PermissionControl.CheckPermission(UserAppPermissions.PettyCash_View))
            { return RedirectToAction("Restricted", "Home"); }

            DateTime frdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fromDate))
                frdate = DateTime.Parse(fromDate);

            DateTime tdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(toDate))
                tdate = DateTime.Parse(toDate);

            PettyCash pettyCash = new PettyCash();
            pettyCash.DTObject = param;
           var list =  pettyCashManagement.GetAllPettyCashByParam(pettyCash, frdate, tdate);
           
            return Json(new
            {
                sEcho = param.draw,
                iTotalRecords = list.Select(i => i.DTObject.TotalRecordsCount).FirstOrDefault(),
                iTotalDisplayRecords = list.Select(i => i.DTObject.TotalRecordsCount).FirstOrDefault(), // Filtered Count
                aaData = list
            });
        }

        [HttpPost]
        [Route("GetOpeningClosingBalance")]
        public JsonResult GetOpeningClosingBalance(string fromDate, string toDate)
        {

            DateTime frdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fromDate))
                frdate = DateTime.Parse(fromDate);

            DateTime tdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(toDate))
                tdate = DateTime.Parse(toDate);

            PettyCashOpenCloseBalance pettyCashOpenCloseBalance = pettyCashManagement.GetOpeningClosingBalance(frdate,tdate);
            return Json(pettyCashOpenCloseBalance);

        }


        [Route("Delete")]
        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.PettyCash_Delete))
                {   return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                PettyCash pettyCash = pettyCashManagement.GetPettyCash(id);

                     
                   if(pettyCash.TransactionDate.Date < DateTime.Now.AddDays(-1).Date)
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_RECORD, MessageClass = MessageClass.Error, Response = false });
                 

                    pettyCash.IsDeleted = true;
                    if (pettyCashManagement.Delete(pettyCash))
                    {
                        pettyCashManagement.SavePettyCash();
                        return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
                    }
                    else
                    {
                        return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
                    }
                
            }
            catch (Exception)
            {
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
                if (!PermissionControl.CheckPermission(UserAppPermissions.PettyCash_Add))
                {
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                if (ModelState.IsValid)
                {
                    model.UserID = new Guid(User.Identity.GetUserId());
                    if (pettyCashManagement.Add(model))
                    {
                        pettyCashManagement.SavePettyCash();
                        return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "PettyCash"), MessageClass = MessageClass.Success, Response = true });
                    }
                    else
                    {
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
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

    }
}