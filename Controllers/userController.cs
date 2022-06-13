using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEWS.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace NEWS.Controllers
{
    public class userController : Controller
    {
        iticontext db = new iticontext();
        // GET: user
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult add()
        {
            return View();
        } 
        [HttpPost]
        public ActionResult add(user ser)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(ser);
                db.SaveChanges();
                Session["id"] = ser.ID;
                return RedirectToAction("sellectbyuser","news",new{ id=Session["id"].ToString()});
            }
            return View();

        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(user us,bool rember)
        {
   
            user u1 = db.users.Where(n => n.E_mail == us.E_mail && n.Password == us.Password).SingleOrDefault();
            if (u1 != null)
            {
                if (rember)
                {
                    HttpCookie co = new HttpCookie("fullname");
                    co.Values.Add("id", us.ID.ToString());
                    co.Values.Add("name", us.Name);
                    co.Values.Add("mail", us.E_mail);
                    co.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(co);
                }
                Session["id"] = u1.ID;
                return RedirectToAction("sellectbyuser", "news", new { id = Session["id"].ToString()});
            }
            return View();
        }
        public ActionResult logout()
        {
            Session["id"] = null;
            HttpCookie c = new HttpCookie("fullname");
            c.Expires = DateTime.Now.AddDays(-30);
            Response.Cookies.Add(c);
            return RedirectToAction("index", "Home");
        }
        public ActionResult edite()
        {
            int id = int.Parse(Session["id"].ToString());
            user ne = db.users.Where(n => n.ID == id).Single();
            return View(ne);
        }
        [HttpPost]
        public ActionResult edite(user u1)
        {
            if (ModelState.IsValid)
            {
                user u = db.users.Where(n => n.ID == u1.ID).Single();
                u.Name = u1.Name;
                u.E_mail = u1.E_mail;
                db.SaveChanges();
            }
            return logout();
        }
        public ActionResult changepassword()
        {
           return View();
        }
        [HttpPost]
        public ActionResult changepassword(user usr ,string pass)
        {
                int id = int.Parse(Session["id"].ToString());
                
               user s = db.users.Where(n => n.ID == id).Single();
                if(usr.Password != s.Password)
                {
                    ViewBag.message = "Old Pass Not Correct";
                    return changepassword();
                }
                if (pass != "")
                {
                    ViewBag.pass = "invalid your password";
                    return changepassword();
                }
                s.Password = pass;
                s.Confirm_password = pass;
                db.SaveChanges();
                 return logout();
        }
        public ActionResult checkEmail(string email)
        {
           user u = db.users.Where(i => i.E_mail == email).FirstOrDefault();
            if (u != null)
            {
                return Json(false,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}