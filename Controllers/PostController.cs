using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;
using OfficeOpenXml;
using PagedList;
using PagedList.Mvc;

namespace CocukYazini.Controllers
{
    public class PostController : BaseController
    {
        // GET: Post
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degeler = db.posttables.Include("usertable").ToList();
            var degerler = from s in db.posttables
                           orderby s.posttime descending
                           select s;
            var degerler2 = degerler.ToList().ToPagedList(sayfa, 10);
            //var degerler = db.posttables.Include("usertable").ToList().ToPagedList(sayfa, 10);
            return View(degerler2);
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

            List<SelectListItem> sorusturma = (from i in db.sorusturmas.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.sorusturmaname,
                                               Value = i.id.ToString()
                                           }).ToList();

            ViewBag.mon = months;
            ViewBag.cat = category;
            ViewBag.usr = users;
            ViewBag.srstrma = sorusturma;
            

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult NewPost(string posttitle, string postcontent, int categoryid, int userid, int isaktif, string previewphoto, string postphoto, string posttime, int monthid, int sorusturmaid, string postspot)
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
                sorusturmaid = sorusturmaid,
                postspot = postspot,
            };

            if (postphoto != null)
            {
                var image = Request.Files[0]; 
                

                if ((5 * 1024 * 1024) < image.ContentLength) //5MB
                    throw new Exception("hata mesaaji");

                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; 
                var filePath = "/Photos/post/" + pic; 
                var tempFilePath = Server.MapPath("~\\Photos\\post\\" + pic);
                image.SaveAs(tempFilePath);
                ekle.postphoto = filePath;
            }

            if(previewphoto != null)
            {
                var smallimage = Request.Files[1];
                var smallfileInfo = new FileInfo(smallimage.FileName);
                var smallpic = "smallpic_" + DateTime.Now.Ticks + smallfileInfo.Extension;
                var smallfilePath = "/Photos/post/" + smallpic;
                var smalltempFilePath = Server.MapPath("~\\Photos\\post\\" + smallpic);
                smallimage.SaveAs(smalltempFilePath);
                ekle.previewphoto = smallfilePath;
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

        [HttpGet]
        public ActionResult PostUpdate(int id)
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
                                              Text = i.username + " " + i.usersurname,
                                              Value = i.id.ToString()
                                          }).ToList();

            List<SelectListItem> sorusturma = (from i in db.sorusturmas.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.sorusturmaname,
                                                   Value = i.id.ToString()
                                               }).ToList();

            



            ViewBag.mon = months;
            ViewBag.cat = category;
            ViewBag.usr = users;
            ViewBag.srstrma = sorusturma;

            var post = db.posttables.Find(id);
            return View("PostUpdate",post);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostUpdate(posttable p1, string nopostphoto, string nopwphoto)
        {
            var post = db.posttables.Find(p1.id);
            var zaman = p1.posttime;
            
            
        if (p1.postphoto != null)
            {
                var image = Request.Files[0];
                if ((5 * 1024 * 1024) < image.ContentLength)
                    throw new Exception("hata mesaaji");
                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name
                var filePath = "/Photos/post/" + pic; //sen bunu db'ye yaz..
                var tempFilePath = Server.MapPath("~\\Photos\\post\\" + pic);
                image.SaveAs(tempFilePath);
                post.postphoto = filePath;
            }

            if (p1.previewphoto != null)
            {
                var image = Request.Files[1];
                if ((5 * 1024 * 1024) < image.ContentLength)
                    throw new Exception("hata mesaaji");
                var fileInfo = new FileInfo(image.FileName);
                var pic = "pic_" + DateTime.Now.Ticks + fileInfo.Extension; //new file name
                var filePath = "/Photos/post/" + pic; //sen bunu db'ye yaz..
                var tempFilePath = Server.MapPath("~\\Photos\\post\\" + pic);
                image.SaveAs(tempFilePath);
                post.previewphoto = filePath;
            }

            if (nopostphoto == "sil")
            {
                post.postphoto = null;
            }
            if (nopwphoto == "sil")
            {
                post.previewphoto = null;
            }

            post.monthid = p1.monthid;
            post.categoryid = p1.categoryid;
            post.userid = p1.userid;
            post.sorusturmaid = p1.sorusturmaid;
            post.postcontent = p1.postcontent;
            post.posttime = Convert.ToDateTime(zaman);
            post.posttitle = p1.posttitle;
            post.postspot = p1.postspot;
            post.isaktif = p1.isaktif;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void ExporToExcel()
        {
            var degerler = db.posttables.ToList();


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "ID";
            ws.Cells["B1"].Value = "BAŞLIK";
            ws.Cells["C1"].Value = "KATEGORİ";
            ws.Cells["D1"].Value = "YAZAR";
            ws.Cells["E1"].Value = "TARİH";
            ws.Cells["F1"].Value = "DOSYA ADI";


            int rowStart = 2;
            foreach (var item in degerler)
            {


                ws.Cells[String.Format("A{0}", rowStart)].Value = item.id;
                ws.Cells[String.Format("B{0}", rowStart)].Value = item.posttitle;
                ws.Cells[String.Format("C{0}", rowStart)].Value = item.categorytable.categoryname;
                ws.Cells[String.Format("D{0}", rowStart)].Value = item.usertable.username+" "+item.usertable.usersurname;
                ws.Cells[String.Format("E{0}", rowStart)].Value = item.posttime.ToString();
                ws.Cells[String.Format("F{0}", rowStart)].Value = item.monthtable.monthname;
                rowStart++;

            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + "IcerikDokumu_" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
        [HttpPost]
        public ActionResult Search(string search, int kategori)
        {
            if (kategori == 0)
            {
                var post = from x in db.posttables
                           select x;

                if (!String.IsNullOrEmpty(search))
                {
                    post = post.Where(s => s.posttitle.Contains(search) || s.usertable.username.Contains(search));

                }

                return View(post.ToList());
            }
            else
            {
                var post = from x in db.posttables
                           select x;

                if (!String.IsNullOrEmpty(search))
                {
                    post = post.Where(s => s.posttitle.Contains(search) && s.categoryid == kategori || s.usertable.username.Contains(search) && s.categoryid == kategori);

                }

                return View(post.ToList());

            }

        }

    }
}