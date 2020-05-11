using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;

namespace CocukYazini.Controllers
{
    public class MessagesController : BaseController
    {
        // GET: Messages
        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()
        {
            //var okunmamis = from i in db.contacttables
            //                           where i.isaktif == 1
            //                           select i;

            var mesajlar = db.contacttables.Include("usertable").ToList();
          
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult MessageRead(int id)
        {
            var mesaj = db.contacttables.Find(id);
            return View("MessageRead", mesaj);
        }
        public ActionResult Sil(int id)
        {
            var mesajdelete = db.contacttables.Find(id);
            db.contacttables.Remove(mesajdelete);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult ReadedMessage(int id)
        {

            var readedmesaj = db.contacttables.Find(id);
            readedmesaj.isaktif = 2;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UnreadMessage(int id)
        {

            var readedmesaj = db.contacttables.Find(id);
            readedmesaj.isaktif = 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}