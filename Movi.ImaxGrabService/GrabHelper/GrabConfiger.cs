namespace Movi.ImaxGrabService.GrabHelper
{
    public class GrabConfiger
    {

        /// <summary>
        /// 列表采集地址
        /// </summary>
        public string GrabListUrl { get; set; }

        /// <summary>
        /// 详细信息采集地址
        /// </summary>
        public string GrabDetailUrl { get; set; }

        /// <summary>
        /// 采集数量
        /// </summary>
        public int GrabCount { get; set; }

        /// <summary>
        /// 终止尝试次数
        /// </summary>
        public int StopAttemptTimes { get; set; }

        /// <summary>
        /// 总页数XPath
        /// </summary>
        public string TotalPageXPath { get; set; }

        /// <summary>
        /// 采集对象列表XPath
        /// </summary>
        public string EntityListXPath { get; set; }

        /// <summary>
        /// 采集对象编号XPath
        /// </summary>
        public string EntityIdXPath { get; set; }

        /// <summary>
        /// 采集对象详细XPath
        /// </summary>
        public string EntityDetailXPath { get; set; } 
    }
}
