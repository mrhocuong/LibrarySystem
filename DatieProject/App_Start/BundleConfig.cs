using System.Web.Optimization;

namespace DatieProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/Jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/Jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //    "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/blockUI").Include(
                "~/Scripts/jquery.blockUI.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                 "~/Scripts/notify.js"));
            bundles.Add(new ScriptBundle("~/bundles/numberic").Include(
                "~/Scripts/jquery.numeric.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/Bootstrap/bootstrap.js"));
                //"~/Scripts/Bootstrap/respond.js"

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/DataTables/css/jquery.dataTables.css"));
            // "~/Content/JqueryUpload/jquery.fileupload*"));
            //User Datatable plugin
            bundles.Add(new ScriptBundle("~/bundles/Datatables").Include(
                "~/Scripts/Datatables/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Gallery").Include(
                "~/Content/Gallery/blueimp-gallery.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/GalleryBody").Include(
                "~/Scripts/Gallery/blueimp-gallery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Material").Include(
                "~/Scripts/Material/material*",
                "~/Scripts/Material/ripples*"));
            bundles.Add(new ScriptBundle("~/bundles/MaterialCss").Include(
                "~/Content/Material/material*",
                "~/Content/Material/ripples*",
                "~/fonts/Material/Material*",
                "~/fonts/Material/Material*"));
        }
    }
}