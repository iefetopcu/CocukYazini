using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;


namespace CocukYazini.Controllers
{
    public class RoportajController : Controller
    {
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()

        {
            // aktif olan ve kategorisine göre döndürme işlemi yapar.
            var degerler = from s in db.posttables
                           where s.isaktif == 1 && s.categoryid == 5
                           orderby s.posttime descending
                           select s;


            return View(degerler);

        }
    }
}