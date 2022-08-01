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
    public class DosyaController : Controller
    {
        // GET: Dosya
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()

        {
            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.monthtables where s.id > 1 && s.isaktif == 1
                           orderby s.datetime descending
                           select s;

            return View(degerler);

            // 
            

        }

        public ActionResult IndexEN()

        {
            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.monthtables
                           where s.id > 1 && s.isaktif == 3
                           orderby s.datetime descending
                           select s;

            return View(degerler);

            // 


        }

        public ActionResult AylikDosya(int id)

        {

            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            var degerler = from s in db.posttables
                           where s.isaktif == 1 && s.categoryid == 1 && s.monthid == id
                           orderby s.posttime descending
                           select s;


            var dosyaadi = (from x in db.monthtables
                           where x.id == id
                           select x).FirstOrDefault();

            ViewBag.DosyaAdi = dosyaadi.monthname;


            return View(degerler);




        }

        public ActionResult AylikDosyaEN(int id)

        {
            ViewBag.ebulten = TempData["ebulten"];
            ViewBag.login = TempData["login"];
            var degerler = from s in db.posttables
                           where s.isaktif == 3 && s.categoryid == 1 && s.monthid == id
                           orderby s.posttime descending
                           select s;

            var dosyaadi = (from x in db.monthtables
                            where x.id == id
                            select x).FirstOrDefault();

            ViewBag.DosyaAdi = dosyaadi.monthname;

            return View(degerler);




        }


    }
}
