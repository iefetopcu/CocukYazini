using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;

namespace CocukYazini.Controllers
{
    public class AdminController : BaseController //gibi. admin controller'ina da giremez boyle. buda ok
    {
        // GET: Admin
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()
        {

            
            ViewBag.icerikler = db.posttables.Count();
            ViewBag.mesajlar = db.contacttables.Count();
            ViewBag.kullanicilar = db.usertables.Count();
            ViewBag.kategoriler = db.categorytables.Count();

            var aktifpost = from x in db.posttables
                             where x.isaktif == 1
                             select x;

            var aktifkullanici = from x in db.usertables
                            where x.isaktif == 1
                            select x;

            var aktifkategori = from x in db.categorytables
                                 where x.isaktif == 1
                                 select x;

            var okunmamismesaj = from x in db.contacttables
                                 where x.isaktif == 1
                                 select x;

            var pasifpost = from x in db.posttables
                            where x.isaktif == 2
                            select x;

            var pasifkullanici = from x in db.usertables
                                 where x.isaktif == 2
                                 select x;

            var pasifkategori = from x in db.categorytables
                                where x.isaktif == 2
                                select x;

            var okunmusmesaj = from x in db.contacttables
                                 where x.isaktif == 2
                                 select x;

            ViewBag.postaktif = aktifpost.Count();
            ViewBag.kullaniciaktif = aktifkullanici.Count();
            ViewBag.kategoriaktif = aktifkategori.Count();
            ViewBag.mesajokunmamis = okunmamismesaj.Count();
            ViewBag.postpasif = pasifpost.Count();
            ViewBag.kullanicipasif = pasifkullanici.Count();
            ViewBag.kategoripasif = pasifkategori.Count();
            ViewBag.mesajokunmus = okunmusmesaj.Count();



            return View();
        }

        public ActionResult Slider()
        {

            var degerler = db.slidertables.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult NewSlider()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewSlider(string sliderpath, string sliderurl)
        {
            var ekle = new slidertable
            {
                sliderpath = sliderpath,
                sliderurl = sliderurl,
               
            };

            if (Request.Files.Count > 0)
            {
                var image = Request.Files[0]; //kanka bu resmi aliyor. gelen request'in icinde gorunuyor su anda resim.

                if ((5 * 1024 * 1024) < image.ContentLength) //5MB
                    throw new Exception("hata mesaaji");

                

                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name

                var filePath = "/Photos/sliderphotos/" + pic; //sen bunu db'ye yaz..

                var tempFilePath = Server.MapPath("~\\Photos\\sliderphotos\\" + pic);

                image.SaveAs(tempFilePath);

                ekle.sliderpath = filePath;
            }

            db.slidertables.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Slider");
        }
        public ActionResult SliderSil(int id)
        {
            var sliderdelete = db.slidertables.Find(id);
            db.slidertables.Remove(sliderdelete);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        public ActionResult Dosyaadi()
        {

            var degerler = db.monthtables.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniDosyaAdi()
        {
            return View();
        }
        public ActionResult DosyaAdiSil(int id)
        {
            var dosyadelete = db.monthtables.Find(id);
            db.monthtables.Remove(dosyadelete);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult YeniDosyaAdi(string monthname, string monthphotourl, string datetime)
        {
            string zaman = datetime;
            var ekle = new monthtable
            {
                monthname = monthname,
                monthphotourl = monthphotourl,
                datetime = Convert.ToDateTime(zaman),


            };

            if (Request.Files.Count > 0)
            {
                var image = Request.Files[0]; 

                



                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name

                var filePath = "/Photos/monthphotos/" + pic; //sen bunu db'ye yaz..

                var tempFilePath = Server.MapPath("~\\Photos\\monthphotos\\" + pic);

                image.SaveAs(tempFilePath);

                ekle.monthphotourl = filePath;
            }

            db.monthtables.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("DosyaAdi");
        }
        [HttpGet]
        public ActionResult DosyaUpdate(int id)
        {
            var dosyatable = db.monthtables.Find(id);
            return View("DosyaUpdate", dosyatable);




        }

        [HttpPost]
        public ActionResult DosyaGuncelle(monthtable p1)
        {
            var dosyalar = db.monthtables.Find(p1.id);

            if (p1.monthphotourl != null)
            {
                var image = Request.Files[0];

                if ((5 * 1024 * 1024) < image.ContentLength)
                    throw new Exception("hata mesaaji");

                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name

                var filePath = "/Photos/monthphotos/" + pic; //sen bunu db'ye yaz..

                var tempFilePath = Server.MapPath("~\\Photos\\monthphotos\\" + pic);

                image.SaveAs(tempFilePath);

                dosyalar.monthphotourl = filePath;
            }

            dosyalar.monthname = p1.monthname;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}