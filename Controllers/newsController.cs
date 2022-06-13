using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using NEWS.Models;

namespace NEWS.Controllers
{
    public class newsController : Controller
    {
        // GET: news
        iticontext db = new iticontext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Selectallnews()
        {
          
            List<news> Nv = db.news.OrderByDescending(n => n.date).ToList();
            return View(Nv);
        }
        public ActionResult MoreInfo(int id)
        {
           news more=db.news.Where(n => n.ID == id).FirstOrDefault();
            return View(more);
        }
       public ActionResult Add()
        {
            ViewBag.list = new SelectList(db.categories.ToList(), "ID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Add(news s,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image.FileName != null)
                {
                    image.SaveAs(Server.MapPath("~/img/") + image.FileName);
                    s.photo = image.FileName;
                }
           
                db.news.Add(s);
                db.SaveChanges();
                return RedirectToAction("Selectallnews");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edite(int id)
        {
            news ne = db.news.Where(n => n.ID == id).SingleOrDefault();
            if (ne != null)
            {
                ViewBag.leg = new SelectList(db.categories.ToList(), "ID", "Name");
                return View(ne);
            }
            return RedirectToAction("sellectbyuser");
        }
        [HttpPost]
        public ActionResult Edite(news s ,HttpPostedFileBase image)
        {

            news s1 =db.news.Where(n => n.ID == s.ID).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if(image.FileName != null)
                {
                    image.SaveAs(Server.MapPath("~/img/") + image.FileName);
                    s1.photo = image.FileName;
                }
                s1.Descr = s.Descr;
                s1.Tittle = s.Tittle;
                s1.date = s.date;
                s1.category_id = s.category_id;
                //db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("sellectbyuser");
            //news rsu = db.news.Where(n => n.ID == s.ID).FirstOrDefault();

        }
        public ActionResult sellectbyuser()
        {
            int id = int.Parse(Session["id"].ToString());
            List<news> coll = db.news.Where(n => n.user_id == id).ToList();
            return View(coll);
        }
        public ActionResult Selecetbycategory(int id)
        {
            List<news> li = db.news.Where(n => n.category_id == id).OrderByDescending(n=>n.date).ToList();
            return View("Selectallnews", li);
        }
    }
}