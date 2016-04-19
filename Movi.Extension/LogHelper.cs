using System;
using System.Web;
using Movi.Common;
using log4net;
namespace Movi.Extension
{
    public enum LoggerType
    {
        /// <summary>
        /// 日志输出至文件
        /// </summary>
        FileLogger = 0,
        /// <summary>
        /// 日志输出至数据库
        /// </summary>
        DbLogger = 1
    }

    /// <summary>
    /// 日志记录帮助类
    /// </summary>
    public class LogHelper<T> where T : class
    {
        private static ILog defaultLogger = LogManager.GetLogger("FileLogger");

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loggerType"></param>
        public static void Log(string message, LoggerType loggerType = LoggerType.FileLogger)
        {
            Log(message, null, string.Empty, loggerType.ToString());
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="loggerType"></param>
        public static void Log(Exception exception, LoggerType loggerType = LoggerType.FileLogger)
        {
            Log(string.Empty, exception, string.Empty, loggerType.ToString());
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="remarks"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="loggerName"></param>
        public static void Log(string message, Exception exception, string remarks, string loggerName)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                if (exception != null)
                {
                    message = exception.Message;
                }
                else
                {
                    return;
                }
            }
            var url = HttpHelper.GetCurrentUrl();

            #region 附加标准的remarks信息

            string appendedRemarks;
            var httpException = exception as HttpException;
            var urlReferrer = HttpHelper.GetUrlReferrer();
            var userAgent = HttpHelper.GetUserAgent();
            var browser = HttpHelper.GetBrowser();
            var clientIp = HttpHelper.GetClientIPAddress();
            if (httpException != null)
            {
                appendedRemarks = string.Format("HttpException Code:{0}.\r\n url:{1} - urlreferrer:{2} - useragent:{3} - browser:{4}", httpException.GetHttpCode(), url, urlReferrer, userAgent, browser);
            }
            else
            {
                appendedRemarks = string.Format("url:{0} - urlreferrer:{1} - useragent:{2} - browser:{3}", url, urlReferrer, userAgent, browser);
            }

            remarks = string.IsNullOrEmpty(remarks) ? appendedRemarks : remarks + "\r\n " + appendedRemarks;

            #endregion

            var logger = !string.IsNullOrEmpty(loggerName) ? LogManager.GetLogger(loggerName) : defaultLogger;
            var logMessage = String.Format("{0} - {1} - {2}", message, clientIp, remarks);
            if (exception == null)
            {
                logger.Info(logMessage);
            }
            else
            {
                logger.Error(logMessage, exception);
            }
        }
    }
}
