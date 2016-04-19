using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movi.Model
{
    public class MediaComment
    {
        /// <summary>
        /// 评论编号
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
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 用户IP
        /// </summary>
        public string UserIp { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string UserEmail { get; set; }
    }
}
