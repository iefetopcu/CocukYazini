using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;

namespace CocukYazini.Controllers
{
    public class DosyaController : Controller
    {
        // GET: Dosya
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()

        {
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.monthtables where s.id > 1
                           orderby s.datetime descending
                           select s;

            return View(degerler);

            // 
            

        }

        public ActionResult AylikDosya(int id)

        {

            

            var degerler = from s in db.posttables
                           where s.isaktif == 1 && s.categoryid == 1 && s.monthid == id
                           orderby s.posttime descending
                           select s;

            return View(degerler);




        }


    }
}
