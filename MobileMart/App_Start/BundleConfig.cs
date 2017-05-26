using System.Web;
using System.Web.Optimization;

namespace MobileMart
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/AdminJSs").Include(
                       "~/Scripts/jquery-3.1.1.js",
                       "~/Scripts/jquery-ui-1.12.1.js",
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/plugins/morris/raphael.min.js",
                       "~/Scripts/plugins/morris/morris.min.js", 
                       "~/Scripts/plugins/morris/morris-data.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/FrontEndJSs").Include(
                        "~/Scripts/jquery-3.1.1.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/jquery.nivo.slider.js",
                        "~/Scripts/plugins.js",
                        "~/Scripts/main.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/alertify.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.8.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/jquery.nivo.slider.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/nivo-slider.css",
                      "~/Content/shortcode/shortcodes.css",
                      "~/Content/color-core.css",
                      "~/Content/default.css",
                      "~/Content/custom.css",
                      "~/Content/core.css",
                      "~/Content/style.css",
                      "~/Content/responsive.css",
                      "~/Content/site.css"));

            //bundles.Add(new StyleBundle("").Include(
            //    "http://localhost:8446/Home/Index"
            //    ));
                
        }
    }
}
