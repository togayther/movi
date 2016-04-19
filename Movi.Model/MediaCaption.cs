using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movi.Model
{
    public class MediaCaption
    {
        /// <summary>
        /// 字幕编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 媒体编号
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 关联媒体
        /// </summary>
        public Media Media { get; set; }

        /// <summary>
        /// 字幕地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 字幕名称
        /// </summary>
        public string Name { get; set; }
    }
}
