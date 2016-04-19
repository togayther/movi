using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Movi.Common;
using Movi.Model;

namespace Movi.ViewModel
{
    /// <summary>
    /// 管理控制面板视图模型
    /// </summary>
    public class DashboardViewModel
    {
        public SiteInfoModel SiteInfo { get; set; }
        public GrabInfoModel GrabInfo { get; set; }
        public LoginInfoModel LoginInfo { get; set; }

        public DashboardViewModel()
        {
            SiteInfo = new SiteInfoModel();
            GrabInfo = new GrabInfoModel();
            LoginInfo = new LoginInfoModel();
        }
    }

    /// <summary>
    /// 站点配置信息
    /// </summary>
    public class SiteInfoModel
    {
        public SiteInfoModel()
        {
        }

        public SiteInfoModel(SiteConfig config)
        {
            SiteName = config.SiteName;
            SiteUrl = config.SiteUrl;
        }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }


        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName
        {
            get { return Environment.MachineName; }
        }

        /// <summary>
        /// 操作系统名称
        /// </summary>
        public string OsName
        {
            get { return Environment.OSVersion.ToString(); }
        }

        /// <summary>
        /// 站点物理路径
        /// </summary>
        public string PhysicalPath
        {
            get { return HttpHelper.GetPhysicalPath(); }
        }

        /// <summary>
        /// 服务器IP 
        /// </summary>
        public string ServerIp
        {
            get
            {
                var ipAddress = HttpHelper.GetServerIPAddress();
                return ipAddress.Equals("::1") ? "127.0.0.1" : ipAddress;
            }
        }

        /// <summary>
        /// IIS环境
        /// </summary>
        public string IISVersion
        {
            get { return HttpHelper.GetServerIISVertion(); }
        }

        /// <summary>
        /// 站点域名
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// NET框架版本
        /// </summary>
        public string NetVersion
        {
            get { return Environment.Version.ToString(); }
        }

        /// <summary>
        /// 站点端口
        /// </summary>
        public string ServerPort
        {
            get { return HttpHelper.GetServerPort(); }
        }

    }

    /// <summary>
    /// 数据采集信息
    /// </summary>
    public class GrabInfoModel
    {
        /// <summary>
        /// 影片采集数量
        /// </summary>
        public int MovieGrabCount { get; set; }

        /// <summary>
        /// 电视剧采集数量
        /// </summary>
        public int TeleplayGrabCount { get; set; }

        /// <summary>
        /// 动漫采集数量
        /// </summary>
        public int CartoonGrabCount { get; set; }
    }

    /// <summary>
    /// 登录用户信息
    /// </summary>
    public class LoginInfoModel
    {
        /// <summary>
        /// 本次登录IP 
        /// </summary>
        public string ThisLoginIp { get; set; }

        /// <summary>
        /// 本次登录时间
        /// </summary>
        public string ThisLoginTime { get; set; }

        /// <summary>
        /// 上次登录IP 
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
    }
}
