using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
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
using System.Web.Script.Serialization;
using Web;

namespace CorporateAndFinance.Web.Controllers.Admin
{
    [Authorize]
    public class BankTransactionController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(BankTransactionController));
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
            logger.Info("Bank Transaction Page Access");
            ViewBag.Title = "Bank Transaction Listing";

            if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_View))
            {
                logger.Info("Don't have rights to access  Bank Transaction Page");
                return RedirectToAction("Restricted", "Home");
            }

            return View();
        }

        [Route("AddEdit")]
        public ActionResult AddEdit(int id)
        {
            ViewBag.Title = "Add/Update Bank Transaction";

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
                    if (model.CategoryType == CompanyType.Inter && model.CompanyBankID == model.ToCompanyBankID)
                    {
                        return Json(new { Message = Resources.Messages.MSG_FROM_INTER_COMAPNY_TO_SAME_COMAPANY_NOTALLOWED, MessageClass = MessageClass.Error, Response = false });
                    }

                    string message = CheckCategoryValidation(model);
                    if(!string.IsNullOrEmpty(message))
                    {
                        return Json(new { Message = message, MessageClass = MessageClass.Error, Response = false });
                    }
                  

                    if (model.CompanyBankTransactionID == 0)
                    {
                        Guid referenceId = Guid.NewGuid();
                        logger.DebugFormat("Add  Bank Transaction with TransactionDate [{0}],  CompanyBankID [{1}],  CategoryType [{2}], CategoryReferenceID [{3}],  Amount [{4}] , CompanyBankID [{5}] , PaymentType[{6}] , TransactionType [{7}] , TransactionStatus [{8}], ReferenceId [{9}]", model.TransactionDate, model.CompanyBankID, model.CategoryType,
                            model.CategoryReferenceID, model.Amount, model.CompanyBankID, model.PaymentType, model.TransactionType, model.TransactionStatus, referenceId);

                       
                        if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_Add))
                        {
                            logger.Info("Don't have rights to add  Bank Transaction");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }
                        SetCategoryTypeID(model);
                        model.CreatedBy = new Guid(User.Identity.GetUserId());
                        model.LastModifiedBy = new Guid(User.Identity.GetUserId());
                        model.ReferenceID = referenceId;
                        if (bankTransactionManagement.Add(model))
                        {
                            bankTransactionManagement.SaveCompanyBankTransaction();
                            logger.Info("Successfully Saved  Bank Transaction");
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

                                logger.Info("Successfully Saved Inter Company 2nd  Bank Transaction");
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

                            logger.Info("Not Saved  Bank Transaction");
                            return Json(new { Message = string.Format("Validation Failded", "Transaction"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                    else
                    {
                        logger.DebugFormat("Update  Bank Transaction with TransactionDate [{0}],  CompanyBankID [{1}],  CategoryType [{2}], CategoryReferenceID [{3}],  Amount [{4}] , CompanyBankID [{5}] , PaymentType[{6}] , TransactionType [{7}] , TransactionStatus [{8}], CompanyBankTransactionID[{9}] , ReferenceId [{10}]", model.TransactionDate, model.CompanyBankID, model.CategoryType,
                          model.CategoryReferenceID, model.Amount, model.CompanyBankID, model.PaymentType, model.TransactionType, model.TransactionStatus, model.CompanyBankTransactionID, model.ReferenceID);
                       
                        if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_Edit))
                        {
                            logger.Info("Don't have rights to update  Bank Transaction");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }
                     
                        SetCategoryTypeID(model);
                        model.LastModifiedBy = new Guid(User.Identity.GetUserId());
                        if (bankTransactionManagement.Update(model))
                        {
                            bankTransactionManagement.SaveCompanyBankTransaction();
                            logger.Info("Successfully Updated  Bank Transaction");
                            if (model.CategoryType == CompanyType.Inter)
                            {
                             
                                var reversal = bankTransactionManagement.GetInterCompanyReversalTransaction(model.ReferenceID, model.CompanyBankTransactionID);
                                if (reversal != null)
                                {   long companyId = model.CompanyBankID;
                                    long? toCompanyId = model.ToCompanyBankID;
                                    reversal.CompanyBankID = Convert.ToInt64(toCompanyId);
                                    reversal.ToCompanyBankID = companyId;
                                    reversal.TransactionType = model.TransactionType == "Receipt" ? "Payment" : "Receipt";
                                    reversal.PaymentType = model.PaymentType;
                                    reversal.Description = model.Description;
                                    reversal.Amount = model.Amount;
                                    reversal.TransactionDate = model.TransactionDate;
                                    reversal.TransactionStatus = model.TransactionStatus;
                                    reversal.CreatedOn = model.CreatedOn;
                                    reversal.LastModified = model.LastModified;
                                    reversal.ReceiptNumber = model.ReceiptNumber;
                                    reversal.LastModifiedBy = new Guid(User.Identity.GetUserId());
                                    bankTransactionManagement.Update(reversal);
                                    bankTransactionManagement.SaveCompanyBankTransaction();
                                    logger.Info("Successfully Updated Inter Company 2nd  Bank Transaction");
                                }
                            }

                              
                          
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
                            logger.Info("Not Updated  Bank Transaction");
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

                    logger.Info("Validation Issue Found  Bank Transaction");
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "Transaction"), MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        private string CheckCategoryValidation(CompanyBankTransaction model)
        {
            if ((model.CategoryType == CompanyType.Client) && (model.CategoryClientID == null))
            {
                return Resources.Messages.MSG_CATEGORY_CLIENT_REQUIRED;
            }
            else if ((model.CategoryType == CompanyType.Vendor) && (model.CategoryVendorID == null))
            {
                return Resources.Messages.MSG_CATEGORY_VENDOR_REQUIRED;
            }
            else if ((model.CategoryType == EntityType.Consultant) && (model.CategoryConsultantID == null))
            {
                return Resources.Messages.MSG_CATEGORY_CONSULTANT_REQUIRED;
            }
   
            return string.Empty;
        }

        private void SetCategoryTypeID(CompanyBankTransaction model)
        {
            if (model.CategoryType == CompanyType.Client)
            {
                model.CategoryReferenceID = model.CategoryClientID;
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
            try
            {
               
                if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_View))
                {
                    return RedirectToAction("Restricted", "Home");
                }

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(fromDate))
                    frdate = DateTime.Parse(fromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(toDate))
                    tdate = DateTime.Parse(toDate);



                logger.DebugFormat("Getting Bank Trasaction List with From Date [{0}] and To Date [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());

                CompanyBankTransactionVM transaction = new CompanyBankTransactionVM();
                transaction.DTObject = param;
                var list = bankTransactionManagement.GetAllBankTransactionByParam(transaction, frdate, tdate);

                logger.DebugFormat("Successfully Retrieve Bank Trasaction List Records [{2}] with From Date [{0}] and To Ddate [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString(), list.Count());
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
                logger.DebugFormat("Deleting Bank Transaction With BankTransactionId [{0}] ", id);


                if (!PermissionControl.CheckPermission(UserAppPermissions.BankTransaction_Delete))
                {
                    logger.Info("Don't have right to delete bank transaction.");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                CompanyBankTransaction transaction = bankTransactionManagement.GetCompanyBankTransaction(id);


                if (transaction.TransactionDate.Date < DateTime.Now.AddDays(-1).Date)
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_RECORD, MessageClass = MessageClass.Error, Response = false });


                transaction.IsDeleted = true;
                transaction.LastModifiedBy = new Guid(User.Identity.GetUserId());
                if (bankTransactionManagement.Delete(transaction))
                {
                    bankTransactionManagement.SaveCompanyBankTransaction();
                    logger.Info("Bank Transaction Successfully Deleted");
                    if (transaction.CategoryType == CompanyType.Inter)
                    {
                        var reversal = bankTransactionManagement.GetInterCompanyReversalTransaction(transaction.ReferenceID, transaction.CompanyBankTransactionID);
                        if(reversal != null)
                        {
                            reversal.LastModifiedBy = new Guid(User.Identity.GetUserId());
                            reversal.IsDeleted = true;
                            bankTransactionManagement.Delete(reversal);
                            bankTransactionManagement.SaveCompanyBankTransaction();
                            logger.Info("Successfully Deleted Inter Company 2nd  Bank Transaction");
                        }
                    }


                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
                }
                else
                {
                    logger.Info("Bank Transaction Not Deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }
    }
}