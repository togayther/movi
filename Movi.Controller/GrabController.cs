using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Movi.IGrabService;
using Movi.IService;
using Microsoft.Practices.ServiceLocation;
using Movi.Model;
using Movi.Model.Enums;

namespace Movi.Controllers
{
    public class GrabController : BaseController
    {
        private readonly static IMediaService MediaDataService = ServiceLocator.Current.GetInstance<IMediaService>();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grab(string grabType,string grabAddress,int grabCount)
        {
            string responseMsg;

            if (String.IsNullOrEmpty(grabType))
            {
                responseMsg = "请选择采集类型！";
            }
            else
            {
                try
                {

                    var watch = new Stopwatch();
                    watch.Start();

                    var successGrabCount = 0;

                    MediaType mediaType;
                    if (Enum.TryParse(grabType,out mediaType))
                    {
                        switch (mediaType)
                        {
                                case MediaType.Movie:
                                successGrabCount = MovieGrab(grabCount).Count;
                                break;
                                case MediaType.Cartoon:
                                successGrabCount = CartoonGrab(grabCount).Count;
                                break;
                                case MediaType.Teleplay:
                                successGrabCount = TeleplayGrab(grabCount).Count;
                                break;
                            default:
                                throw new ArgumentException("采集类型配置错误!");
                        }
                    }

                    watch.Stop();
                    responseMsg = String.Format("采集成功！共采集数据：{0}，耗时：{1} 毫秒", successGrabCount, watch.ElapsedMilliseconds);

                }
                catch (Exception e)
                {
                    responseMsg = e.Message;
                }
            }

            ViewData["ResponseMsg"] = responseMsg;

            return View("Index");
        }

        private List<Media> MovieGrab(int grabCount)
        {

            var movieGrabService = ServiceLocator.Current.GetInstance<IMovieGrabService>();
            

            if (grabCount > 0)
            {
                movieGrabService.GrabCount = grabCount;
            }

            //影片采集
            var movies = movieGrabService.Grab();

            if (movies != null && movies.Any())
            {
                MediaDataService.InsertBatch(movies);
            }

            return movies;
        }

        private List<Media> CartoonGrab(int grabCount)
        {
            var cartoonGrabService = ServiceLocator.Current.GetInstance<ICartoonGrabService>();
            
            if (grabCount > 0)
            {
                cartoonGrabService.GrabCount = grabCount;
            }

            //剧集采集
            var cartoons = cartoonGrabService.Grab();

            if (cartoons != null && cartoons.Any())
            {
                MediaDataService.InsertBatch(cartoons);
            }

            return cartoons;
        }

        private List<Media> TeleplayGrab(int grabCount)
        {
            var teleplayGrabService = ServiceLocator.Current.GetInstance<ITeleplayGrabService>();
            
            if (grabCount > 0)
            {
                teleplayGrabService.GrabCount = grabCount;
            }

            //剧集采集
            var teleplaies = teleplayGrabService.Grab();

            if (teleplaies != null && teleplaies.Any())
            {
                MediaDataService.InsertBatch(teleplaies);
            }

            return teleplaies;
        }
    }
}
