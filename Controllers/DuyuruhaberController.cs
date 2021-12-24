using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace CocukYazini.Controllers
{
    public class DuyuruhaberController : Controller
    {
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index(int sayfa=1)

        {
            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.posttables
                           where s.isaktif == 1 && s.categoryid == 6
                           orderby s.posttime descending
                           select s;

            var degerler2 = degerler.ToPagedList(sayfa, 10);

            return View(degerler2);

        }

        public ActionResult IndexEN(int sayfa = 1)

        {
            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.posttables
                           where s.isaktif == 3 && s.categoryid == 6
                           orderby s.posttime descending
                           select s;

            var degerler2 = degerler.ToPagedList(sayfa, 10);

            return View(degerler2);

        }
    }
}