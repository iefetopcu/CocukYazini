using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;
using System.Data.Entity;

namespace CocukYazini.Controllers
{

    
    public class HomeController : Controller
    {
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()
        {
            var degerler = (from s in db.slidertables
                            orderby s.sliderpath descending
                            where s.isaktif == 1
                            select s).Take(5);

            return View(degerler);

        }

        public ActionResult IndexEN()
        {
            var degerler = (from s in db.slidertables
                            where s.isaktif == 3
                            orderby s.sliderpath descending
                            select s).Take(5);

            return View(degerler);

        }
        [HttpPost]
        public ActionResult Login(usertable model)
        {
            var CocukYaziniUser = db.usertables.FirstOrDefault(x => x.usermail == model.usermail && x.password == model.password);

            if (CocukYaziniUser != null)
            {


                Session["MySessionUser"] = CocukYaziniUser;

                
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

            Session.Remove("MySessionUser");
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

            if (Session["MySessionUser"] != null)
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

        [HttpGet]
        public ActionResult ForgetPasswordEN()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(usertable k)
        {            
            var mailadresi = db.usertables.Where(x => x.usermail == k.usermail).FirstOrDefault();
            if (mailadresi != null)
            {
                Guid rastgele = Guid.NewGuid();
                mailadresi.password = rastgele.ToString().Substring(0, 8);
                db.SaveChanges();

                SmtpClient client = new SmtpClient("mail.cocukyazini.com",587);

                //client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("info@cocukyazini.com","Şifre Sıfırlama - Çocuk Yazını");
                mail.To.Add(mailadresi.usermail);
                mail.IsBodyHtml = true;
                mail.Subject = "Şifre Değiştirme İsteği";
                mail.Body += "Merhaba " + mailadresi.username + "<br/>" + "<b>Kullanıcı Mail Adresi :</b> " + mailadresi.usermail + "<br/>"
                    + " <b>Şifreniz :</b> " + mailadresi.password
                    + "<br /> <br />" + "Eğer şifrenin başkası tarafından değiştirildiğini düşünüyorsan bizimle iletişime geçebilirsin. </br> info@cocukyazini.com <br/><br/><br/>"
                    + "<a href='https://cocukyazini.com/'>Çocuk Yazını</a> <br /> <br /> <img src='https://cocukyazini.com/Photos/logo/cocuk.png' height='100px' />"
                    ;

                NetworkCredential net = new NetworkCredential("info@cocukyazini.com", "CocukYazinicom1234!");
                client.Credentials = net;
                client.Send(mail);

                return RedirectToAction("Index");

            }
            else
            {
                var HataMesaji = " Girmiş olduğunuz E-posta sistemimizde kayıtlı değil.";
                ViewBag.mesaj = HataMesaji;
                return View();
            }


        }
        [HttpPost]
        public ActionResult ForgetPasswordEN(usertable k)
        {


            var mailadresi = db.usertables.Where(x => x.usermail == k.usermail).FirstOrDefault();

            if (mailadresi != null)
            {
                Guid rastgele = Guid.NewGuid();
                mailadresi.password = rastgele.ToString().Substring(0, 8);
                db.SaveChanges();

                SmtpClient client = new SmtpClient("mail.cocukyazini.com", 587);

                //client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("info@cocukyazini.com", "Şifre Sıfırlama - Çocuk Yazını");
                mail.To.Add(mailadresi.usermail);
                mail.IsBodyHtml = true;
                mail.Subject = "Change Password - Children's Literature";
                mail.Body += "Hello " + mailadresi.username + "<br/>" + "<b>User Mail :</b> " + mailadresi.usermail + "<br/>"
                    + " <b>Your New Password :</b> " + mailadresi.password
                    + "<br /> <br />" + "If you think your password has been changed by someone else, you can contact us.. </ br> info@cocukyazini.com <br/><br/><br/>"
                    + "<a href='https://cocukyazini.com/'>Çocuk Yazını</a> <br /> <br /> <img src='https://cocukyazini.com/Photos/logo/cocuk.png' height='100px' />"
                    ;

                NetworkCredential net = new NetworkCredential("info@cocukyazini.com", "CocukYazinicom1234!");
                client.Credentials = net;
                client.Send(mail);

                return RedirectToAction("Index");

            }
            else
            {
                var HataMesaji = " The email you entered is not registered in system..";
                ViewBag.mesaj = HataMesaji;
                return View();
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

        public ActionResult ShowUser()
        {

            usertable currentUser = (usertable)Session["MySessionUser"];
            
            if(currentUser != null)
            {
                var user = db.usertables.Find(currentUser.id);

                var post = from x in db.posttables
                           where x.isaktif == 1 && x.userid == currentUser.id
                           select x;



                ViewBag.postlar = post.Count();


                return View("ShowUser", user);

            }
            else
            {
                return RedirectToAction ("Index");
            }



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
            Session.Remove("MySessionUser");
            Session.Abandon();
            Session.Clear();
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Search(string search, int kategori)
        {
            if(kategori == 0)
            {
                var post = from x in db.posttables
                           select x;

                if (!String.IsNullOrEmpty(search))
                {
                    post = post.Where(s => s.posttitle.Contains(search));
                    
                }

                return View(post.ToList());
            }
            else
            {
                var post = from x in db.posttables
                           select x;

                if (!String.IsNullOrEmpty(search))
                {
                    post = post.Where(s => s.posttitle.Contains(search) && s.categoryid == kategori);

                }

                return View(post.ToList());

            }
           
        }
        [HttpPost]
        public ActionResult SearchEN(string search, int kategori)
        {
            if (kategori == 0)
            {
                var post = from x in db.posttables
                           select x;

                if (!String.IsNullOrEmpty(search))
                {
                    post = post.Where(s => s.posttitle.Contains(search));

                }

                return View(post.ToList());
            }
            else
            {
                var post = from x in db.posttables
                           select x;

                if (!String.IsNullOrEmpty(search))
                {
                    post = post.Where(s => s.posttitle.Contains(search) && s.categoryid == kategori);

                }

                return View(post.ToList());

            }

        }

        [Route("PostRead/{id}/{baslik}")]
        public ActionResult PostRead(int id)
        {
            var post = db.posttables.Include(a => a.comenttables).FirstOrDefault(a => a.id == id);
            var yorumlar = db.posttables.Include(a => a.comenttables);
            if (post == null)
                return RedirectPermanent("/");

            //var userotherpost = from s in db.posttables
            //                    where s.userid == post.userid
            //                    orderby s.posttime
            //                    descending
            //                    select s;

            return View("PostRead", post);
        }

        public ActionResult PostReadEN(int id)
        {
            var post = db.posttables.Include(a => a.comenttables).FirstOrDefault(a => a.id == id);
            if (post == null)
                return RedirectPermanent("/");

            return View("PostReadEN", post);
        }

        public ActionResult UserPosts(int id)
        {

            var userposts = from s in db.posttables
                           where s.userid == id && s.isaktif == 1
                           orderby s.posttime descending
                           select s;

            return View("UserPosts", userposts);

        }

        public ActionResult UserPostsEN(int id)
        {

            var userposts = from s in db.posttables
                            where s.userid == id && s.isaktif == 3
                            orderby s.posttime descending
                            select s;

            return View("UserPostsEN", userposts);

        }


    }
}