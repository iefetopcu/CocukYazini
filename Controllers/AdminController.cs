using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;
using OfficeOpenXml;

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
        public ActionResult NewSlider(string sliderpath, string sliderurl,int isaktif)
        {
            var ekle = new slidertable
            {
                sliderpath = sliderpath,
                sliderurl = sliderurl,
                isaktif = isaktif,

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
        public ActionResult YeniDosyaAdi(string monthname, string monthphotourl, string datetime, int isaktif)
        {
            string zaman = datetime;
            var ekle = new monthtable
            {
                monthname = monthname,
                monthphotourl = monthphotourl,
                datetime = Convert.ToDateTime(zaman),
                isaktif = isaktif,

            };

            if (monthphotourl != null)
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
            var zaman = p1.datetime;

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
            dosyalar.datetime = Convert.ToDateTime(zaman);
            db.SaveChanges();
            return RedirectToAction("DosyaAdi");
        }

        public ActionResult Sorusturma()
        {

            var degerler = db.sorusturmas.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSorusturma()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSorusturma(string sorusturmaname, string sorusturmaphotourl, string datetime , int isaktif)
        {
            string zaman = datetime;
            var ekle = new sorusturma
            {
                sorusturmaname = sorusturmaname,
                sorusturmaphotourl = sorusturmaphotourl,
                datetime = Convert.ToDateTime(zaman),
                isaktif = isaktif,

            };

            if (sorusturmaphotourl != null)
            {
                var image = Request.Files[0];
                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name
                var filePath = "/Photos/sorusturma/" + pic; //sen bunu db'ye yaz..
                var tempFilePath = Server.MapPath("~\\Photos\\sorusturma\\" + pic);
                image.SaveAs(tempFilePath);
                ekle.sorusturmaphotourl = filePath;
            }

            db.sorusturmas.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Sorusturma");
        }
        public ActionResult SorusturmaSil(int id)
        {
            var sorusturmadelete = db.sorusturmas.Find(id);
            db.sorusturmas.Remove(sorusturmadelete);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult SorusturmaUpdate(int id)
        {
            var sorusturmatable = db.sorusturmas.Find(id);
            return View("SorusturmaUpdate", sorusturmatable);
        }

        [HttpPost]
        public ActionResult SorusturmaGuncelle(sorusturma p1)
        {
            var sorusturmalar = db.sorusturmas.Find(p1.id);
            var zaman = p1.datetime;

            if (p1.sorusturmaphotourl != null)
            {
                var image = Request.Files[0];

                if ((5 * 1024 * 1024) < image.ContentLength)
                    throw new Exception("hata mesaaji");

                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name

                var filePath = "/Photos/sorusturma/" + pic; //sen bunu db'ye yaz..

                var tempFilePath = Server.MapPath("~\\Photos\\sorusturma\\" + pic);

                image.SaveAs(tempFilePath);

                sorusturmalar.sorusturmaphotourl = filePath;
            }

            sorusturmalar.sorusturmaname = p1.sorusturmaname;
            sorusturmalar.datetime = Convert.ToDateTime(zaman);
            db.SaveChanges();
            return RedirectToAction("Sorusturma");
        }

        public ActionResult DosyaUpload()
        {
            var degerler = db.uploadtables.ToList();
            return View(degerler);
        }
        public ActionResult UploadSil(int id)
        {
            var uploaddelete = db.uploadtables.Find(id);
            db.uploadtables.Remove(uploaddelete);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        public ActionResult Bulten()
        {
            var bulten = (from x in db.ebultens
                          orderby x.id descending
                          select x).ToList();
            return View(bulten);
        }

        public ActionResult BultenSil(int id)
        {
            var bulten = db.ebultens.Find(id);
            db.ebultens.Remove(bulten);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult BultenGonder()
        {        
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BultenGonder(string title, string icerik)
        {
            var bultenlist = (from x in db.ebultens                             
                             select x).ToList();
            foreach (var item in bultenlist)
            {
                using(var client = new SmtpClient("mail.cocukyazini.com", 587))
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("info@cocukyazini.com", "Çocuk Yazını");
                    mail.To.Add(item.ebultenemail);
                    mail.IsBodyHtml = true;
                    mail.Subject = title;
                    mail.Body += icerik;

                    NetworkCredential net = new NetworkCredential("info@cocukyazini.com", "CocukYazinicom1234!");
                    client.Credentials = net;
                    client.Send(mail);
                }
            }
           return RedirectToAction("Index");

        }

        public void EbultenExcel()
        {
            var degerler = db.ebultens.ToList();


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Email Adresi";
            


            int rowStart = 2;
            foreach (var item in degerler)
            {
                ws.Cells[String.Format("A{0}", rowStart)].Value = item.ebultenemail;              
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + "E_Bulten_Listesi_" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

        public ActionResult CocukYaziniTV()
        {
            var videos = (from s in db.videotables
                          orderby s.videotime descending
                          select s).ToList();
            return View(videos);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult VideoAdd(string videotitle, string videodescription, string videotime, string videourl)
        {
            string rtime = videotime;
            //DateTime rtime = DateTime.ParseExact(releasetime, "yyyy/MM/dd", null);
            var add = new videotable
            {
                videotitle = videotitle,
                videodescription = videodescription,               
                videourl = videourl,                
                videotime = Convert.ToDateTime(rtime),
            };
            
            db.videotables.Add(add);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        public ActionResult VideoDelete(int id)
        {            
            var video = db.videotables.Find(id);                     
            db.videotables.Remove(video);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult VideoUpdate(int id)
        {
            var video = db.videotables.Find(id);
            return View("VideoUpdate", video);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult VideoUpdate(videotable p1)
        {           
            var video = db.videotables.Find(p1.id);
            var zaman = p1.videotime;
            video.videotitle = p1.videotitle;
            video.videodescription = p1.videodescription;
            video.videourl = p1.videourl;                 
            video.videotime = Convert.ToDateTime(zaman);          
            db.SaveChanges();
            return RedirectToAction("CocukYaziniTV","Admin");
        }

        public ActionResult Reklam()
        {
            var reklam = (from s in db.adtables
                          select s).ToList();

            return View(reklam);                       
        }
        [HttpPost]
        public ActionResult AddReklam(string reklamstart, string reklamend, string reklamurl, string reklamphotourl, string format)
        {
            string stime = reklamstart;
            string etime = reklamend;
            var add = new adtable
            {
                reklamurl = reklamurl,
                reklamstart = Convert.ToDateTime(stime),
                reklamend = Convert.ToDateTime(etime),
            };

            if(format == "footer")
            {
                add.reklamfooter = "true";
            }
            else
            {
                add.reklamside = "true";
            }
            if (reklamphotourl != null)
            {
                var image = Request.Files[0];
                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension;
                var filePath = "/Photos/post/" + pic;
                var tempFilePath = Server.MapPath("~\\Photos\\post\\" + pic);
                image.SaveAs(tempFilePath);
                add.reklamphotourl = filePath;
            }

            db.adtables.Add(add);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        public ActionResult DeleteAd(int id)
        {
            var reklam = db.adtables.Find(id);
            db.adtables.Remove(reklam);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
    }
}