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
    public class RoportajController : Controller
    {
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index(int sayfa = 1)

        {
            // aktif olan ve kategorisine göre döndürme işlemi yapar. 5
            var degerler = from s in db.posttables
                           where s.isaktif == 1 && s.categoryid == 5
                           orderby s.posttime descending
                           select s;

            var degerler2 = degerler.ToPagedList(sayfa, 10);
            return View(degerler2);

        }


        public ActionResult IndexEN(int sayfa = 1)

        {
            // aktif olan ve kategorisine göre döndürme işlemi yapar. 5
            var degerler = from s in db.posttables
                           where s.isaktif == 3 && s.categoryid == 5
                           orderby s.posttime descending
                           select s;

            var degerler2 = degerler.ToPagedList(sayfa, 10);
            return View(degerler2);

        }





    }

}