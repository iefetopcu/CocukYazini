﻿using System;
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
                           select s).Take(8);

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
                           select s).Take(8);

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

        //public ActionResult menuler()
        //{


        //    var kategoriler = db.categorytables.ToList();

        //    return PartialView(kategoriler);
        //}


    }
}