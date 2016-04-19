using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Movi.Common;
using HtmlAgilityPack;
using Movi.IGrabService;
using Movi.ImaxGrabService.GrabHelper;
using Movi.Model;

namespace Movi.ImaxGrabService
{
    public abstract class GrabBase<T> where T: class, new ()
    {

        #region Fields Elements

        /// <summary>
        /// 列表页面
        /// </summary>
        protected List<GrabPageContent> IndexPages = new List<GrabPageContent>();
        protected Object LockIndexPages = new object();

        /// <summary>
        /// 详细页面链接
        /// </summary>
        protected List<GrabHref> DetailLinks = new List<GrabHref>();
        protected Object LockDetailLinks = new object();

        /// <summary>
        /// 详细页面
        /// </summary>
        protected List<GrabPageContent> DetailPages = new List<GrabPageContent>();
        protected Object LockDetailPages = new object();

        /// <summary>
        /// 对象信息
        /// </summary>
        protected List<T> EntityInfos = new List<T>();
        protected Object LockEntityInfos = new object();

        /// <summary>
        /// 是否停止采集
        /// </summary>
        protected bool IsStop = false;

        protected GrabConfiger _configer;
        /// <summary>
        /// 采集配置信息
        /// </summary> 
        protected GrabConfiger Configer
        {
            get { return _configer ?? (_configer = GetConfiger()); }
        }

        #endregion
        

        #region Internal Implemente
        /// <summary>
        /// 采集数量
        /// </summary>
        public int GrabCount { get; set; }

        /// <summary>
        /// 采集对象信息
        /// </summary>
        /// <returns></returns>
        public List<T> Grab()
        {
            //获取列表页面
            var getListPageThread = new Thread(GetListPages);
            getListPageThread.Start();

            //分析列表页面链接
            var getDetailHrefThread1 = new Thread(GetDetailHrefs);
            getDetailHrefThread1.Start();
            var getDetailHrefThread2 = new Thread(GetDetailHrefs);
            getDetailHrefThread2.Start();

            //获取详细页面
            var getDetailThread1 = new Thread(GetDetailPages);
            getDetailThread1.Start();
            var getDetailThread2 = new Thread(GetDetailPages);
            getDetailThread2.Start();
            var getDetailThread3 = new Thread(GetDetailPages);
            getDetailThread3.Start();

            //获取影片信息
            var getDetailInfoThread1 = new Thread(GetDetailInfo);
            getDetailInfoThread1.Start();
            var getDetailInfoThread2 = new Thread(GetDetailInfo);
            getDetailInfoThread2.Start();


            var times = 0;
            var entityCountTemp = EntityInfos.Count;
            while (times <= Configer.StopAttemptTimes && !IsStop)
            {
                Thread.Sleep(1000);

                if (entityCountTemp == EntityInfos.Count)
                {
                    times++;
                    if (times > Configer.StopAttemptTimes)
                    {
                        IsStop = true;
                        break;
                    }
                }
                else
                {
                    entityCountTemp = EntityInfos.Count;
                    times = 0;
                }
            }

            return EntityInfos;
        }

        /// <summary>
        /// 提取列表页内容
        /// </summary>
        protected void GetListPages()
        {
            string content;
            var defaultListPage = String.Format(Configer.GrabListUrl, 1);
            try
            {
                content = WebHelper.CreateGetHttpRequest(defaultListPage);
            }
            catch (Exception)
            {
                return;
            }
            lock (LockIndexPages)
            {
                IndexPages.Add(new GrabPageContent { Url = defaultListPage, Content = content });
            }

            var document = new HtmlDocument();
            document.LoadHtml(content);
            var rootNode = document.DocumentNode;

            var lastPageNode = rootNode.SelectSingleNode(Configer.TotalPageXPath);
            var lastPageValue = lastPageNode.InnerText;

            if (!String.IsNullOrEmpty(lastPageValue))
            {
                var lastPageIndex = int.Parse(lastPageValue);

                if (lastPageIndex > 0)
                {

                    for (var i = 2; i <= lastPageIndex; i++)
                    {
                        var nextPageUrl = String.Format(Configer.GrabListUrl, i);
                        string nextPageContent;
                        try
                        {
                            nextPageContent = WebHelper.CreateGetHttpRequest(nextPageUrl);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        lock (LockIndexPages)
                        {
                            IndexPages.Add(new GrabPageContent { Url = nextPageUrl, Content = nextPageContent });
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 提取详细页链接内容
        /// </summary>
        protected void GetDetailHrefs()
        {
            while (!IsStop)
            {
                GrabPageContent page = null;
                lock (LockIndexPages)
                {
                    if (IndexPages.Count > 0)
                    {
                        page = IndexPages[0];
                        IndexPages.RemoveAt(0);
                    }
                }

                if (page != null)
                {
                    var document = new HtmlDocument();
                    document.LoadHtml(page.Content);

                    var rootNode = document.DocumentNode;
                    var movieNodes = rootNode.SelectNodes(Configer.EntityListXPath);
                    if (movieNodes != null && movieNodes.Any())
                    {
                        foreach (var movieNode in movieNodes)
                        {
                            var movieCover = movieNode.SelectSingleNode(Configer.EntityIdXPath);
                            //影片编号
                            var movieId = movieCover.Attributes["href"].Value;
                            movieId = movieId.Substring(movieId.LastIndexOf('/') + 1);

                            lock (LockDetailLinks)
                            {
                                var detailHref = new GrabHref { Url = String.Format(Configer.GrabDetailUrl,movieId) };
                                DetailLinks.Add(detailHref);
                            }
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 提取详细页内容
        /// </summary>
        protected void GetDetailPages()
        {
            while (!IsStop)
            {
                GrabHref href = null;
                lock (LockDetailLinks)
                {
                    if (DetailLinks.Count > 0)
                    {
                        href = DetailLinks[0];
                        DetailLinks.RemoveAt(0);
                    }
                }

                if (href != null)
                {
                    string detailContent;
                    try
                    {
                        detailContent = WebHelper.CreateGetHttpRequest(href.Url);
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    lock (LockDetailPages)
                    {
                        DetailPages.Add(new GrabPageContent { Url = href.Url, Content = detailContent });
                    }
                }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        protected void GetDetailInfo()
        {
            while (!IsStop)
            {
                GrabPageContent detailContent = null;
                lock (LockDetailPages)
                {
                    if (DetailPages.Count > 0)
                    {
                        detailContent = DetailPages[0];
                        DetailPages.RemoveAt(0);
                    }
                }
                if (detailContent != null)
                {
                    lock (LockEntityInfos)
                    {
                        var t = GetEntityDetailInfos(detailContent.Content);
                        if (t != null)
                        {
                            EntityInfos.Add(t);

                            if (GrabCount > 0 && EntityInfos.Count == GrabCount)
                            {
                                IsStop = true;
                                break;
                            }

                        }
                    }
                }
                Thread.Sleep(5);
            }
        }

        #endregion


        #region Abstract Implemente

        /// <summary>
        /// 采集对象详细信息
        /// </summary>
        /// <param name="detailContent"></param>
        protected abstract T GetEntityDetailInfos(string detailContent);

        /// <summary>
        /// 获取采集配置信息
        /// </summary>
        /// <returns></returns>
        protected abstract GrabConfiger GetConfiger(); 

        #endregion

    }
}
