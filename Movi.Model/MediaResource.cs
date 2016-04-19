using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movi.Model
{
    public class MediaResource
    {
        /// <summary>
        /// 资源编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 关联编号
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 关联媒体
        /// </summary>
        public Media Media { get; set; }

        /// <summary>
        /// 资源大小 
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 种子品质（1080p/720p）
        /// </summary>
        public string Quality { get; set; }
    }
}
