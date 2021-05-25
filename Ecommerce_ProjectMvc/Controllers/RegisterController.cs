using Ecommerce_ProjectMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecommerce_ProjectMvc.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register

        public ActionResult Signin()
        {
            var userInCookie = Request.Cookies["UserInfo"];
            if (userInCookie != null)
            {
                return RedirectToAction("Index", "Home1");
            }
            else
            {
                var adminInCookie = Request.Cookies["AdminInfo"];
                if (adminInCookie != null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Signin(Tbl_user model)
        {
            using (var context = new Ecommerce_ProjectEntities())
            {
                context.Tbl_user.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");

        }

        public ActionResult Login()
        {
            var userInCookie = Request.Cookies["UserInfo"];
            if (userInCookie != null)
            {
                return RedirectToAction("Index", "Home1");
            }
            else
            {
                var adminInCookie = Request.Cookies["AdminInfo"];
                if (adminInCookie != null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View();
                }
            }
       
        }

        [HttpPost]
        public ActionResult Login(Models.Tbl_user model)
        {
            using (var context = new Ecommerce_ProjectEntities())
            {
                
                var data = context.Tbl_user.Where(s => s.U_name.Equals(model.U_name) && s.U_name.Equals(model.U_password)).ToList();
                if(data.Count() > 0)
                {
                    HttpCookie cooskie = new HttpCookie("UserInfo");
                    cooskie.Values["idUser"] = Convert.ToString(data.FirstOrDefault().U_id);
                    cooskie.Values["FullName"] = Convert.ToString(data.FirstOrDefault().U_name);
                    cooskie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cooskie);
                    return RedirectToAction("Index", "Home1");
                }
               
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }


        }
       
        public ActionResult Logout()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("UserInfo"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["UserInfo"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}