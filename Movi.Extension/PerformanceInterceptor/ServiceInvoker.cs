using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Movi.Extension
{
    /// <summary>
    /// 服务调用者接口定义
    /// </summary>
    public abstract class ServiceInvoker
    {
        /// <summary>
        /// 获取服务Url
        /// </summary>
        /// <returns></returns>
        public abstract string getServiceUrl();

        /// <summary>
        /// 获取Logger名称
        /// </summary>
        /// <returns></returns>
        public abstract string getLoggerName();

        /// <summary>
        /// 获取接口名称
        /// </summary>
        /// <returns></returns>
        public abstract string getInterfaceName();

        /// <summary>
        /// 获取服务方法名
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public abstract string getServiceMethodName(MethodBase method);

        /// <summary>
        /// 获取超时时间（毫秒）
        /// </summary>
        /// <returns></returns>
        public abstract int getTimeOutMilliseconds();

        /// <summary>
        /// 是否记录访问时间
        /// </summary>
        /// <returns></returns>
        public virtual bool recordAccessTime()
        {
            return true;
        }

        /// <summary>
        /// 是否写超时日志
        /// </summary>
        /// <returns></returns>
        public virtual bool logTimeout()
        {
            return true;
        }

        /// <summary>
        /// 记录服务日志信息
        /// </summary>
        /// <typeparam name="T">服务类</typeparam>
        /// <param name="ex">错误</param>
        /// <param name="method">报错方法</param>
        /// <param name="remark"> </param>
        public virtual void Log<T>(Exception ex, MethodBase method, string remark = null) where T : class
        {
            if (ex != null)
            {
                LogHelper<T>.Log(
                    string.Format("Interface:{0},method:{1}; \r\n message:{2}", getInterfaceName(), getServiceMethodName(method), ex.Message),
                    ex,
                    remark ?? string.Empty,
                    getLoggerName());
            }
        }
    }
}
