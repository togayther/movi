using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Microsoft.Practices.ServiceLocation;
using Movi.Controllers;
using Movi.Extension;
using Movi.Extension.MvcXmlRouting;

namespace Movi.RazorWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeRoutes(RouteTable.Routes);

            InitializeLogger();

            InitializeServiceLocator();

        }

        protected void Session_Start()
        {
            RecordVisitorInfo();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex != null)
            {
                LogHelper<MvcApplication>.Log(ex);
               // Server.ClearError();
            }
        }

        public static void InitializeRoutes(RouteCollection routes)
        {
            var section = (MvcRouteConfigurationSection)ConfigurationManager.GetSection("RouteConfiguration");
            routes.MapRoute(section);
            routes.RouteExistingFiles = true;
        }

        protected virtual void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer(new XmlInterpreter());
            
            ComponentRegister.AddComponentsTo(container);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        protected virtual void InitializeLogger()
        {
            const string logConfigPath = "~/Configuration/Log4net.config";
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath(logConfigPath)));
        }

        protected void RecordVisitorInfo()
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Visitor";
            routeData.Values["action"] = "Index";
            Response.StatusCode = 500;
            IController controller = new VisitorController();

            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }
    }
}