﻿using CorporateAndFinance.Core.Helper.Structure;
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
    }
}