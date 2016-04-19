using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Movi.Common
{
    public class HttpHelper
    {
        private static readonly List<string> RobotUserAgentList =
            String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["RobotUserAgentList"])
                ? new List<string>()
                : ConfigurationManager.AppSettings["RobotUserAgentList"].Split(',').ToList();

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIPAddress()
        {
            return GetClientIPAddress(HttpContext.Current);
        }

        public static string GetClientIPAddress(HttpContext context)
        {
            var list = HttpContext.Current != null
                               ? new List<string>
                                 {
                                     context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                                     context.Request.ServerVariables["REMOTE_ADDR"],
                                     context.Request.UserHostAddress,
                                     "127.0.0.1"
                                 }
                               : new List<string>
                                 {
                                     "127.0.0.1"
                                 };
            var clientIP = list.First(item => !String.IsNullOrEmpty(item) && item != "::1");

            IPAddress ip;
            if (!IPAddress.TryParse(clientIP, out ip))
            {
                clientIP = "127.0.0.1";
            }
            return clientIP;
        }

        

        public static string GetUserAgent()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.UserAgent;
        }

        public static string GetUserHostAddress()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.UserHostAddress;
        }

        public static string GetCurrentUrl()
        {
            return GetCurrentUrl(true);
        }

        public static string GetCurrentUrl(bool queryStringRequired)
        {
            string currentUrl = string.Empty;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                //url重写的状况
                string originalUrl = context.Request.ServerVariables["HTTP_X_ORIGINAL_URL"];
                currentUrl = String.IsNullOrWhiteSpace(originalUrl)
                                 ? HttpContext.Current.Request.Url.ToString()
                                 : new Uri(new Uri(string.Format("http://{0}/", context.Request.Url.Host)), originalUrl).ToString();
            }
            //如果不需要querystring，截掉
            if (!queryStringRequired && currentUrl.IndexOf('?') > -1)
            {
                currentUrl = currentUrl.Split('?')[0];
            }
            return currentUrl;
        }

        public static string GetUrlReferrer()
        {
            return (HttpContext.Current != null && HttpContext.Current.Request.UrlReferrer != null)
                       ? HttpContext.Current.Request.UrlReferrer.ToString()
                       : String.Empty;
        }

        public static string GetBrowser()
        {
            return HttpContext.Current == null
                       ? String.Empty
                       : string.Format("{0} {1}", HttpContext.Current.Request.Browser.Browser, HttpContext.Current.Request.Browser.Version);
        }

        /// <summary>
        /// 当前访问者是否为蜘蛛程序
        /// 注：可通过手动更改浏览器user-agent设置或者url后附加isrobot=1参数来伪造当前请求为蜘蛛程序
        /// </summary>
        /// <returns></returns>
        public static bool IsRobot()
        {
            var isRobot = false;
            if (HttpContext.Current != null)
            {
                var userAgent = HttpContext.Current.Request.UserAgent;
                isRobot = (!String.IsNullOrWhiteSpace(userAgent) && RobotUserAgentList.Any(item => userAgent.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                          (HttpContext.Current.Request.QueryString.AllKeys.Contains("isrobot") && HttpContext.Current.Request.QueryString["isrobot"] == "1");
            }

            return isRobot;
        }

        public static string GetServerIPAddress()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
        }

        public static string GetServerPort()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        }

        public static string GetServerIISVertion()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"];
        }

        public static string GetPhysicalPath()
        {
            return HttpContext.Current == null ? String.Empty : HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
        }
    }
}
