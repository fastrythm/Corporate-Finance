using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web;

namespace CorporateAndFinance.Web.Controllers.Admin
{
    [Authorize]
    public class UserExpenseController : Controller
    {
        #region Variables

        private static ILog logger = LogManager.GetLogger(typeof(BankPositionController));
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUserExpenseManagement userExpenseManagement;
        private readonly IDepartmentManagement departmentManagement;
        private readonly ICompanyManagement companyManagement;
        private readonly IUserAllocationManagement userAllocationManagement;
        private readonly IUserAllocationBillingManagement userAllocationBillingManagement;

        private static readonly List<string> ColumnList = new List<string>() { UserExpenseMandatoryColumn.Department, UserExpenseMandatoryColumn.Department_Type, UserExpenseMandatoryColumn.Employee_Number, UserExpenseMandatoryColumn.Employee_Name, UserExpenseType.Monthly_Salary, UserExpenseType.Monthly_Salary2,
        UserExpenseType.EOBI_Employer , UserExpenseType.PF_Employer,UserExpenseType.Mobile_Allowance,
        UserExpenseType.Bonus,UserExpenseType.Meal_Reimbursement,UserExpenseType.Transportation,UserExpenseType.Leave_Encashment,
        UserExpenseType.Incentive_PSM,UserExpenseType.Health_Insurance,UserExpenseType.Medical_OPD,UserExpenseType.Billable_Salary_PKR,UserExpenseType.Billable_Salary_USD};

        private static readonly List<string> MandatoryColumnList = new List<string>() { UserExpenseMandatoryColumn.Department, UserExpenseMandatoryColumn.Department_Type, UserExpenseMandatoryColumn.Employee_Number, UserExpenseMandatoryColumn.Employee_Name };


        private static readonly List<string> ValidateColumnList = new List<string>() { UserExpenseType.Monthly_Salary, UserExpenseType.Monthly_Salary2,
        UserExpenseType.EOBI_Employer , UserExpenseType.PF_Employer,UserExpenseType.Mobile_Allowance,
        UserExpenseType.Bonus,UserExpenseType.Meal_Reimbursement,UserExpenseType.Transportation,UserExpenseType.Leave_Encashment,
        UserExpenseType.Incentive_PSM,UserExpenseType.Health_Insurance,UserExpenseType.Medical_OPD,UserExpenseType.Billable_Salary_PKR,UserExpenseType.Billable_Salary_USD};

        #endregion

        public UserExpenseController(IUserExpenseManagement userExpenseManagement, IDepartmentManagement departmentManagement, ICompanyManagement companyManagement, IUserAllocationManagement userAllocationManagement, IUserAllocationBillingManagement userAllocationBillingManagement)
        {
            this.userExpenseManagement = userExpenseManagement;
            this.departmentManagement = departmentManagement;
            this.companyManagement = companyManagement;
            this.userAllocationManagement = userAllocationManagement;
            this.userAllocationBillingManagement = userAllocationBillingManagement;
        }

        public UserExpenseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
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
        private ApplicationRoleManager _roleManager;
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



        // GET: User Expense
        public ActionResult Index()
        {
            logger.Info("User Expense Page Access");
            ViewBag.Title = "User Expense Listing";

            if (!PermissionControl.CheckPermission(UserAppPermissions.ALP_Costing_View))
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

            if (!PermissionControl.CheckPermission(UserAppPermissions.ALP_Costing_Add))
            {
                return RedirectToAction("Restricted", "Home");
            }

            return PartialView("_UploadUserExpense");
        }

        [Route("Delete")]
        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                logger.DebugFormat("Deleting User Expense With CardExpenseID [{0}] ", id);


                if (!PermissionControl.CheckPermission(UserAppPermissions.ALP_Costing_Delete))
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
                    var billing = userAllocationBillingManagement.GetUserAllocationBillingByExpenseId(userExpense.UserExpenseID);
                    if(billing != null )
                    {
                        billing.IsDeleted = true;
                        userAllocationBillingManagement.Update(billing);
                    }
                    userAllocationBillingManagement.SaveUserAllocationBilling();
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
                if (!PermissionControl.CheckPermission(UserAppPermissions.ALP_Costing_View))
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

        [HttpPost]

