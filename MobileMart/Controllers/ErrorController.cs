using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileMart.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /Error/Page404
        public ActionResult Page404(string message)
        {
            ViewBag.message = message;
            return View();
        }
        // GET: /Error/Page500
        public ActionResult Page500()
        {
            return View();
        }
    }
}