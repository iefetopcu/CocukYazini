using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;

namespace CocukYazini.Controllers
{
    public class PartialController : Controller
    {
        cocukyaziniEntities db = new cocukyaziniEntities();

        // GET: Partial
        public ActionResult footer()
        {      
            var degerler = (from s in db.posttables
                           where s.isaktif == 1 
                           orderby s.posttime descending
                           select s).Take(3);                     
            return PartialView(degerler);
        }

        public ActionResult homepost()
        {
            var postlar = (from s in db.posttables
                           where s.isaktif == 1
                           orderby s.posttime descending
                           select s).Take(12);

            return PartialView(postlar);
        }

        public ActionResult duyuruhaber()
        {
            var duyuruhaber = (from s in db.posttables
                          where s.isaktif == 1 && s.categoryid == 6
                               orderby s.posttime descending
                          select s).Take(3);

            return PartialView(duyuruhaber);
        }

        public ActionResult footerEN()
        {


            var degerler = (from s in db.posttables
                            where s.isaktif == 3
                            orderby s.posttime descending
                            select s).Take(3);





            return PartialView(degerler);
        }

        public ActionResult homepostEN()
        {


            var postlar = (from s in db.posttables
                           where s.isaktif == 3
                           orderby s.posttime descending
                           select s).Take(12);

            return PartialView(postlar);
        }

        public ActionResult duyuruhaberEN()
        {


            var duyuruhaber = (from s in db.posttables
                               where s.isaktif == 3 && s.categoryid == 6
                               orderby s.posttime descending
                               select s).Take(3);

            return PartialView(duyuruhaber);
        }
        public ActionResult sidebarreklam()
        {
            var ad = db.adtables.Where(x => x.reklamstart < DateTime.Now && x.reklamend > DateTime.Now && x.reklamside == "true").OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            return PartialView(ad);
        }
        public ActionResult footerreklam()
        {
            var ad = db.adtables.Where(x => x.reklamstart < DateTime.Now && x.reklamend > DateTime.Now && x.reklamfooter == "true").OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            return PartialView(ad);
        }

        public ActionResult postvitrin()
        {
            var postlar = (from s in db.posttables
                           where s.isaktif == 1 && s.postvitrin == "true"
                           orderby s.posttime descending
                           select s).Take(6);

            return PartialView(postlar);
        }

        public ActionResult postvitrinEN()
        {
            var postlar = (from s in db.posttables
                           where s.isaktif == 3 && s.postvitrin == "true"
                           orderby s.posttime descending
                           select s).Take(6);

            return PartialView(postlar);
        }

        public ActionResult homepopuler()
        {
            var postlar = (from s in db.posttables
                           where s.isaktif == 1 
                           orderby s.postviewcount descending
                           select s).Take(4);

            return PartialView(postlar);
        }

        public ActionResult homepopulerEN()
        {
            var postlar = (from s in db.posttables
                           where s.isaktif == 3
                           orderby s.postviewcount descending
                           select s).Take(4);

            return PartialView(postlar);
        }
    }
}