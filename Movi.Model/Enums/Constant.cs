using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Movi.Model.Enums
{
    public  class Constant
    {

        /// <summary>
        /// 站点配置文件路径
        /// </summary>
        public static string SiteConfigPath = HttpContext.Current.Server.MapPath("/Configuration/SiteConfig.config");

        /// <summary>
        /// 站点缓存键集合
        /// </summary>

        public static class CacheKey
        {
            public static string SiteConfigCacheKey = "CACHE_SITE_CONFIG";

            public static string LockUserInfoCacheKey = "CACHE_LOCK_USER";

            public static string LoginUserInfoCacheKey = "CACHE_LOGIN_USER";

        }
    }
}
