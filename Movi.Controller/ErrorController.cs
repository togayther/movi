using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Movi.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Page404()
        {
            return View("404");
        }

        public ActionResult Page500()
        {
            return View("500");
        }

        public ActionResult LowVersionBrowser()
        {
            return View("Browser");
        }
    }
}
