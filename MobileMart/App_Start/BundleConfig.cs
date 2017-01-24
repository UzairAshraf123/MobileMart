using System.Web;
using System.Web.Optimization;

namespace MobileMart
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/jquery.nivo.slider.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/JSs").Include(
                        "~/Scripts/plugins.js",
                        "~/Scripts/main.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.8.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/core.css",
                      "~/Content/shortcodes.css",
                      "~/Content/color-core.css",
                      "~/Content/custom.css",
                      "~/Content/nivo-slider.css",
                      "~/Content/responsive.css",
                      "~/Content/default.css",
                      "~/Content/style.css"));
        }
    }
}
