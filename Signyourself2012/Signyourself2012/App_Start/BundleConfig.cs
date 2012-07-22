using System.Web;
using System.Web.Optimization;

namespace Signyourself2012
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/sus").Include(
                "~/Scripts/sus/css3-mediaqueries*",
                "~/Scripts/sus/custom*",
                "~/Scripts/sus/form-Validation*",
                "~/Scripts/sus/hideshow*",
                "~/Scripts/sus/hoverIntent*",
                "~/Scripts/sus/imagesloaded*",
                "~/Scripts/sus/moveform*",
                "~/Scripts/sus/superfish*",
                "~/Scripts/sus/supersubs*",
                "~/Scripts/sus/jquery*"));

            bundles.Add(new StyleBundle("~/Content/sus").Include(
                "~/Content/sus/style.css",
                "~/Content/sus/jquery.tweet.css",
                "~/Content/sus/superfish*",
                "~/Content/sus/prettyPhoto.css",
                "~/Content/sus/tip-Twitter.css",
                "~/Content/sus/tip-yellowsimple.css",
                "~/Content/sus/comments.css",
                "~/Content/sus/flexslider.css",
                "~/Content/sus/lessframework.css",
                "~/Content/sus/skin.css"));

            bundles.Add(new StyleBundle("~/Content/sus/dashboard").Include(
                "~/Content/sus/dashboard/ie.css",
                "~/Content/sus/dashboard/layout.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }

    }
}