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
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.sorusturmas
                           where s.id > 1 && s.isaktif == 1
                           orderby s.datetime descending
                           select s;

            return View(degerler);
        }

        public ActionResult IndexEN()

        {
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.sorusturmas
                           where s.id > 1 && s.isaktif == 3
                           orderby s.datetime descending
                           select s;

            return View(degerler);
        }

        public ActionResult Sorusturmalar(int id)

        {



            var degerler = from s in db.posttables
                           where s.isaktif == 1 && s.categoryid == 4 && s.sorusturmaid == id
                           orderby s.posttime descending
                           select s;


            return View(degerler);


        }

        public ActionResult SorusturmalarEN(int id)

        {



            var degerler = from s in db.posttables
                           where s.isaktif == 3 && s.categoryid == 4 && s.sorusturmaid == id
                           orderby s.posttime descending
                           select s;


            return View(degerler);


        }
    }
}