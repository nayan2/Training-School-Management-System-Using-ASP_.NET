using System;
using System.Web.Optimization;

namespace TSMS_Atp_2_Final_Project.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBuldles(BundleCollection Icollection)
        {
            Icollection.Add(new ScriptBundle("~/bundles/scripts")
                .Include(
                         "~/Scripts/Custom_Login.js",
                         "~/Scripts/MyCustomFile.js",
                         "~/Scripts/jquery-ui-1.12.1.js"));

            Icollection.Add(new ScriptBundle("~/Bundles/js")
                .Include("~/Content/js/plugins/jquery/jquery-2.2.4.js")
                .Include("~/Content/js/plugins/bootstrap/bootstrap.js")
                .Include("~/Content/js/plugins/fastclick/fastclick.js")
                .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.js")
                .Include("~/Content/js/plugins/select2/select2.full.js")
                .Include("~/Content/js/plugins/moment/moment.js")
                .Include("~/Content/js/plugins/moment/combodate.js")
                .Include("~/Content/js/plugins/icheck/icheck.js")
                .Include("~/Content/js/plugins/validator.js")
                .Include("~/Content/js/plugins/inputmask/jquery.inputmask.bundle.js")
                .Include("~/Content/js/app.js")
                .Include("~/Content/js/init.js")
                .Include("~/Content/datetimepicker/js/bootstrap-datetimepicker.js")
                .Include("~/Content/datetimepicker/js/bootstrap-datetimepicker.fr.js"));

            Icollection.Add(new StyleBundle("~/bundles/styles")
                .Include(
                        "~/Content/bootstrap-theme.css",
                        "~/Content/Custom_Login.css"));

            Icollection.Add(new StyleBundle("~/Bundles/css")
                .Include("~/Content/css/select2.css")
                .Include("~/Content/css/datepicker3.css")
                .Include("~/Content/css/AdminLTE.css")
                .Include("~/Content/bootstrap-dialog.css")
                .Include("~/Content/css/font-awesome.min.css")
                .Include("~/Content/css/icheck/blue.min.css")
                .Include("~/Content/Custom_Admin.css")
                .Include("~/Content/css/skins/skin-blue.css")
                .Include("~/Content/datetimepicker/css/bootstrap-datetimepicker.min.css"));

            #if DEBUG
                        BundleTable.EnableOptimizations = false;
            #else
                        BundleTable.EnableOptimizations = true;
            #endif
        }
    }
}