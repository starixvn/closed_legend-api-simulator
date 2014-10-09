namespace TaiwanApiProxy
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                                        "~/Scripts/jquery/jquery.js", 
                                        "~/Scripts/placeholder/jquery.placeholder.js"));

            bundles.Add(new ScriptBundle("~/js/jqueryui").Include(
                                        "~/Scripts/jqueryui/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/js/bootstrap").Include(
                                        "~/Scripts/bootstrap/bootstrap.js"));

            bundles.Add(new LessBundle("~/css/bootstrap").Include("~/content/bootstrap/bootstrap.less"));

            bundles.Add(new LessBundle("~/css/site").Include("~/content/site.less"));

            bundles.Add(new ScriptBundle("~/js/home").Include(
                       "~/Scripts/home.js"));

            bundles.Add(new ScriptBundle("~/js/rule").Include(
                        "~/Scripts/utility.js",
                        "~/Scripts/rule.js"));
        }
    }
}