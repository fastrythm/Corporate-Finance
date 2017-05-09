using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
using log4net;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web;

namespace CorporateAndFinance.Web.Controllers.Admin
{
    public class ReportController : Controller
    {
        private static ILog logger = LogManager.GetLogger(typeof(ReportController));

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IBankTransactionManagement bankTransactionManagement;

        public ReportController(IBankTransactionManagement bankTransactionManagement)
        {
            this.bankTransactionManagement = bankTransactionManagement;
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

        public ActionResult PaymentAndCollection(ReportVM model)
        {
            ViewBag.Title = "Payment And Collection Report";
            logger.Info("Payment And Collection Report Access");
            if (!PermissionControl.CheckPermission(UserAppPermissions.PaymentAndCollectionReport_View))
            {
                logger.Info("Don't have rights to access Payment And Collection Report");
                return RedirectToAction("Restricted", "Home");
            }

            if (model != null)
            {

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.FromDate))
                    frdate = DateTime.Parse(model.FromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.ToDate))
                    tdate = DateTime.Parse(model.ToDate);

                logger.DebugFormat("Getting Payment And Collection Report with From Date [{0}] and To Date [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());

                ReportViewer reportViewer = new ReportViewer();
                try
                {
                    ReportParameter rp = new ReportParameter("ReportDates", string.Format("Date: {0} - {1}", frdate.ToShortDateString(), tdate.ToShortDateString()));
                    reportViewer.ProcessingMode = ProcessingMode.Local;
                    reportViewer.SizeToReportContent = true;
                    reportViewer.Width = Unit.Percentage(100);
                    reportViewer.Height = Unit.Percentage(100);
                    reportViewer.ZoomMode = ZoomMode.Percent;
                    reportViewer.ShowZoomControl = true;
                    reportViewer.AsyncRendering = false;
                    reportViewer.ShowPrintButton = true;

                    var data = bankTransactionManagement.GetCollectionAndPaymentDetails(frdate, tdate);
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + "/Reports/BankTransaction.rdlc";
                    reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp });
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBankTransaction", data));
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                }
                ViewBag.ReportViewer = reportViewer;
                logger.Info("Payment And Collection Report Successfully Accessed");
            }
            return View(model);

        }

        public ActionResult InterCompanyReconciliation(ReportVM model)
        {
            ViewBag.Title = "InterCompany Reconciliation Report";
            logger.Info("Inter Company Reconciliation Report Access");
            if (!PermissionControl.CheckPermission(UserAppPermissions.InterCompanyReconciliation_View))
            {
                logger.Info("Don't have rights to access Inter Company Reconciliation Report");
                return RedirectToAction("Restricted", "Home");
            }

            if (model != null)
            {

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.FromDate))
                    frdate = DateTime.Parse(model.FromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.ToDate))
                    tdate = DateTime.Parse(model.ToDate);

                logger.DebugFormat("Getting Inter Company Reconciliation Report with From Date [{0}] and To Date [{1}]", frdate.ToShortDateString(), tdate.ToShortDateString());

                ReportViewer reportViewer = new ReportViewer();
                try
                {
                    ReportParameter rp = new ReportParameter("ReportDates", string.Format("Date: {0} - {1}", frdate.ToShortDateString(), tdate.ToShortDateString()));
                    reportViewer.ProcessingMode = ProcessingMode.Local;
                    reportViewer.SizeToReportContent = true;
                    reportViewer.Width = Unit.Percentage(100);
                    reportViewer.Height = Unit.Percentage(100);
                    reportViewer.ZoomMode = ZoomMode.Percent;
                    reportViewer.ShowZoomControl = true;
                    reportViewer.AsyncRendering = false;
                    reportViewer.ShowPrintButton = true;

                    var data = bankTransactionManagement.InterCompanyReconciliation(frdate, tdate);
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + "/Reports/CompanyWiseBankRecon.rdlc";
                    reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp });
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetCompanyReconciliation", data));
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                }
                ViewBag.ReportViewer = reportViewer;
                logger.Info("Inter Company Reconciliation Report Successfully Accessed");
            }
            return View(model);

        }

        public ActionResult PaymentTypeWiseBankTransaction(ReportVM model)
        {
            ViewBag.Title = "Payment Wise Bank Transaction Report";
            logger.Info("Payment Wise Bank Transaction Report Access");
            if (!PermissionControl.CheckPermission(UserAppPermissions.PaymentTypeWiseBankTransactionReport_View))
            {
                logger.Info("Don't have rights to access Payment Wise Bank Transaction Report");
                return RedirectToAction("Restricted", "Home");
            }

            if (model != null)
            {

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.FromDate))
                    frdate = DateTime.Parse(model.FromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.ToDate))
                    tdate = DateTime.Parse(model.ToDate);

                if (string.IsNullOrEmpty(model.PaymentType))
                    model.PaymentType =  "ach,wire";

                logger.DebugFormat("Getting Payment Wise Bank Transaction Report with From Date [{0}] and To Date [{1}] and Payment Type [{2}]", frdate.ToShortDateString(), tdate.ToShortDateString(), model.PaymentType);


                ReportViewer reportViewer = new ReportViewer();
                try
                {
                    ReportParameter rp = new ReportParameter("ReportDates", string.Format("Date: {0} - {1}", frdate.ToShortDateString(), tdate.ToShortDateString()));
                    reportViewer.ProcessingMode = ProcessingMode.Local;
                    reportViewer.SizeToReportContent = true;
                    reportViewer.Width = Unit.Percentage(100);
                    reportViewer.Height = Unit.Percentage(100);
                    reportViewer.ZoomMode = ZoomMode.Percent;
                    reportViewer.ShowZoomControl = true;
                    reportViewer.AsyncRendering = false;
                    reportViewer.ShowPrintButton = true;

                    var data = bankTransactionManagement.PaymentTypeWiseBankTransaction(frdate, tdate, model.PaymentType);
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + "/Reports/BankTransactionPaymentTypeWise.rdlc";
                    reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp });
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("BankTransactionPaymentTypeWise", data));
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                }
                ViewBag.ReportViewer = reportViewer;
                logger.Info("Payment Wise Bank Transaction Report Successfully Accessed");
            }
            return View(model);

        }


        public ActionResult BankReconciliationQBWise(ReportVM model)
        {
            ViewBag.Title = "QB AND Bank Wise Reconciliation Report";
            logger.Info("QB AND Bank Wise Reconciliation Report Access");
            if (!PermissionControl.CheckPermission(UserAppPermissions.BankReconciliationQBWiseReport_View))
            {
                logger.Info("Don't have rights to access QB AND Bank Wise Reconciliation Report");
                return RedirectToAction("Restricted", "Home");
            }

            if (model != null)
            {

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.FromDate))
                    frdate = DateTime.Parse(model.FromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.ToDate))
                    tdate = DateTime.Parse(model.ToDate);

                logger.DebugFormat("Getting QB AND Bank Wise Reconciliation Report with From Date [{0}] and To Date [{1}] ", frdate.ToShortDateString(), tdate.ToShortDateString());

                ReportViewer reportViewer = new ReportViewer();
                try
                {
                    ReportParameter rp = new ReportParameter("ReportDates", string.Format("Date: {0}", frdate.ToShortDateString()));
                    reportViewer.ProcessingMode = ProcessingMode.Local;
                    reportViewer.SizeToReportContent = true;
                    reportViewer.Width = Unit.Percentage(100);
                    reportViewer.Height = Unit.Percentage(100);
                    reportViewer.ZoomMode = ZoomMode.Percent;
                    reportViewer.ShowZoomControl = true;
                    reportViewer.AsyncRendering = false;
                    reportViewer.ShowPrintButton = true;

                    var data = bankTransactionManagement.BankReconciliationQBWise(frdate, tdate);
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + "/Reports/BankReconciliation.rdlc";
                    reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp });
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("BankReconciliation", data));
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                }
                ViewBag.ReportViewer = reportViewer;
                logger.Info("QB AND Bank Wise Reconciliation Report Successfully Accessed");
            }
            return View(model);

        }
    }
}