using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Movi.Common;
using Movi.IService;
using Movi.Model;
using Movi.Model.Enums;
using Movi.ViewModel;
using Webdiyer.WebControls.Mvc;

namespace Movi.Controllers
{
    [Authorize]
    public class StatisticsController : BaseController
    {
        protected static readonly IVisitorService VisitorService = ServiceLocator.Current.GetInstance<IVisitorService>();

        /// <summary>
        /// 仅支持查看最近一周（天数不连续）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VisitStat()
        {
            var visitors = VisitorService.GetAll();
            var visitorList = visitors.Where(c => 
                c.VisitTime >= DateTime.Now.Date.AddDays(-Config.VisitStatDays)
                && c.VisitTime < DateTime.Now.Date).ToList();

            var results = new List<string[]>();

            if (visitorList.Any())
            {
                var dateGroups = visitorList.GroupBy(c => c.VisitTime.Date.Date);
                
                foreach (var dateGroup in dateGroups)
                {
                    var dateCount = dateGroup.Count();
                    var dataItem = new string[2];
                    dataItem[0] = dateGroup.Key.ToString("MM月dd日") ;
                    dataItem[1] = dateCount.ToString();
                    results.Add(dataItem);
                }
            }

            var resultArray = results.ToArray();
            var chartData = Newtonsoft.Json.JsonConvert.SerializeObject(resultArray);

            var visitStatViewMdoel = new VisitStatViewModel();

            visitStatViewMdoel.TotalVisitCount = visitors.Count();
            visitStatViewMdoel.All.ChartName = "所有";
            visitStatViewMdoel.All.ChartData = chartData;
            visitStatViewMdoel.All.Count = visitorList.Count;

            //百度
            var baiduVisitData = visitorList.Where(c => c.Source.Contains("baidu")).ToList();
            visitStatViewMdoel.Baidu.ChartName = "百度";
            if (baiduVisitData.Any())
            {
                visitStatViewMdoel.Baidu.ChartData = GetSparklineData(baiduVisitData);
                visitStatViewMdoel.Baidu.Count = baiduVisitData.Count;
            }
            
            //直接访问
            var visitorsBookmark = visitorList.Where(c => String.IsNullOrEmpty(c.Source)).ToList();
            visitStatViewMdoel.Bookmark.ChartName = "直接访问";
            if (visitorsBookmark.Any())
            {
                visitStatViewMdoel.Bookmark.ChartData = GetSparklineData(visitorsBookmark);
                visitStatViewMdoel.Bookmark.Count = visitorsBookmark.Count;
            }

            //其它
            var visitorsOther = visitorList.Where(c => !String.IsNullOrEmpty(c.Source) && !c.Source.Contains("baidu")).ToList();
            visitStatViewMdoel.Other.ChartName = "其它";
            if (visitorsOther.Any())
            {
                visitStatViewMdoel.Other.ChartData = GetSparklineData(visitorsOther);
                visitStatViewMdoel.Other.Count = visitorsOther.Count;
            }

            return View("/Views/Admin/Statistics/VisitStat.cshtml",visitStatViewMdoel);
        }

        #region Helper

        private string GetSparklineData(List<Visitor> visitors)
        {
            if (visitors.Any())
            {
                var dateGroups = visitors.GroupBy(c => c.VisitTime.Date.Date);
                var datas = new List<int>();
                foreach (var dateGroup in dateGroups)
                {
                    datas.Add(dateGroup.Count());
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(datas.ToArray());
            }
            return "[]";
        }

        #endregion
    }
}
