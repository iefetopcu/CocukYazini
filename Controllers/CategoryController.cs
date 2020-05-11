using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CocukYazini.Models.Entity;

namespace CocukYazini.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category

        cocukyaziniEntities db = new cocukyaziniEntities();
        public ActionResult Index()
        {
            var degerler = db.categorytables.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult NewCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCategory(categorytable ekle)
        {
            db.categorytables.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var categorydelete = db.categorytables.Find(id);
            db.categorytables.Remove(categorydelete);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult CategoryGetir(int id)
        {
            var category = db.categorytables.Find(id);
            return View("CategoryGetir", category);
        }
        public ActionResult Guncelle(categorytable p1)
        {
            var category = db.categorytables.Find(p1.id);
            category.categoryname = p1.categoryname;
            category.isaktif = p1.isaktif;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}