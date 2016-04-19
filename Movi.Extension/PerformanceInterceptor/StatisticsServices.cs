using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movi.Extension
{
    /// <summary>
    /// 统计服务接口定义
    /// </summary>
    public class StatisticsServices : IStatisticsServices
    {
        #region Components

        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName
        {
            get;
            set;
        }

        /// <summary>
        /// 临界值
        /// </summary>
        private const uint Boundary = int.MaxValue;

        #endregion

        #region IStatisticsServices<T> Members

        /// <summary>
        /// 记录接口性能计数
        /// </summary>
        /// <param name="typeName">类名</param>
        /// <param name="elapsedMilliseconds">调用的毫秒数</param>
        public void RecordProcessTime(string typeName, uint elapsedMilliseconds)
        {
            return;
        }

        /// <summary>
        /// 获取接口性能计数平均值
        /// </summary>
        /// <param name="typeName">类名</param>
        public long? GetAverageProcessTime(string typeName)
        {
            return null;
        }

        /// <summary>
        /// 清除接口性能计数
        /// </summary>
        /// <param name="typeName">类名</param>
        public void ClearProcessTimeRecord(string typeName)
        {
            return;
        }

        #endregion
    }
}
