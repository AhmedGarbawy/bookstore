using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEWS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Request.Cookies["fullname"] != null)
            {
                Session["id"] = Request.Cookies["fullname"].Values["id"];
                return RedirectToAction("sellectbyuser", "news", new { id = Session["id"].ToString()});
            }
            return RedirectToAction("Selectallnews", "news");
        }
    }
}