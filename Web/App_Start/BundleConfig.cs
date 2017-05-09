using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/Bundles/Themes/Finance-1/Javascript").Include(
                        "~/Themes/finance-1/js/jquery.js", 
                         "~/Themes/finance-1/js/bootstrap.js",
                         "~/Themes/finance-1/js/jquery-ui.js",
                         "~/Themes/finance-1/js/app.js",
                          "~/Themes/finance-1/js/jquery.metro-btn.js",
                          "~/Themes/finance-1/js/jquery.dataTables.min.js",
                          "~/Themes/finance-1/js/dataTables.bootstrap.js"
                        ));

            bundles.Add(new ScriptBundle("~/Bundles/Scripts/Javascript").Include(
                        "~/Scripts/DataTables/jszip.min.js",
                       "~/Scripts/DataTables/dataTables.buttons.min.js",
                       "~/Scripts/DataTables/buttons.html5.min.js",
                       "~/Scripts/DataTables/jquery.dataTables.rowGrouping.js"
                      ));


          var cssBundle =  new StyleBundle("~/Bundles/Themes/Finance-1/css").Include(
                   "~/Themes/finance-1/css/jquery.dataTables.yadcf.css",
                     "~/Themes/finance-1/css/colors.css",
                     "~/Themes/finance-1/css/font-awesome.css",
                      "~/Themes/finance-1/css/bootstrap.css",
                      "~/Themes/finance-1/css/jquery-ui.css",
                      "~/Themes/finance-1/css/buttons.bootstrap.min.css",
                          "~/Content/dataTables/css/dataTables.bootstrap.css",
                          "~/Themes/finance-1/css/style.css",
                           "~/Themes/finance-1/css/AdminLTE.css",
                           "~/Themes/finance-1/css/skin-red.css",
                        "~/Themes/finance-1/css/google_Open_San_Verdana.css",
                        "~/Themes/finance-1/css/Interacao.css", "~/Content/DataTables/css/buttons.dataTables.css",
                        "~/Themes/finance-1/css/jq-metro.css");
             
            bundles.Add(cssBundle);

 
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                         "~/Scripts/jquery.validate.min.js",
                         "~/Scripts/jquery.fileupload.js",
                       "~/Scripts/jquery.validate.unobtrusive.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/Bundles/jquery-admin-custom").Include(
                        "~/Scripts/jquery.gritter.min.js",
                         "~/Scripts/Custom/GenericDTBind.js",
                          "~/Scripts/Custom/shared.js",
                           "~/Scripts/Custom/Messages.js"
                         )
                        );


            BundleTable.EnableOptimizations = false;

        }
    }
}
