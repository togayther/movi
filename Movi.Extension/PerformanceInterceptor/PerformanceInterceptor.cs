using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Movi.Common;

namespace Movi.Extension
{
    /// <summary>
    /// 性能记录拦截器，如超过预设超时时间，则记录日志
    /// </summary>
    public class PerformanceInterceptor : IInterceptor
    {
        public IStatisticsServices StatisticsServices { get; set; }

        public void Intercept(IInvocation invocation)
        {
            var watch = new Stopwatch();
            watch.Start();
            invocation.Proceed();
            watch.Stop();
            //ServiceInvoker是一个未实现的抽象类
            if (invocation.InvocationTarget is ServiceInvoker)
            {
                var target = ((ServiceInvoker)invocation.InvocationTarget);
                if (target.logTimeout())
                {
                    int timeout = target.getTimeOutMilliseconds();
                    if (watch.ElapsedMilliseconds > timeout)
                    {
                        LogHelper<PerformanceInterceptor>.Log(
                            string.Format("Interface:{0},method:{1}; message:The operation has timed out", target.getInterfaceName(), target.getServiceMethodName(invocation.Method)),
                            null,
                            string.Empty,
                            target.getLoggerName());
                    }
                }

                if (StatisticsServices != null && target.recordAccessTime())
                    StatisticsServices.RecordProcessTime(invocation.InvocationTarget.GetType().ToString(), Convert.ToUInt32(watch.ElapsedMilliseconds));
            }
            else
            {
                if (watch.ElapsedMilliseconds > ConfigHelper.GetConfigInt("DefaultTimeout"))
                {
                    LogHelper<PerformanceInterceptor>.Log(string.Format("class:{0} method:{1}; message:The operation has timed out", invocation.InvocationTarget, invocation.Method.Name));
                }
            }
        }
    }
}
