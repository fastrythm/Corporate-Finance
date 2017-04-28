using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Service.Interface;
using CorporateAndFinance.Web.Helper;
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

            if (!PermissionControl.CheckPermission(UserAppPermissions.PaymentAndCollectionReport_View))
            { return RedirectToAction("Restricted", "Home"); }

            if (model != null)
            {

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.FromDate))
                    frdate = DateTime.Parse(model.FromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.ToDate))
                    tdate = DateTime.Parse(model.ToDate);

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
                }
                ViewBag.ReportViewer = reportViewer;
            }
            return View(model);

        }

        public ActionResult InterCompanyReconciliation(ReportVM model)
        {
            ViewBag.Title = "InterCompany Reconciliation Report";

            if (!PermissionControl.CheckPermission(UserAppPermissions.InterCompanyReconciliation_View))
            { return RedirectToAction("Restricted", "Home"); }

            if (model != null)
            {

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.FromDate))
                    frdate = DateTime.Parse(model.FromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.ToDate))
                    tdate = DateTime.Parse(model.ToDate);

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
                }
                ViewBag.ReportViewer = reportViewer;
            }
            return View(model);

        }

        public ActionResult PaymentTypeWiseBankTransaction(ReportVM model)
        {
            ViewBag.Title = "Payment Wise Bank Transaction Report";

            if (!PermissionControl.CheckPermission(UserAppPermissions.PaymentTypeWiseBankTransactionReport_View))
            { return RedirectToAction("Restricted", "Home"); }

            if (model != null)
            {

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.FromDate))
                    frdate = DateTime.Parse(model.FromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.ToDate))
                    tdate = DateTime.Parse(model.ToDate);

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
                }
                ViewBag.ReportViewer = reportViewer;
            }
            return View(model);

        }


        public ActionResult BankReconciliationQBWise(ReportVM model)
        {
            ViewBag.Title = "QB AND Bank Wise Reconciliation Report";

            if (!PermissionControl.CheckPermission(UserAppPermissions.BankReconciliationQBWiseReport_View))
            { return RedirectToAction("Restricted", "Home"); }

            if (model != null)
            {

                DateTime frdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.FromDate))
                    frdate = DateTime.Parse(model.FromDate);

                DateTime tdate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(model.ToDate))
                    tdate = DateTime.Parse(model.ToDate);

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

                    var data = bankTransactionManagement.BankReconciliationQBWise(frdate, tdate);
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + "/Reports/BankReconciliation.rdlc";
                    reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp });
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("BankReconciliation", data));
                }
                catch (Exception ex)
                {
                }
                ViewBag.ReportViewer = reportViewer;
            }
            return View(model);

        }
    }
}