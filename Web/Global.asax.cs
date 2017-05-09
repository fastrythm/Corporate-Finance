
using CorporateAndFinance.Data;
using CorporateAndFinance.Web.App_Start;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            logger.Info("Application_Start is invoked");
            //DOMConfigurator.Configure();
            XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //   Database.SetInitializer(new CorporateAndFinanceSeedData());
           
        }
    }
}
