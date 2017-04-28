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
                          "~/Themes/finance-1/js/dataTables.bootstrap.js",
                          "~/Scripts/DataTables/jszip.min.js",
                         "~/Scripts/DataTables/dataTables.buttons.min.js",
                         "~/Scripts/DataTables/buttons.html5.min.js",
                         "~/Scripts/DataTables/jquery.dataTables.rowGrouping.js"
                        ));


            bundles.Add(new StyleBundle("~/Bundles/Themes/Finance-1/css").Include(
                   "~/Themes/finance-1/css/jquery.dataTables.yadcf.css",
                     "~/Themes/finance-1/css/colors.css",
                     "~/Themes/finance-1/css/font-awesome.css",
                      "~/Themes/finance-1/css/bootstrap.css",
                      "~/Themes/finance-1/css/jquery-ui.css",
                      "~/Themes/finance-1/css/buttons.bootstrap.min.css",
                         //   "~/Themes/finance-1/css/datatables.min.css",
                          "~/Content/dataTables/css/dataTables.bootstrap.css",
                          "~/Themes/finance-1/css/style.css",
                           "~/Themes/finance-1/css/AdminLTE.css",
                           "~/Themes/finance-1/css/skin-red.css",
                        "~/Themes/finance-1/css/google_Open_San_Verdana.css",
                        "~/Themes/finance-1/css/Interacao.css","~/Content/DataTables/css/buttons.dataTables.css",
                        "~/Themes/finance-1/css/jq-metro.css"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                       "~/Scripts/jquery-ui-1.12.1.min.js"));
 

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/Bundles/jquery-DT").Include(
            "~/Scripts/DataTables/jquery.dataTables.min.js",
             "~/Scripts/DataTables/dataTables.bootstrap.js",
            "~/Scripts/DataTables/dataTables.colReorder.min.js",
            "~/Scripts/DataTables/buttons.colVis.min.js" )
        );

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


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/dataTables/css/dataTables.bootstrap.css",
                      "~/Content/css/jquery.gritter.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/site.css"));


        }
    }
}
