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
    public class SorusturmaController : Controller
    {
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()

        {
            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.sorusturmas
                           where s.id > 1 && s.isaktif == 1
                           orderby s.datetime descending
                           select s;

            return View(degerler);
        }

        public ActionResult IndexEN()

        {
            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.sorusturmas
                           where s.id > 1 && s.isaktif == 3
                           orderby s.datetime descending
                           select s;

            return View(degerler);
        }

        public ActionResult Sorusturmalar(int id)

        {
            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            var degerler = from s in db.posttables
                           where s.isaktif == 1 && s.categoryid == 4 && s.sorusturmaid == id
                           orderby s.posttime descending
                           select s;

            var sorusturmaadi = (from x in db.sorusturmas
                                 where x.id == id
                                 select x).FirstOrDefault();

            ViewBag.SorusturmaAdi = sorusturmaadi.sorusturmaname;

            return View(degerler);


        }

        public ActionResult SorusturmalarEN(int id)

        {
            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            var degerler = from s in db.posttables
                           where s.isaktif == 3 && s.categoryid == 4 && s.sorusturmaid == id
                           orderby s.posttime descending
                           select s;

            var sorusturmaadi = (from x in db.sorusturmas
                            where x.id == id
                            select x).FirstOrDefault();

            ViewBag.SorusturmaAdi = sorusturmaadi.sorusturmaname;
            return View(degerler);


        }
    }
}