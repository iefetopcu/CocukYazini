using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CocukYazini.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            ViewBag.ebulten = TempData["ebulten"];
            return View();
        }

        public ActionResult IndexEN()
        {
            ViewBag.ebulten = TempData["ebulten"];
            return View();
        }
    }
}