using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Movi.Common;
using Movi.IService;
using Movi.Model;
using Movi.Model.Enums;
using Webdiyer.WebControls.Mvc;

namespace Movi.Controllers
{
    public class BaseController : Controller
    {
        protected static readonly IUserService UserService = ServiceLocator.Current.GetInstance<IUserService>();

        protected static readonly ISiteConfigService ConfigService = ServiceLocator.Current.GetInstance<ISiteConfigService>();

        protected static readonly IMediaService MediaService = ServiceLocator.Current.GetInstance<IMediaService>();

        /// <summary>
        /// 站点配置信息
        /// </summary>
        protected SiteConfig Config = null;

        /// <summary>
        /// 登录用户信息
        /// </summary>
        protected User LoginUser = null;

        /// <summary>
        /// 执行前读取站点配置信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Config = CacheHelper.GetCache(Constant.CacheKey.SiteConfigCacheKey) as SiteConfig;
            if (Config == null)
            {
                Config = ConfigService.LoadConfig(Constant.SiteConfigPath);
                CacheHelper.SetCache(Constant.CacheKey.SiteConfigCacheKey, Config);
            }
        }

        /// <summary>
        /// 返回结果前附加数据
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Config != null)
            {
                ViewData["SiteConfig"] = Config;
            }

            if (LoginUser != null)
            {
                ViewData["LoginUser"] = LoginUser;
            }
        }

        /// <summary>
        /// 获取已验证用户信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            //获取已登录用户信息
            var userName = User.Identity.Name;
            if (!String.IsNullOrEmpty(userName))
            {
                LoginUser = CacheHelper.GetCache(Constant.CacheKey.LoginUserInfoCacheKey + userName) as User;
                if (LoginUser == null)
                {
                    RedirectToAction("Login", "Admin");
                }
            }
        }

        #region Genernal Helper

        /// <summary>
        /// 查询条件构造
        /// </summary>
        /// <param name="medias"></param>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="area"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        protected static IQueryable<Media> SearchConditionBuilder(
            IQueryable<Media> medias,
            string category,
            string area,
            string year,
            MediaType type = MediaType.Movie)
        {
            medias = medias.Where(t => t.Type == type);

            category = ValidateHelper.SqlFilter(category);
            if (!String.IsNullOrEmpty(category) && !category.Equals("全部"))
            {
                medias = medias.Where(t => t.Categories.Contains(category));
            }
            area = ValidateHelper.SqlFilter(area);
            if (!String.IsNullOrEmpty(area) && !area.Equals("全部"))
            {
                medias = medias.Where(t => t.Area.Equals(area));
            }
            year = ValidateHelper.SqlFilter(year);
            if (!String.IsNullOrEmpty(year) && !year.Equals("全部"))
            {
                int yearVal;
                if (Int32.TryParse(year, out yearVal))
                {
                    medias = medias.Where(t => t.Year == yearVal);
                }
                else
                {
                    var yearPrefix = year.Substring(0, 2);
                    var yearStart = 0;
                    var yearEnd = 0;
                    if (yearPrefix.Equals("00"))
                    {
                        yearStart = 2000;
                        yearEnd = 2009;
                    }
                    else
                    {
                        yearStart = int.Parse("19" + yearPrefix);
                        yearEnd = int.Parse("19" + (int.Parse(yearPrefix) + 9));
                    }
                    medias = medias.Where(t => t.Year >= yearStart && t.Year <= yearEnd);
                }
            }
            return medias;
        }

        /// <summary>
        /// 排序条件构造
        /// </summary>
        /// <param name="medias"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        protected static IQueryable<Media> SortConditionBuilder(IQueryable<Media> medias, SortField sort)
        {
            if (sort == SortField.AddTime)
            {
                medias = medias.OrderByDescending(c => c.AddTime);
            }
            if (sort == SortField.Year)
            {
                medias = medias.OrderByDescending(c => c.Year);
            }
            if (sort == SortField.Rank)
            {
                medias = medias.OrderByDescending(c => c.Rank);
            }
            return medias;
        }

        /// <summary>
        /// 查询结果构造
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="medias"></param>
        /// <param name="searchKey"></param>
        /// <param name="p"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected PagedList<Media> SearchResultBuilder(
            IQueryable<Media> medias,
            string searchKey,
            int? p = 1,
            MediaType type = MediaType.Movie)
        {
            medias = medias.Where(c => c.Type == type);

            searchKey = ValidateHelper.SqlFilter(searchKey);
            if (!String.IsNullOrEmpty(searchKey))
            {
                medias = medias.Where(c => c.Name.Contains(searchKey)
                                  || c.ActorsSerialized.Contains(searchKey)
                                  || c.Director.Contains(searchKey)
                                  || c.CategoriesSerialized.Contains(searchKey)
                                  || c.AnotherName.Contains(searchKey));
            }
            var mediaPageList = medias.OrderBy(c => c.Rank).ToPagedList(p ?? 1, Config.ShowPageSize);
            return mediaPageList;
        }

        /// <summary>
        /// 相似集合构造
        /// </summary>
        /// <param name="medias"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        protected List<Media> SimilarmediasBuilder(
            IQueryable<Media> medias,
            Media media)
        {
            if (medias == null || !medias.Any() || media == null)
            {
                return new List<Media>();
            }

            const float similarRank = 7.0f;

            var categories = media.Categories;

            if (categories != null && categories.Any())
            {
                var firstCategory = categories.First();

                medias = medias.Where(p => p.Type == media.Type
                    && p.Id != media.Id
                    && p.CategoriesSerialized.Contains(firstCategory)
                    && p.Area == media.Area
                    && p.Rank > similarRank);

                var similars = medias.Take(10).ToList();

                return similars;
            }
            return new List<Media>();
        }

        /// <summary>
        /// 获取邮件发送服务
        /// </summary>
        /// <returns></returns>
        protected MailHelper GetMailSender(string subject,string body,string to,bool isHtmlBody = true)
        {
            var mailHelper = new MailHelper();

            mailHelper.From = Config.EmailFrom;
            mailHelper.Subject = subject;
            mailHelper.Html = isHtmlBody;
            mailHelper.Body = body;
            mailHelper.MailDomain = Config.EmailSmtp;
            mailHelper.MailDomainPort = Config.EmailPort;
            mailHelper.MailServerUserName = Config.EmailUserName;
            mailHelper.MailServerPassWord = Config.EmailPassword;
            mailHelper.RecipientName = to;

            return mailHelper;
        }

        #endregion
    }
}
