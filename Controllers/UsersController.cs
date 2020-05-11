using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;
using ImageMagick;

namespace CocukYazini.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Users
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()
        {
            var degeler = db.usertables.ToList();
            return View(degeler);
        }
        [HttpGet]
        
        public ActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewUser(string username, string usersurname, string usermail, string password, int authority, int isaktif)
        {
            var ekle = new usertable
            {
                username = username,
                usersurname = usersurname,
                password = password,
                usermail = usermail,
                authority = authority,
                isaktif = isaktif,
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
        

        
        public ActionResult ShowUser(int id)
        {
            var user = db.usertables.Find(id);
            return View("ShowUser", user);

            


        }
        [HttpPost]
        public ActionResult UpdateUser(usertable p1)
        {
            var kullanici = db.usertables.Find(p1.id);
            //var ekle = new usertable
            //{
            //    id = id,
            //    username = username,
            //    usersurname = usersurname,
            //    password = password,
            //    usermail = usermail,
            //    authority = authority,
            //    isaktif = isaktif,
                
                
            //};
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

            kullanici.authority = p1.authority;
            kullanici.usermail = p1.usermail;
            kullanici.isaktif = p1.isaktif;
            kullanici.password = p1.password;
            kullanici.username = p1.username;
            kullanici.usersurname = p1.usersurname;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var userdelete = db.usertables.Find(id);
            db.usertables.Remove(userdelete);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}