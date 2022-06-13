using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEWS.Models;
namespace NEWS.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        iticontext db = new iticontext();
        public ActionResult Index()
        {
            SelectList cat = new SelectList(db.categories.ToList(),"ID","NAME");
            return View(cat);
        }
        public ActionResult selectNewsByCatogery(int id)
        {
            List<news> newscat = db.news.Where(n => n.category_id == id).ToList();
            return PartialView(newscat);
        }
    }
}