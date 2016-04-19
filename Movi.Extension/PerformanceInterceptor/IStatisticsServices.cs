using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movi.Extension
{
    public interface IStatisticsServices
    {
        /// <summary>
        /// 记录接口性能计数
        /// </summary>
        /// <param name="typeName">类名</param>
        /// <param name="elapsedMilliseconds">调用的毫秒数</param>
        void RecordProcessTime(string typeName, uint elapsedMilliseconds);

        /// <summary>
        /// 获取接口性能计数平均值
        /// </summary>
        /// <param name="typeName">类名</param>
        long? GetAverageProcessTime(string typeName);

        /// <summary>
        /// 清除接口性能计数
        /// </summary>
        /// <param name="typeName">类名</param>
        void ClearProcessTimeRecord(string typeName);
    }
}
