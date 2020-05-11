using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;

namespace CocukYazini.Controllers
{

    
    public class HomeController : Controller
    {
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()
        {
            var degerler = (from s in db.slidertables
                            orderby s.sliderpath descending
                            select s).Take(5);

            return View(degerler);

        }
        [HttpPost]
        public ActionResult Login(usertable model)
        {
            var KULLANICI = db.usertables.FirstOrDefault(x => x.usermail == model.usermail && x.password == model.password);

            if (KULLANICI != null)
            {


                Session["kullaniciadi"] = KULLANICI;

                ViewBag.successMessage = "Başarılı Giriş";

                return RedirectToAction("Index");
            }
            //YALNIŞ GİRİŞ
            else
            {
                ViewBag.successMessage = "E-Posta ve ya Şifre Hatalı";
                return View("Index");
            }
        }

        public ActionResult LogOut()
        {

            Session.Remove("kullaniciadi");
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult NewUser(string username, string usersurname, string usermail, string password)
        {
            var kullanici = db.usertables.FirstOrDefault(x => x.usermail == usermail);
            if(kullanici != null){
                // Mükerrer kullanıcı
                return RedirectToAction("Index");

            }
                var ekle = new usertable
                {
                    username = username,
                    usersurname = usersurname,
                    password = password,
                    usermail = usermail,
                    authority = 2,
                    isaktif = 1,
                };

                if (Request.Files.Count > 0)
                {
                    var image = Request.Files[0]; //kanka bu resmi aliyor. gelen request'in icinde gorunuyor su anda resim.

                    if ((5 * 1024 * 1024) < image.ContentLength) //5MB
                        throw new Exception("hata mesaaji");

                    //burada content type denetlenebilir. sadece resimlerin gelebilmesi icin
                    //if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                    //{
                    //    throw new Exception("Admin_FileUploadDescriptionMessage");
                    //}

                    var fileInfo = new FileInfo(image.FileName);
                    var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name

                    var filePath = "/Photos/users/" + pic; //sen bunu db'ye yaz..

                    var tempFilePath = Server.MapPath("~\\Photos\\users\\" + pic);

                    image.SaveAs(tempFilePath);

                    ekle.userphoto = filePath;
                }

                db.usertables.Add(ekle);
                db.SaveChanges();
                return RedirectToAction("Index");
            
            


        }
        [HttpPost]
        public ActionResult NewContact(contacttable ekle)
        {

            var KULLANICI = db.usertables.FirstOrDefault();

            if (Session["kullaniciadi"] != null)
            {
                ekle.userid = KULLANICI.id;
            }
            
            ekle.isaktif = 1;
            ekle.contacttime = DateTime.Now;
            db.contacttables.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(usertable model,string body)
        {

            
            var mailadresi = db.usertables.FirstOrDefault(x => x.usermail == model.usermail);
            if (mailadresi != null)
            {
                
                




                return RedirectToAction("Index");

            }
            else
            {
                var HataMesaji = " Girmiş olduğunuz E-posta sistemimizde kayıtlı değil.";
                ViewBag.mesaj = HataMesaji;
                return RedirectToAction("ForgetPassword");
            }


        }

        [HttpPost]
        public ActionResult Comment(int postid , string comenttitle, string comentcontent, string whowrote)
        {

            var ekle = new comenttable
            {
                postid = postid,
                comenttitle = comenttitle,
                comentcontent = comentcontent,
                whowrote = whowrote,
                userid = null,
                isaktif = 1,
                comenttime = DateTime.Now,
            };
            db.comenttables.Add(ekle);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        public ActionResult CommentDelete(int id)
        {

           
            var commentdel = db.comenttables.Find(id);
            db.comenttables.Remove(commentdel);

            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());






        }

        public ActionResult ShowUser(int id)
        {
            var user = db.usertables.Find(id);

            var post = from x in db.posttables
                            where x.isaktif == 1 && x.userid == id
                            select x;

            

            ViewBag.postlar = post.Count();
            

            return View("ShowUser", user);

        }
        [HttpPost]
        public ActionResult UpdateUser(usertable p1)
        {
            var kullanici = db.usertables.Find(p1.id);
            if (p1.userphoto != null)
            {
                var image = Request.Files[0];

                if ((5 * 1024 * 1024) < image.ContentLength)
                    throw new Exception("hata mesaaji");

                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name

                var filePath = "/Photos/users/" + pic; //sen bunu db'ye yaz..

                var tempFilePath = Server.MapPath("~\\Photos\\users\\" + pic);

                image.SaveAs(tempFilePath);

                kullanici.userphoto = filePath;
            }

            kullanici.usermail = p1.usermail;
            kullanici.password = p1.password;
            kullanici.username = p1.username;
            kullanici.usersurname = p1.usersurname;
            db.SaveChanges();
            Session.Remove("kullaniciadi");
            Session.Abandon();
            Session.Clear();
            
            return RedirectToAction("Index");
        }
    }
}