using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
using log4net;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web;

namespace CorporateAndFinance.Web.Controllers.Admin
{
    [Authorize]
    public class CardExpenseController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(CardExpenseController));

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUserCardExpenseManagement userCardExpenseManagement;
        private readonly IUserCardManagement userCardManagement;
        private readonly ICompanyManagement companyManagement;
        private readonly IConsultantManagement consultantManagement;



        private static readonly List<string> ColumnList = new List<string>() { UserCardExpenseEnum.Date,UserCardExpenseEnum.Receipt_Number, UserCardExpenseEnum.Account_Number,
            UserCardExpenseEnum.Description, UserCardExpenseEnum.Card_Member, UserCardExpenseEnum.Payment_Detail, UserCardExpenseEnum.Expense_Head, UserCardExpenseEnum.BE_NBE,
            UserCardExpenseEnum.Amount, UserCardExpenseEnum.Sales_Related, UserCardExpenseEnum.Transaction_Type, UserCardExpenseEnum.Client, UserCardExpenseEnum.Client_Number, UserCardExpenseEnum.Consultant, UserCardExpenseEnum.Consultant_Number, UserCardExpenseEnum.Remarks};
        private static readonly List<string> MandatoryColumnList = new List<string>() { UserCardExpenseEnum.Date,UserCardExpenseEnum.Receipt_Number, UserCardExpenseEnum.Account_Number,
            UserCardExpenseEnum.Description, UserCardExpenseEnum.Card_Member, UserCardExpenseEnum.Payment_Detail, UserCardExpenseEnum.Expense_Head, UserCardExpenseEnum.BE_NBE,
            UserCardExpenseEnum.Amount, UserCardExpenseEnum.Sales_Related, UserCardExpenseEnum.Transaction_Type, };

        public CardExpenseController(IUserCardExpenseManagement userCardExpenseManagement,IUserCardManagement userCardManagement, ICompanyManagement companyManagement, IConsultantManagement consultantManagement)
        {
            this.userCardExpenseManagement = userCardExpenseManagement;
            this.userCardManagement = userCardManagement;
            this.companyManagement = companyManagement;
            this.consultantManagement = consultantManagement;
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

        public ActionResult Index()
        {
            logger.Info("Card Expense Page Accessed");
            ViewBag.Title = "User Card Expense Listing";

            if (!PermissionControl.CheckPermission(UserAppPermissions.UserCardExpense_View))
            {
                logger.Info("Don't have rights to access  Card Expense Page");
                return RedirectToAction("Restricted", "Home");
            }


            return View();

        }


        [Route("Delete")]
        [HttpPost]
        
        public JsonResult Delete(long id)
        {
            try
            {
                logger.DebugFormat("Deleting Card Expense With CardExpenseID [{0}] ", id);


                if (!PermissionControl.CheckPermission(UserAppPermissions.UserCardExpense_Delete))
                {
                    logger.Info("Don't have right to delete Card Expense record");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                var userCard = userCardExpenseManagement.GetUserCardExpense(id);

                userCard.IsDeleted = true;
                if (userCardExpenseManagement.Delete(userCard))
                {
                    userCardExpenseManagement.SaveUserCardExpense();
                    logger.Info("Card Expense record Successfully Deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
                }
                else
                {
                    logger.Info("Card Expense record Not Deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        [Route("UploadNew")]
       
        public ActionResult UploadNew()
        {
            ViewBag.Title = "Upload New User Card Expense Sheet";

            if (!PermissionControl.CheckPermission(UserAppPermissions.UserCardExpense_Add))
            {
                return RedirectToAction("Restricted", "Home");
            }

            return PartialView("_UploadUserCardExpense");
        }


        [HttpPost]
        [Route("UserCardExpenseList")]
     
        public ActionResult UserCardExpenseList(DataTablesViewModel param, string fromDate, string toDate)
        {try { 
            if (!PermissionControl.CheckPermission(UserAppPermissions.UserCardExpense_View))
            { return RedirectToAction("Restricted", "Home"); }

            DateTime frdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fromDate))
                frdate = DateTime.Parse(fromDate);

            DateTime tdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(toDate))
                tdate = DateTime.Parse(toDate);

            logger.DebugFormat("Getting Card Expense List with From Date [{0}] and To Ddate [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());


            UserCardExpenseVM userCardExpenseVM = new UserCardExpenseVM();
            userCardExpenseVM.DTObject = param;
            var list = userCardExpenseManagement.GetAllUserCardExpensesByParam(userCardExpenseVM, frdate, tdate);

            logger.DebugFormat("Successfully Retrieve Card Expense List Records [{2}] with From Date [{0}] and To Ddate [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString(), list.Count());

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
        
        // use IEnumerable<HttpPostedFileBase> files if you want to post multiple files
        public ActionResult Uploader(HttpPostedFileBase expenseSheet)
        {
            var fileName = string.Empty;
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.UserCardExpense_Add))
                { return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false }); }

                var fileExtension = string.Empty;
                if (expenseSheet.ContentLength > 0)
                {
                    logger.DebugFormat("Uploading New Expense Sheet with Name [{0}]", expenseSheet.FileName);


                    fileName = Path.GetFileName(expenseSheet.FileName);
                    fileExtension = Path.GetExtension(expenseSheet.FileName);
                    string path = string.Format("{0}{1}{2}",Server.MapPath("~/UploadFiles/"), DateTime.Now.Ticks.ToString(),fileExtension);
                    expenseSheet.SaveAs(path);

                    logger.DebugFormat("Expense Sheet successfully uploaded with Name [{0}] and new Path [{1}]", expenseSheet.FileName, path);


                    var result =   ProcessExpenseSheet(path, fileName);

                    return Json(new { Message = result.Message, MessageClass = result.MessageType, Response = result.Response });
                }
                return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPLOAD_ERROR, fileName), MessageClass = MessageClass.Error, Response = false });
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPLOAD_ERROR, fileName), MessageClass = MessageClass.Error, Response = false });
            }
        }

        private ExcelFileProcess ProcessExpenseSheet(string path,string fileName)
        {
            logger.DebugFormat("Processing Expense Sheet Name [{0}]", fileName);

            DataSet ds =  ExcelFileReader.Read(path);
            DataTable dataTable = ds.Tables[0];
            dataTable = dataTable.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((Convert.ToString(field)).Trim(), string.Empty) == 0)).CopyToDataTable();
            if (dataTable.Rows.Count == 0)
            {
                logger.DebugFormat("No rows found in file [{0}]", fileName);

                return new ExcelFileProcess { Message = "No rows to insert.", Response = false, MessageType = MessageClass.Error };
            }

            string missingcolumn = ExcelFileReader.CheckAllColumnExist(dataTable, ColumnList);
            if(!string.IsNullOrWhiteSpace(missingcolumn))
            {
                logger.Debug(string.Format(Resources.Messages.MSG_GENERIC_COLUMN_MISSING, missingcolumn));

                return new ExcelFileProcess { Message = string.Format(Resources.Messages.MSG_GENERIC_COLUMN_MISSING, missingcolumn), Response = false, MessageType = MessageClass.Error };
            }

            string info = ExcelFileReader.ValidateData(dataTable, MandatoryColumnList);

            if(!string.IsNullOrWhiteSpace(info))
            {
                logger.Debug(info);
                return new ExcelFileProcess { Message = info, Response = false, MessageType = MessageClass.Error };
            }

            string errors =  ExcelFileReader.ValidateDataFormat(dataTable);
            if (!string.IsNullOrWhiteSpace(errors))
            {
                logger.Debug(errors);
                return new ExcelFileProcess { Message = errors, Response = false, MessageType = MessageClass.Error };
            }

            var userCards =  userCardManagement.GetAllUserCards();
            string accNumberError = ExcelFileReader.ValidateAccountNumber(dataTable, userCards);
            if (!string.IsNullOrWhiteSpace(accNumberError))
            {
                logger.Debug(accNumberError);
                return new ExcelFileProcess { Message = accNumberError, Response = false, MessageType = MessageClass.Error };
            }

            ExcelFileProcess process =  InsertData(dataTable, fileName, userCards);

            return process;
        }

        private ExcelFileProcess InsertData(DataTable dt, string fileName,IEnumerable<UserCard> userCards)
        {
            logger.DebugFormat("Inserting Expense Sheet Data from File [{0}]", fileName);
            foreach (DataRow row in dt.Rows)
            {
                UserCardExpense userCardExp = new UserCardExpense();

               // UserCard userCard = userCardManagement.GetUserCardByAccountNumber(Convert.ToString(row[UserCardExpenseEnum.Account_Number]));
                UserCard userCard = userCards.Where(x => x.CardNumber.ToLower() == Convert.ToString(row[UserCardExpenseEnum.Account_Number]).ToLower()).FirstOrDefault();
                if (userCard == null)
                {
                    logger.DebugFormat(string.Format(Resources.Messages.MSG_GENERIC_UPLOAD_USERCARD_ERROR, Convert.ToString(row[UserCardExpenseEnum.Account_Number]), Convert.ToString(row[UserCardExpenseEnum.Card_Member])));
                    return new ExcelFileProcess { Message = string.Format(Resources.Messages.MSG_GENERIC_UPLOAD_USERCARD_ERROR, Convert.ToString(row[UserCardExpenseEnum.Account_Number]), Convert.ToString(row[UserCardExpenseEnum.Card_Member])), Response = true, MessageType = MessageClass.Error };
                }
                userCardExp.UserCardID = userCard.UserCardID;

                userCardExp.Date = Convert.ToDateTime(row[UserCardExpenseEnum.Date]);
                userCardExp.RecieptNumber = Convert.ToString(row[UserCardExpenseEnum.Receipt_Number]);
                userCardExp.Description = Convert.ToString(row[UserCardExpenseEnum.Description]);
                userCardExp.BillType = Convert.ToString(row[UserCardExpenseEnum.BE_NBE]);
                userCardExp.Amount = Convert.ToDecimal(row[UserCardExpenseEnum.Amount]);
                userCardExp.IsSalesRelated = Convert.ToString(row[UserCardExpenseEnum.Sales_Related]).ToLower() == "yes";
                userCardExp.TransactionType = Convert.ToString(row[UserCardExpenseEnum.Transaction_Type]);
                userCardExp.Remarks = Convert.ToString(row[UserCardExpenseEnum.Remarks]);
                userCardExp.ExpenseHead = Convert.ToString(row[UserCardExpenseEnum.Expense_Head]);
                userCardExp.PaymentDetail = Convert.ToString(row[UserCardExpenseEnum.Payment_Detail]);

                if (!string.IsNullOrWhiteSpace(Convert.ToString(row[UserCardExpenseEnum.Client_Number])))
                {
                    Company company = companyManagement.GetCompanyByNumber(Convert.ToInt64(row[UserCardExpenseEnum.Client_Number]));
                    Guid companyId = Guid.NewGuid();
                    if (company == null)
                    {
                        logger.DebugFormat("Company Not Found Creating New Company With Name [{0}] and Number [{1}] and ID [{2}]", Convert.ToString(row[UserCardExpenseEnum.Client]), Convert.ToInt64(row[UserCardExpenseEnum.Client_Number]), companyId);

                        CreateNewCompany(Convert.ToString(row[UserCardExpenseEnum.Client]), Convert.ToInt64(row[UserCardExpenseEnum.Client_Number]), companyId);
                    }
                    else
                    {
                        companyId = company.CompanyID;
                    }
                    userCardExp.ClientID = companyId;
                }


                if (!string.IsNullOrWhiteSpace(Convert.ToString(row[UserCardExpenseEnum.Consultant_Number])))
                {
                    Consultant consultant = consultantManagement.GetConsultantByNumber(Convert.ToInt32(row[UserCardExpenseEnum.Consultant_Number]));
                    Guid consultantId = Guid.NewGuid();
                    if (consultant == null)
                    {
                        logger.DebugFormat("Consultant Not Found Creating New Consultant With Name [{0}] and Number [{1}] and ID [{2}]", Convert.ToString(row[UserCardExpenseEnum.Consultant]), Convert.ToInt64(row[UserCardExpenseEnum.Consultant_Number]), consultantId);
                        CreateNewConsultant(Convert.ToString(row[UserCardExpenseEnum.Consultant]), Convert.ToInt32(row[UserCardExpenseEnum.Consultant_Number]), consultantId);
                    }
                    else
                    {
                        consultantId = consultant.ConsultantID;
                    }

                    userCardExp.ConsultantID = consultantId;
                }
                userCardExpenseManagement.Add(userCardExp);

            }
            userCardExpenseManagement.SaveUserCardExpense();

            logger.DebugFormat("Successfully Inserted Expense Sheet Data from File [{0}]", fileName);
            return new ExcelFileProcess { Message = string.Format(Resources.Messages.MSG_GENERIC_UPLOAD_SUCCESS, fileName), Response = true, MessageType = MessageClass.Success };
        }

        private void CreateNewCompany(string clientName, long clientNumber,Guid clientId)
        {
            Company company = new Company();
            company.CompanyID = clientId;
            company.Name = clientName;
            company.CompanyNumber = clientNumber;
            company.CompanyType = CompanyType.Client;
            companyManagement.Add(company);
           
        }

        private void CreateNewConsultant(string consultantName, int consultantNumber, Guid consultantId)
        {
            Consultant consultant = new Consultant();
            consultant.ConsultantID = consultantId;
            consultant.FirstName = consultantName;
            consultant.ConsultantNumber = consultantNumber;
           
            consultantManagement.Add(consultant);

        }
    }
}