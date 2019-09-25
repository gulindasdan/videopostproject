using System.Web;
using System.Web.Optimization;

namespace VideoPostProject.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/jquery-3.2.1.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/general").Include(
                      "~/Content/js/jquery-3.2.1.min.js",
                      "~/Content/js/jquery.sticky-kit.min.js",
                      "~/Content/js/custom.js",
                      "~/Content/js/bootstrap.min.js",
                      "~/Content/js/imagesloaded.pkgd.min.js",
                      "~/Content/js/grid-blog.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/areageneral").Include(
                     "~/Content/js/jquery-3.2.1.min.js",
                     "~/Content/js/chartist.min.js",
                     "~/Content/js/bootstrap-notify.js",
                     "~/Content/js/light-bootstrap-dashboard.js",
                     "~/Content/js/demo.js"));

            bundles.Add(new ScriptBundle("~/bundles/area").Include(
                        "~/Content/js/bootstrap.js",
                        "~/Content/js/scripts.js",
                        "~/Content/js/jquery.nicescroll.js"));

            bundles.Add(new StyleBundle("~/styles/general").Include(
                     "~/Content/css/bootstrap.min.css",
                     "~/Content/css/style.css",
                     "~/Content/css/responsive.css"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.



            bundles.Add(new StyleBundle("~/styles/area").Include(
               "~/Content/css3/bootstrap.min.css",
               "~/Content/css3/animate.min.css",
               "~/Content/css3/light-bootstrap-dashboard.css",
               "~/Content/css3/demo.css",
               "~/Content/css3/pe-icon-7-stroke.css",
               "~/Content/css2/font-awesome.min.css"
               ));
            bundles.Add(new StyleBundle("~/styles/login").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/style.css",
                "~/Content/css/responsive.css"
                ));
            bundles.Add(new StyleBundle("~/styles/profil").Include(
                "~/Content/css2/app.css",
                "~/Content/css2/font-awesome.min.css",
                "~/Content/css2/hover-min.css",
                "~/Content/css2/jquery.kyco.easyshare.css",
                "~/Content/css2/owl.theme.default.min.css",
                "~/Content/css2/owl.carousel.min.css",
                "~/Content/css2/responsive.css",
                "~/Content/css2/theme.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/betube").Include(
               "~/Content/js/jquery.js",
               "~/Content/js/what-input.js",
               "~/Content/js/foundation.js",
               "~/Content/js/jquery.showmore.src.js",
               "~/Content/js/app.js",
               "~/Content/js/greensock.js",
               "~/Content/js/layerslider.transitions.js",
               "~/Content/js/layerslider.kreaturamedia.jquery.js",
               "~/Content/js/owl.carousel.min.js",
               "~/Content/js/inewsticker.js",
               "~/Content/js/jquery.kyco.easyshare.js"
               ));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //           "~/Scripts/bootstrap.min.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/bootstrap.js"));
        }
    }
}
