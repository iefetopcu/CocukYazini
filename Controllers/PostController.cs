using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;

namespace CocukYazini.Controllers
{
    public class PostController : BaseController
    {
        // GET: Post
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()
        {
            var degeler = db.posttables.Include("usertable").ToList();
            return View(degeler);
        }
        [HttpGet]
        public ActionResult NewPost()
        {
            List<SelectListItem> category = (from i in db.categorytables.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.categoryname,
                                                 Value = i.id.ToString()
                                             }).ToList();
            List<SelectListItem> months = (from i in db.monthtables.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.monthname,
                                                 Value = i.id.ToString()
                                             }).ToList();
            
            

            List<SelectListItem> users = (from i in db.usertables.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.username +" "+ i.usersurname,
                                                 Value = i.id.ToString()
                                             }).ToList();


            ViewBag.mon = months;
            ViewBag.cat = category;
            ViewBag.usr = users;
            

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult NewPost(string posttitle, string postcontent, int categoryid, int userid, int isaktif, string previewphoto, string postphoto, string posttime, int monthid)
        {
            string zaman = posttime;
            
            
            

            var ekle = new posttable
            {
                posttitle = posttitle,
                postcontent = postcontent,
                categoryid = categoryid,
                userid = userid,
                previewphoto = previewphoto,
                postphoto = postphoto,
                isaktif = isaktif,
                posttime = Convert.ToDateTime(zaman),
                monthid = monthid,
               
            




        };

            if (Request.Files.Count > 0)
            {
                var image = Request.Files[0]; //kanka bu resmi aliyor. gelen request'in icinde gorunuyor su anda resim.
                var smallimage = Request.Files[1];

                if ((5 * 1024 * 1024) < image.ContentLength) //5MB
                    throw new Exception("hata mesaaji");


                var fileInfo = new FileInfo(image.FileName);
                var smallfileInfo = new FileInfo(smallimage.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name
                var smallpic = "smallpic_" + DateTime.Now.Ticks + smallfileInfo.Extension;
                var filePath = "/Photos/post/" + pic; //sen bunu db'ye yaz..
                var smallfilePath = "/Photos/post/" + smallpic;
                var tempFilePath = Server.MapPath("~\\Photos\\post\\" + pic);
                var smalltempFilePath = Server.MapPath("~\\Photos\\post\\" + smallpic);

                image.SaveAs(tempFilePath);
                smallimage.SaveAs(smalltempFilePath);
                ekle.previewphoto = smallfilePath;
                ekle.postphoto = filePath;
            }

            db.posttables.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Sil(int id)
        {

            var comments = db.comenttables.Where(c => c.postid == id);
            db.comenttables.RemoveRange(comments);
            var postdelete = db.posttables.Find(id);
            db.posttables.Remove(postdelete);

            db.SaveChanges();
            return RedirectToAction("Index");
            
                
            

            

        }

        public ActionResult PostRead(int id)
        {
            var post = db.posttables.Find(id);
            return View("PostRead", post);
        }



    }
}