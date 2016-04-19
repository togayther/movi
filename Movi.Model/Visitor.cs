using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movi.Model
{
    /// <summary>
    /// 访问者信息
    /// </summary>
    public class Visitor
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 访问者IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 访问者浏览器
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// 访问者来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime VisitTime { get; set; }

        /// <summary>
        /// 浏览器 UserAgent
        /// </summary>
        public string UserAgent { get; set; }
    }
}
