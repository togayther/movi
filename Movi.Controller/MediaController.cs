using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;
using Movi.Common;
using Movi.IService;
using Movi.Model;
using Movi.Model.Enums;
using Webdiyer.WebControls.Mvc;

namespace Movi.Controllers
{
    public class MediaController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="mtype"></param>
        /// <param name="p"></param>
        /// <param name="category"></param>
        /// <param name="area"></param>
        /// <param name="year"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(
            MediaType mtype = MediaType.Movie,
            int? p = 1,
            string category = "全部",
            string area = "全部",
            string year = "全部",
            SortField sort = SortField.AddTime)
        {
            var medias = MediaService.GetAll();
            ViewData["MediaCount"] = medias.Count(c => c.Type == mtype);

            //查询参数构造
            medias = SearchConditionBuilder(medias, category, area, year, mtype);

            //排序参数构造
            medias = SortConditionBuilder(medias, sort);

            var mediaPage = medias.ToPagedList(p ?? 1, Config.ShowPageSize);

            ViewData["MediaType"] = mtype;
            ViewData["Medias"] = mediaPage;

            return View();
        }

        /// <summary>
        /// 关键字查询
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="p"></param>
        /// <param name="mtype"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search(
            string searchKey,
            int? p = 1,
            MediaType mtype = MediaType.Movie)
        {
            var medias = MediaService.GetAll();
            ViewData["MediaCount"] = medias.Count(c => c.Type == mtype);
            ViewData["MediaType"] = mtype;
            ViewData["Medias"] = SearchResultBuilder(medias, searchKey, p, mtype);
            return View("Index");
        }

        /// <summary>
        /// 详细页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(string id)
        {
            var media = MediaService.Find(p => p.Id.Equals(id));
            var medias = MediaService.GetAll();
            ViewData["Media"] = media;
            ViewData["MediaType"] = media.Type;
            ViewData["SimilarMedias"] = SimilarmediasBuilder(medias, media);
            return View();
        }

        #region Admin Manage
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Edit(string id)
        {
            var media = MediaService.Find(p => p.Id.Equals(id));
            ViewData["Media"] = media;
            return View("/Views/Admin/Content/Edit.cshtml", media);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Media media)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    InitDefaultValue(media);
                    MediaService.Update(media);
                    TempData["Success"] = "恭喜你，更新成功！";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Global", "内部服务器错误，更新失败！Error:" + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("Global", "数据验证错误，更新失败！");
            }
            return View("/Views/Admin/Content/Edit.cshtml", media);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Add(MediaType mtype = MediaType.Movie)
        {
            ViewData["MediaType"] = mtype;
            return View("/Views/Admin/Content/Add.cshtml");
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Add(Media media)
        {
            if (ModelState.IsValid)
            {
                InitDefaultValue(media);
                try
                {
                    MediaService.Insert(media);

                    TempData["Success"] = "恭喜你，添加成功！";
                    ModelState.Clear();

                    return RedirectToAction("ManageIndex", "Media", new RouteValueDictionary() { { "mtype", media.Type } });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Global", "内部服务器错误，添加失败！Error:" + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("Global", "数据验证错误，添加失败！");
            }

            return View("/Views/Admin/Content/Add.cshtml", media);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="mtype"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Delete(List<string> ids, MediaType mtype = MediaType.Movie)
        {
            if (ids != null && ids.Any())
            {
                MediaService.Delete(p => ids.Contains(p.Id));
            }

            return RedirectToAction("ManageIndex", "Media", new RouteValueDictionary() { { "mtype", mtype } });
        }

        /// <summary>
        /// 影视管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult ManageIndex(MediaType mtype = MediaType.Movie, int? p = 1)
        {
            var medias = MediaService.GetAll();
            var mediaPage = medias.Where(c => c.Type == mtype).OrderByDescending(c => c.AddTime).ToPagedList(p ?? 1, 12);
            ViewData["Medias"] = mediaPage;
            ViewData["MediaType"] = mtype;
            return View("/Views/Admin/Content/List.cshtml");
        }

        /// <summary>
        /// 影片关键字查询
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="p"></param>
        /// <param name="mtype"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult ManageSearch(string searchKey, int? p = 1, MediaType mtype = MediaType.Movie)
        {
            var medias = MediaService.GetAll();
            ViewData["Medias"] = SearchResultBuilder(medias, searchKey, p);
            ViewData["MediaType"] = mtype;

            return View("/Views/Admin/Content/List.cshtml");
        } 
        #endregion

        #region Helper

        /// <summary>
        /// 验证接收到的Media数据
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        private static void InitDefaultValue(Media media)
        {
            if (String.IsNullOrEmpty(media.Id))
            {
                media.Id = StringHelper.GetGuidString();
            }
            if (String.IsNullOrEmpty(media.AnotherName))
            {
                media.AnotherName = media.Name;
            }
            if (String.IsNullOrEmpty(media.Language))
            {
                media.Language = "未知";
            }

            if (media.AddTime == default(DateTime))
            {
                media.AddTime = DateTime.Now;
            }

            if (media.Resources != null && media.Resources.Any())
            {
                var newResources = media.Resources.Where(p => String.IsNullOrEmpty(p.Id)).ToList();
                if (newResources.Any())
                {
                    newResources.ForEach(c => c.Id = StringHelper.GetGuidString());
                }
            }
        }

        #endregion
    }
}
