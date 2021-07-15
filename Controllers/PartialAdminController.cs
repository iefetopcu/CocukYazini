using CocukYazini.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CocukYazini.Controllers
{
    public class PartialAdminController : BaseController
    {
        // GET: PartialAdmin
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult DosyaUpload()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DosyaKaydet(string uploadname, string uploadurl)
        {
            var ekle = new uploadtable
            {
                uploadname = uploadname,
                
            };

            if (uploadurl != null)
            {
                var userphoto = Request.Files[0];
                var fileInfo = new FileInfo(userphoto.FileName);
                var pic = "Upload_" + DateTime.Now.Ticks + fileInfo.Extension;
                var filePath = "/Photos/Upload/" + pic;
                var copylink = "Photos/Upload/" + pic;
                var tempFilePath = Server.MapPath("~\\Photos\\Upload\\" + pic);
                userphoto.SaveAs(tempFilePath);
                ekle.uploadurl = filePath;
                ekle.uploadlink = copylink;
            }

            db.uploadtables.Add(ekle);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

    }
}