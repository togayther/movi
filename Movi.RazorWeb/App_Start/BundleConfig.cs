using System.Web;
using System.Web.Optimization;

namespace Movi.RazorWeb
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region FrontPage Scripts
            //框架脚本
            bundles.Add(new ScriptBundle("~/Scripts/framework").Include(
                        "~/Content/Scripts/jquery.1.9.1.js",
                        "~/Content/Plugins/Bootstrap/js/bootstrap.min.js",
                        "~/Content/Scripts/respond.min.js"
                        ));

            //特性检测
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
                        "~/Content/Scripts//modernizr.custom.js"
                        ));

            //工具脚本及插件
            bundles.Add(new ScriptBundle("~/Scripts/common").Include(
                        "~/Content/Scripts/utils.js",
                        "~/Content/Plugins/Raty/jquery.raty.min.js",
                        "~/Content/Scripts/init.js"
                        ));

            //详细页
            bundles.Add(new ScriptBundle("~/Scripts/detail").Include(
                        "~/Content/Scripts/detail.js"
                        )); 

            #endregion

            #region Frontpage Styles
            //框架样式
            bundles.Add(new StyleBundle("~/Styles/framework").Include(
                        "~/Content/Plugins/Bootstrap/css/bootstrap-unite.css",
                        "~/Content/Styles/common.css"
                        ));
            //列表页
            bundles.Add(new StyleBundle("~/Styles/home").Include(
                        "~/Content/Styles/home.css"
                        ));
            //详细页
            bundles.Add(new StyleBundle("~/Styles/detail").Include(
                        "~/Content/Styles/detail.css"
                        ));
            
            #endregion


            #region BackDesk Scripts

            bundles.Add(new ScriptBundle("~/Scripts/admin").Include(
                        "~/Content/Admins/js/jquery/jquery.uniform.js",
                        "~/Content/Admins/js/jquery/jquery.validate.js",
                        "~/Content/Admins/js/jquery/jquery.validate.unobtrusive.js",
                        "~/Content/Admins/js/messages_cn.js",
                        "~/Content/Admins/js/doT.js",
                        "~/Content/Admins/js/bootstrap.switch.js",
                        "~/Content/Admins/js/jquery/jquery.flot.js",
                        "~/Content/Admins/js/jquery/jquery.flot.categories.js",
                        "~/Content/Admins/js/jquery/jquery.sparkline.min.js",
                        "~/Content/Admins/js/main.js"
                        ));
            #endregion

            #region BackDesk Styles
            //后台样式
            bundles.Add(new StyleBundle("~/Styles/admin").Include(
                        "~/Content/Admins/css/bootstrap.min.css",
                        "~/Content/Admins/css/bootstrap.switch.css",
                        "~/Content/Admins/css/glyphicons.min.css",
                        "~/Content/Admins/css/animate.css",
                        "~/Content/Admins/css/theme.css",
                        "~/Content/Admins/css/responsive.css",

                        "~/Content/Admins/css/uniform.default.css",

                        "~/Content/Admins/css/main.css"
                        )); 
            #endregion

        }
    }
}