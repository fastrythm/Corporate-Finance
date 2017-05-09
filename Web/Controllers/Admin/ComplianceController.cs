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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web;

namespace CorporateAndFinance.Web.Controllers.Admin
{
    [Authorize]
    public class ComplianceController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(ComplianceController));

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IComplianceManagement complianceManagement;
        private readonly ICompanyManagement companyManagement;

        public ComplianceController(IComplianceManagement complianceManagement, ICompanyManagement companyManagement)
        {
            // UserManager = userManager;
            // SignInManager = signInManager;
            this.complianceManagement = complianceManagement;
            this.companyManagement = companyManagement;
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

        // GET: Compliance
        public ActionResult Index()
        {
            ViewBag.Title = "Compliance Listing";
            logger.Info("Compliance Page Access");
            if (!PermissionControl.CheckPermission(UserAppPermissions.Compliance_View))
            {
                logger.Info("Don't have rights to access  Compliance Page");
                return RedirectToAction("Restricted", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("ComplianceList")]
        public ActionResult ComplianceList(DataTablesViewModel param, string fromDate, string toDate)
        {
            try
            {
                if (!PermissionControl.CheckPermission(UserAppPermissions.Compliance_View))
                { return RedirectToAction("Restricted", "Home"); }

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(fromDate))
                    frdate = DateTime.Parse(fromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(toDate))
                    tdate = DateTime.Parse(toDate);

                logger.DebugFormat("Getting Company Compliance List with From Date [{0}] and To Date [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());

                CompanyComplianceVM compliance = new CompanyComplianceVM();
                compliance.DTObject = param;
                var list = complianceManagement.GetAllCompliancesByParam(compliance, frdate, tdate);
                logger.DebugFormat("Successfully Retrieve  Company Compliance List Records [{2}] with From Date [{0}] and To Ddate [1]", frdate.ToShortDateString(), tdate.ToShortDateString(), list.Count());

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
                logger.DebugFormat("Deleting Compliance With ComplianceID [{0}] ", id);


                if (!PermissionControl.CheckPermission(UserAppPermissions.Compliance_Delete))
                {
                    logger.Info("Don't have right to delete compliance record");
                    return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                }

                CompanyCompliance compliance = complianceManagement.GetCompliance(id);


                if (complianceManagement.Delete(compliance))
                {
                    complianceManagement.SaveCompliance();
                    logger.Info("Compliance record Successfully Deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_SUCCESS, MessageClass = MessageClass.Success, Response = true });
                }
                else
                {
                    logger.Info("Compliance record not deleted");
                    return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex )
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_DELETE_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }

        [Route("AddEdit")]
        public ActionResult AddEdit(int id)
        {
            ViewBag.Title = "Add/Update Compliance";

            var compliance = complianceManagement.GetCompliance(id);
            if (compliance == null)
                compliance = new CompanyCompliance();

            compliance.Companies = companyManagement.GetAllCompanies();
            return PartialView("_AddEditCompliance", compliance);
        }


        [Route("AddEdit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult AddEdit(CompanyCompliance model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    model.Remarks1UserID = new Guid(User.Identity.GetUserId());
                    if (model.CompanyComplianceID == 0)
                    {
                        logger.DebugFormat("Add Compliance with CompanyID [{0}],  Date [{1}],  Description [{2}], FilePath [{3}],  LegalAuthorityName [{4}] , Status [{5}] , Amount[{6}]",
                            model.CompanyID, model.Date, model.Description, model.FilePath,model.LegalAuthorityName,model.Status,model.Amount);

                        if (!PermissionControl.CheckPermission(UserAppPermissions.Compliance_Add))
                        {
                            logger.Info("Don't have rights to add  Compliance");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        if (complianceManagement.Add(model))
                        {
                            complianceManagement.SaveCompliance();
                            logger.Info("Successfully Saved Company Compliance");
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_SUCCESS, "Compliance"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            if (model != null)
                                model.Companies = companyManagement.GetAllCompanies();

                            logger.Info("Company Compliance Not Saved");
                            return Json(new { Message = string.Format("Validation Failded", "Compliance"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                    else
                    {
                        logger.DebugFormat("Update Compliance with CompanyID [{0}],  Date [{1}],  Description [{2}], FilePath [{3}],  LegalAuthorityName [{4}] , Status [{5}] , Amount[{6}] , CompanyComplianceID [{7}]",
                     model.CompanyID, model.Date, model.Description, model.FilePath, model.LegalAuthorityName, model.Status, model.Amount,model.CompanyComplianceID);

                        if (!PermissionControl.CheckPermission(UserAppPermissions.Compliance_Edit))
                        {
                            logger.Info("Don't have rights to update  Compliance");
                            return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
                        }

                        if (complianceManagement.Update(model))
                        {
                            complianceManagement.SaveCompliance();
                            logger.Info("Successfully Updated Company Compliance");
                            return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_UPDATE_SUCCESS, "Compliance"), MessageClass = MessageClass.Success, Response = true });
                        }
                        else
                        {
                            if (model != null)
                                model.Companies = companyManagement.GetAllCompanies();

                            logger.Info("Not Updated Company Compliance");
                            return Json(new { Message = string.Format("Validation Failded", "Compliance"), MessageClass = MessageClass.Error, Response = false });
                        }
                    }
                }
                else
                {
                    if (model != null)
                        model.Companies = companyManagement.GetAllCompanies();
                    return Json(new { Message = string.Format(Resources.Messages.MSG_GENERIC_ADD_FAILED, "Compliance"), MessageClass = MessageClass.Error, Response = false });
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return Json(new { Message = Resources.Messages.MSG_GENERIC_FAILED, MessageClass = MessageClass.Error, Response = false });
            }
        }



        public ActionResult Uploader()
        {
  
            if (!PermissionControl.CheckPermission(UserAppPermissions.Compliance_Edit) || !PermissionControl.CheckPermission(UserAppPermissions.Compliance_Add))
            {
                return Json(new { Message = Resources.Messages.MSG_RESTRICTED_ACCESS, MessageClass = MessageClass.Error, Response = false });
            }

            string fname = string.Empty;
            string tempFname = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];


                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            tempFname = fname;
                        }
                        else
                        {
                            fname = file.FileName;
                            tempFname = fname;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/UploadFiles/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(String.Format("/UploadFiles/{0}", tempFname));

                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }

    }

    public static class ExtensionMethods
    {
        public static string RelativePath(this HttpServerUtility srv, string path)
        {
            return path.Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/");
        }
    }

}