        // use IEnumerable<HttpPostedFileBase> files if you want to post multiple files
        public ActionResult Uploader(HttpPostedFileBase expenseSheet, string expenseMonth)
        {
            var fileName = string.Empty;
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.ALP_Costing_Add))
                { return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false }); }



                if (string.IsNullOrWhiteSpace(expenseMonth))
                {
                    return Json(new { Message = "Expense Month Year Required", MessageClass = MessageClass.Error, Response = false });
                }

                try
                {
                    DateTime.Parse(expenseMonth);
                }
                catch (Exception ex)
                {
                    return Json(new { Message = "Expense Month/Year is invalid format", MessageClass = MessageClass.Error, Response = false });
                }


                DateTime expenseDate = DateTime.Parse(expenseMonth);
                var fileExtension = string.Empty;
                if (expenseSheet.ContentLength > 0)
                {
                    logger.DebugFormat("Uploading New User Expense Sheet with Name [{0}]", expenseSheet.FileName);


                    fileName = Path.GetFileName(expenseSheet.FileName);
                    fileExtension = Path.GetExtension(expenseSheet.FileName);
                    string path = string.Format("{0}{1}_{2}{3}", Server.MapPath("~/UploadFiles/"), fileName, DateTime.Now.Ticks.ToString(), fileExtension);
                    expenseSheet.SaveAs(path);

                    logger.DebugFormat("User Expense Sheet successfully uploaded with Name [{0}] and new Path [{1}]", expenseSheet.FileName + "_" + expenseMonth, path);


                    var result = ProcessExpenseSheet(path, fileName, expenseDate);

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

        private ExcelFileProcess ProcessExpenseSheet(string path, string fileName, DateTime expenseDate)
        {
            logger.DebugFormat("Processing User Expense Sheet Name [{0}]", fileName);

            DataSet ds = ExcelFileReader.Read(path);
            DataTable table = ds.Tables[0];
            table = table.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((Convert.ToString(field)).Trim(), string.Empty) == 0)).CopyToDataTable();
            if (table.Rows.Count  == 0)
            {
                logger.DebugFormat("No rows found in file [{0}]", fileName);

                return new ExcelFileProcess { Message = "No rows to insert.", Response = false, MessageType = MessageClass.Error };
            }

            string missingcolumn = ExcelFileReader.CheckAllColumnExist(table, ColumnList);
            if (!string.IsNullOrWhiteSpace(missingcolumn))
            {
                logger.Debug(string.Format(Resources.Messages.MSG_GENERIC_COLUMN_MISSING, missingcolumn));

                return new ExcelFileProcess { Message = string.Format(Resources.Messages.MSG_GENERIC_COLUMN_MISSING, missingcolumn), Response = false, MessageType = MessageClass.Error };
            }

            string info = ExcelFileReader.ValidateData(table, MandatoryColumnList);

            if (!string.IsNullOrWhiteSpace(info))
            {
                logger.Debug(info);
                return new ExcelFileProcess { Message = info, Response = false, MessageType = MessageClass.Error };
            }

            string errors = ExcelFileReader.ValidateAmount(table, ValidateColumnList);
            if (!string.IsNullOrWhiteSpace(errors))
            {
                logger.Debug(errors);
                return new ExcelFileProcess { Message = errors, Response = false, MessageType = MessageClass.Error };
            }

            var department = departmentManagement.GetAllDepartments();
            var allocation = userAllocationManagement.GetAllUsersActiveAllocations();
            var users = UserManager.Users.Where(x => !x.IsDeleted).ToList();
            errors = ExcelFileReader.ValidateDepartmentAndUsersAndAllocation(table, department, users, allocation);
            if (!string.IsNullOrWhiteSpace(errors))
            {
                logger.Debug(errors);
                return new ExcelFileProcess { Message = errors, Response = false, MessageType = MessageClass.Error };
            }

            ExcelFileProcess process = InsertData(table, fileName, users, expenseDate, allocation);

            return process;
        }

        private ExcelFileProcess InsertData(DataTable dt, string fileName, List<ApplicationUser> users, DateTime expenseDate, IEnumerable<UserAllocation> userAllocation)
        {
            try
            {
                logger.DebugFormat("Checking existing expense date record exist");
                var expenses = userExpenseManagement.GetAllExpenseByDate(expenseDate);
                logger.DebugFormat("Successfully retrieve user expenses [{0}] record  regarding date [{1}]", expenses.Count(), expenseDate.ToShortDateString());
                if(expenses.Count() > 0)
                {
                    logger.DebugFormat("Deleting existing user allocation billing by date [{0}] records ", expenseDate.ToShortDateString());
                    userAllocationBillingManagement.DeleteAllByDate(expenseDate);
                    userAllocationBillingManagement.SaveUserAllocationBilling();
                    logger.DebugFormat("Successfully deleted user allocation billing by expense date [{0}] records ", expenseDate.ToShortDateString());

                    logger.DebugFormat("Deleting existing expense by date [{0}] records ", expenseDate.ToShortDateString());
                    userExpenseManagement.DeleteAllByDate(expenseDate);
                    userExpenseManagement.SaveUserExpense();
                    logger.DebugFormat("Successfully deleted existing expense by date [{0}] records ", expenseDate.ToShortDateString());
                  
                }



                logger.DebugFormat("Inserting User Expense Sheet Data from File [{0}]", fileName);
                foreach (DataRow row in dt.Rows)
                {
                    UserExpense userExp = new UserExpense();
                    var department = departmentManagement.GetDepartment(Convert.ToString(row[UserExpenseMandatoryColumn.Department])) ;
                    if (department == null)
                    {
                        logger.DebugFormat(string.Format(Resources.Messages.MSG_GENERIC_UPLOAD_DEPARTMENT_NOT_FOUND, Convert.ToString(row[UserExpenseMandatoryColumn.Department])));
                        department = CreateNewDepartment(Convert.ToString(row[UserExpenseMandatoryColumn.Department]), Convert.ToString(row[UserExpenseMandatoryColumn.Department_Type]));
                    }

                    ApplicationUser user = users.Where(x => x.EmployeeNumber == Convert.ToString(row[UserExpenseMandatoryColumn.Employee_Number])).FirstOrDefault();
                    if (user == null)
                    {
                        logger.DebugFormat(string.Format(Resources.Messages.MSG_GENERIC_UPLOAD_EMP_NUMBER_NOT_FOUND, Convert.ToString(row[UserExpenseMandatoryColumn.Employee_Number]), Convert.ToString(row[UserExpenseMandatoryColumn.Employee_Name])));
                        user = CreateNewUser(Convert.ToString(row[UserExpenseMandatoryColumn.Employee_Number]), Convert.ToString(row[UserExpenseMandatoryColumn.Employee_Name]), department.DepartmentID);
                    }

                    var expenseId = Guid.NewGuid();
                    userExp.UserExpenseID = expenseId;
                    userExp.DepartmentID = department.DepartmentID;
                    userExp.UserID = user.Id;
                    userExp.ExpenseDate = expenseDate;
                    userExp.IsActive = true;
                    userExp.CreatedBy = new Guid(User.Identity.GetUserId());

                    userExp.Monthly_Salary = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Monthly_Salary])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Monthly_Salary]);
                    userExp.Monthly_Salary2 = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Monthly_Salary2])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Monthly_Salary2]);
                    userExp.EOBI_Employer = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.EOBI_Employer])) ? 0 : Convert.ToDecimal(row[UserExpenseType.EOBI_Employer]);
                    userExp.PF_Employer = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.PF_Employer])) ? 0 : Convert.ToDecimal(row[UserExpenseType.PF_Employer]);
                    userExp.Mobile_Allowance = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Mobile_Allowance])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Mobile_Allowance]);
                    userExp.Bonus = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Bonus])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Bonus]);
                    userExp.Meal_Reimbursement = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Meal_Reimbursement])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Meal_Reimbursement]);
                    userExp.Transportation = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Transportation])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Transportation]);
                    userExp.Leave_Encashment = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Leave_Encashment])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Leave_Encashment]);
                    userExp.Incentive_PSM = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Incentive_PSM])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Incentive_PSM]);
                    userExp.Health_Insurance = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Health_Insurance])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Health_Insurance]);
                    userExp.Medical_OPD = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Medical_OPD])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Medical_OPD]);
                    userExp.Billable_Salary_PKR = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Billable_Salary_PKR])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Billable_Salary_PKR]);
                    userExp.Billable_Salary_USD = string.IsNullOrEmpty(Convert.ToString(row[UserExpenseType.Billable_Salary_USD])) ? 0 : Convert.ToDecimal(row[UserExpenseType.Billable_Salary_USD]);
                    userExp.SerialNumber = DateTime.Now.Ticks.ToString();

                   
                    userExpenseManagement.Add(userExp);
                    userExpenseManagement.SaveUserExpense();

                    AddUserAlloctionBilling(expenseId, user.Id, userAllocation, userExp.Billable_Salary_PKR, userExp.Billable_Salary_USD, expenseDate);

                }
               

                logger.DebugFormat("Successfully Inserted User Expense Sheet Data from File [{0}]", fileName);
                return new ExcelFileProcess { Message = string.Format(Resources.Messages.MSG_GENERIC_UPLOAD_SUCCESS, fileName), Response = true, MessageType = MessageClass.Success };
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return new ExcelFileProcess { Message = string.Format(Resources.Messages.MSG_GENERIC_UPDATE_FAILED, fileName), Response = false, MessageType = MessageClass.Error };
            }
        }

        private bool AddUserAlloctionBilling(Guid userExpenseID,string userId, IEnumerable<UserAllocation> userAllocation, decimal billable_Salary_PKR, decimal billable_Salary_USD,DateTime expenseDate)
        {
            try
            {
           
            logger.DebugFormat("Adding user allocation billing by UserExpenseId[{0}], UserID[{1}], SalaryInPKR[{2], SalaryInUSD[{3}] , ExpenseDate[{4}]",  userExpenseID,  userId,  billable_Salary_PKR,  billable_Salary_USD,  expenseDate.ToShortDateString());

                if (userAllocation != null && userAllocation.Count() > 0)
                {
                    var allocation = userAllocation.Where(x => x.UserID == userId && x.IsActive && x.Status.Equals(RequestStatus.Approved)).ToList();
                    if (allocation != null)
                    {
                        foreach (var allocate in allocation)
                        {
                            UserAllocationBilling userBilling = new UserAllocationBilling();
                            userBilling.DepartmentID = allocate.DepartmentID;
                            userBilling.UserID = userId;
                            userBilling.BillingDate = expenseDate;
                            userBilling.UserExpenseID = userExpenseID;
                            userBilling.Percentage = allocate.Percentage;
                            userBilling.CreatedBy = new Guid(User.Identity.GetUserId());
                            
                            if (billable_Salary_PKR > 0)
                            {
                                userBilling.AmountPKR = (billable_Salary_PKR / 100) * allocate.Percentage;
                            }

                            if (billable_Salary_USD > 0)
                            {
                                userBilling.AmountUSD = (billable_Salary_USD / 100) * allocate.Percentage;
                            }

                            userAllocationBillingManagement.Add(userBilling);
                        }
                        userAllocationBillingManagement.SaveUserAllocationBilling();
                        logger.DebugFormat("User Allocation Billing Successfully Saved");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return false;
            }
        }
 

        private ApplicationUser CreateNewUser(string employeeNumber, string employeeName, long departmentID)
        {
            try
            {
                logger.DebugFormat("Creating new user with name [{0}] and employee number  [{1}] and departmentID [{2}]", employeeName, employeeNumber, departmentID);



                ApplicationUser user = new ApplicationUser();

                string uniqueEmail = employeeName.Replace(" ", "") + DateTime.Now.Ticks + "@arthurlawrence.net";

                user.FirstName = employeeName;
                user.Email = uniqueEmail;
                user.UserName = uniqueEmail;
                user.EmployeeNumber = employeeNumber;
               // user.DepartmentID = departmentID;

                var isSaved = UserManager.Create(user, "Password123");

                if (isSaved.Succeeded)
                {
                    if (!RoleManager.RoleExists("Employee"))
                    {
                        var role = new IdentityRole();
                        role.Name = "Employee";
                        RoleManager.Create(role);
                    }

                    var result = UserManager.AddToRoles(user.Id, "Employee");
                    logger.DebugFormat("Successfully created new user with name [{0}] and employee number [{1}] and  UserID [{2}] ", employeeNumber, employeeName, user.Id);
                }
                else
                { logger.DebugFormat("User Not Create Due to Error [{0}]", isSaved.Errors); }

                return user;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        private Department CreateNewDepartment(string departmentName, string departmentType)
        {
            try
            {
                logger.DebugFormat("Creating new department with name [{0}] and type [{1}]", departmentName, departmentType);
                Department dept = new Department();
                dept.Name = departmentName;
                dept.DepartmentType = departmentType;
                var company = companyManagement.GetCompanyByName("Arthur Lawrence Pakistan");

                dept.CompanyID = company.CompanyID;
                departmentManagement.Add(dept);
                departmentManagement.SaveDepartment();
                logger.DebugFormat("Successfully created new department [{0}]", departmentName);
                return dept;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
             }
        }
    }
}