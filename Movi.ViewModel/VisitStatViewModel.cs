using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movi.Model;

namespace Movi.ViewModel
{
    /// <summary>
    /// 访问统计
    /// </summary>
    public class VisitStatViewModel
    {

        public VisitStatViewModel()
        {
            All = new ChartDataModel();
            Baidu = new ChartDataModel();
            Bookmark = new ChartDataModel();
            Other = new ChartDataModel();
        }


        /// <summary>
        /// 总访问数
        /// </summary>
        public int TotalVisitCount { get; set; }

        /// <summary>
        /// 指定天数内访问统计
        /// </summary>
        public ChartDataModel All { get; set; }

        /// <summary>
        /// 指定天数内百度访问统计
        /// </summary>

        public ChartDataModel Baidu { get; set; }

        /// <summary>
        /// 指定天数内搜狗访问统计
        /// </summary>
        public ChartDataModel Other { get; set; }

        /// <summary>
        /// 指定天数内直接访问统计（书签/地址栏网址）
        /// </summary>
        public ChartDataModel Bookmark { get; set; }
    }

    public class ChartDataModel
    {
        public string ChartName { get; set; }
        public int Count { get; set; }
        public string ChartData { get; set; }
    }
}
