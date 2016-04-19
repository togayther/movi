using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Movi.Common;
using Movi.IGrabService;
using Movi.IService;
using Microsoft.Practices.ServiceLocation;
using Movi.Model;
using Movi.Model.Enums;

namespace Movi.Controllers
{
    public class VisitorController : BaseController
    {
        private static readonly IVisitorService VisitorService = ServiceLocator.Current.GetInstance<IVisitorService>();

        public void Index()
        {
            var vistorInfo = new Visitor
            {
                Id = StringHelper.GetGuidString(),
                Ip = HttpHelper.GetClientIPAddress(),
                Browser = HttpHelper.GetBrowser(),
                VisitTime = DateTime.Now,
                UserAgent = HttpHelper.GetUserAgent(),
                Source = HttpHelper.GetUrlReferrer()
            };

            VisitorService.Insert(vistorInfo);
        }
    }
}
