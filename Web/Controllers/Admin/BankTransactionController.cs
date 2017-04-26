using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
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
    public class BankTransactionController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IBankTransactionManagement bankTransactionManagement;
        private readonly ICompanyManagement companyManagment;
        private readonly IConsultantManagement consultantManagment;

        public BankTransactionController(IBankTransactionManagement bankTransactionManagement, ICompanyManagement companyManagment, IConsultantManagement consultantManagment)
        {
            this.bankTransactionManagement = bankTransactionManagement;
            this.companyManagment = companyManagment;
            this.consultantManagment = consultantManagment;
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



        // GET: BankTransaction
        public ActionResult Index()
        {
            if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_View))
            { return RedirectToAction("Restricted", "Home"); }

            return View();
        }

        [Route("AddEdit")]
        public ActionResult AddEdit(int id)
        {


            var transaction = bankTransactionManagement.GetCompanyBankTransaction(id);
            if (transaction == null)
                transaction = new CompanyBankTransaction();

            transaction.CompanyBankAccounts = companyManagment.GetCompanyBankAccounts();
            transaction.CategoryVendors = companyManagment.GetAllVendorCompanies();
            transaction.CategoryClients = companyManagment.GetAllClientCompanies();
            transaction.CategoryConsultants = consultantManagment.GetAllConsultants();

            return PartialView("_AddEditBankTransaction", transaction);
        }


        [Route("AddEdit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddEdit(CompanyBankTransaction model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (model.CompanyBankTransactionID == 0)
                    {
                        if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_Add))
                        {
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }
                        SetCategoryTypeID(model);

                        if (bankTransactionManagement.Add(model))
                        {
                            bankTransactionManagement.SaveCompanyBankTransaction();
                            //Reversal entery for Inter Company case.
                            if (model.CategoryType == CompanyType.Inter)
                            {
                                long companyId = model.CompanyBankID;
                                long? toCompanyId = model.ToCompanyBankID;
                                model.CompanyBankID = Convert.ToInt64(toCompanyId);
                                model.ToCompanyBankID = companyId;
                                model.TransactionType = model.TransactionType == "Receipt" ? "Payment" : "Receipt";
                                bankTransactionManagement.Add(model);
                                bankTransactionManagement.SaveCompanyBankTransaction();
                            }


                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "Transaction"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            if (model != null)
                            {
                                model.CompanyBankAccounts = companyManagment.GetCompanyBankAccounts();
                                model.CategoryVendors = companyManagment.GetAllVendorCompanies();
                                model.CategoryClients = companyManagment.GetAllClientCompanies();
                                model.CategoryConsultants = consultantManagment.GetAllConsultants();
                            }
                            return Json(new { Message = string.Format("Validation Failded", "Transaction"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                    else
                    {
                        if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_Edit))
                        {
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        SetCategoryTypeID(model);
                        if (bankTransactionManagement.Update(model))
                        {
                            bankTransactionManagement.SaveCompanyBankTransaction();
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPDATE_SUCCESS, "Transaction"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            if (model != null)
                            {
                                model.CompanyBankAccounts = companyManagment.GetCompanyBankAccounts();
                                model.CategoryVendors = companyManagment.GetAllVendorCompanies();
                                model.CategoryClients = companyManagment.GetAllClientCompanies();
                                model.CategoryConsultants = consultantManagment.GetAllConsultants();
                            }
                            return Json(new { Message = string.Format("Validation Failded", "Transaction"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                }
                else
                {
                    if (model != null)
                    {
                        model.CompanyBankAccounts = companyManagment.GetCompanyBankAccounts();
                        model.CategoryVendors = companyManagment.GetAllVendorCompanies();
                        model.CategoryClients = companyManagment.GetAllClientCompanies();
                        model.CategoryConsultants = consultantManagment.GetAllConsultants();
                    }
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "Transaction"), MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        private void SetCategoryTypeID(CompanyBankTransaction model)
        {
            if (model.CategoryType == CompanyType.Client)
            { model.CategoryReferenceID = model.CategoryClientID;
                model.ToCompanyBankID = null;
            }
            else if (model.CategoryType == CompanyType.Vendor)
            { model.CategoryReferenceID = model.CategoryVendorID; model.ToCompanyBankID = null; }
            else if (model.CategoryType == EntityType.Consultant)
            { model.CategoryReferenceID = model.CategoryConsultantID; model.ToCompanyBankID = null; }
            else if (model.CategoryType == EntityType.AutoDebit || model.CategoryType == EntityType.Other)
            { model.CategoryReferenceID = null; model.ToCompanyBankID = null; }
        }

        [HttpPost]
        [Route("BankTransaction")]
        public ActionResult BankTransactionList(DataTablesViewModel param, string fromDate, string toDate)
        {
            if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_View))
            { return RedirectToAction("Restricted", "Home"); }

            DateTime frdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fromDate))
                frdate = DateTime.Parse(fromDate);

            DateTime tdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(toDate))
                tdate = DateTime.Parse(toDate);

            CompanyBankTransactionVM transaction = new CompanyBankTransactionVM();
            transaction.DTObject = param;
            var list = bankTransactionManagement.GetAllBankTransactionByParam(transaction, frdate, tdate);

            return Json(new
            {
                sEcho = param.draw,
                iTotalRecords = list.Select(i => i.DTObject.TotalRecordsCount).FirstOrDefault(),
                iTotalDisplayRecords = list.Select(i => i.DTObject.TotalRecordsCount).FirstOrDefault(), // Filtered Count
                aaData = list
            });
        }


        [Route("Delete")]
        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_Delete))
                {
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                CompanyBankTransaction transaction = bankTransactionManagement.GetCompanyBankTransaction(id);


                if (transaction.TransactionDate.Date < DateTime.Now.AddDays(-1).Date)
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_RECORD, MessageClass = MessageClass.Error, Response = false });


                transaction.IsDeleted = true;
                if (bankTransactionManagement.Delete(transaction))
                {
                    bankTransactionManagement.SaveCompanyBankTransaction();
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
    }
}