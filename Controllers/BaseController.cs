using CocukYazini.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CocukYazini.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Session["kullaniciadi"] != null)
            {
                var session = (usertable)filterContext.HttpContext.Session["kullaniciadi"];
                if (session.authority == 1)// session acan kullanicilarin eger 1 degilse giris yapamaz. giris yapmak istemedikleri controller'a bu baseController'dan turetirsen is cozulur.
                { 
                    base.OnActionExecuting(filterContext);
                    return;

                }
                    
                    
                    
                    
                else
                {
                    filterContext.Result = new RedirectResult("/");
                    base.OnActionExecuting(filterContext);
                }
            }

            filterContext.Result = new RedirectResult("/");
            base.OnActionExecuting(filterContext);
        }
    }
}