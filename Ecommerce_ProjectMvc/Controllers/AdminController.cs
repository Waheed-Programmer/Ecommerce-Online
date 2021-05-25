
using Ecommerce_ProjectMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecommerce_ProjectMvc.Controllers
{
    public class AdminController : Controller
    {
        Ecommerce_ProjectEntities DB = new Ecommerce_ProjectEntities();
        // GET: Admin
        public ActionResult Index()
        {
            var adminInCookie = Request.Cookies["AdminInfo"];
            if (adminInCookie != null)
            {
                return View(DB.Tbl_product.ToList());
            }
            else
            {
                var userInCookie = Request.Cookies["UserInfo"];
                if (userInCookie != null)
                {
                    return RedirectToAction("Index", "Home1");
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }



        }

        public ActionResult Create()
        {


            var adminInCookie = Request.Cookies["AdminInfo"];
            if (adminInCookie != null)
            {
                return View();
            }
            else
            {
                var userInCookie = Request.Cookies["UserInfo"];
                if (userInCookie != null)
                {
                    return RedirectToAction("Index", "Home1");
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }

        }
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Tbl_product emp)
        {
            string filename = Path.GetFileName(file.FileName);

            string extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/upload/"), filename);
            emp.Pro_image = "~/upload/" + file.FileName;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (file.ContentLength <= 1000000)
                {
                    DB.Tbl_product.Add(emp);
                    if (DB.SaveChanges() > 0)
                    {
                        file.SaveAs(path);
                        ViewBag.msg = "Record Added";
                        ModelState.Clear();
                    }
                }
                else
                {
                    ViewBag.msg = "Size is not valid";
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var adminInCookie = Request.Cookies["AdminInfo"];
            if (adminInCookie != null)
            {
                Tbl_product tbl_img = DB.Tbl_product.Find(id);



                if (tbl_img == null)
                {
                    return HttpNotFound();
                }
                return View(tbl_img);
            }
            else
            {
                var userInCookie = Request.Cookies["UserInfo"];
                if (userInCookie != null)
                {
                    return RedirectToAction("Index", "Home1");
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }





        }
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Tbl_product Emp)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/upload"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    string filename = file.FileName;
                    ViewBag.Message = "File uploaded successfully";
                    Emp.Pro_image = "~/upload/" + filename;

                    DB.Entry(Emp).State = EntityState.Modified;
                    DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return View();





        }

        public ActionResult Details(int? id)
        {
            var adminInCookie = Request.Cookies["AdminInfo"];
            if (adminInCookie != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var tbl_img = DB.Tbl_product.Find(id);


                if (tbl_img == null)
                {
                    return HttpNotFound();
                }
                return View(tbl_img);
            }
            else
            {
                var userInCookie = Request.Cookies["UserInfo"];
                if (userInCookie != null)
                {
                    return RedirectToAction("Index", "Home1");
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }







        }

        public ActionResult Delete(int? id)
        {

            var adminInCookie = Request.Cookies["AdminInfo"];
            if (adminInCookie != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var tbl_img = DB.Tbl_product.Find(id);


                if (tbl_img == null)
                {
                    return HttpNotFound();
                }
                string currentimg = Request.MapPath(tbl_img.Pro_image);
                DB.Entry(tbl_img).State = EntityState.Deleted;
                if (DB.SaveChanges() > 0)
                {
                    if (System.IO.File.Exists(currentimg))
                    {
                        System.IO.File.Delete(currentimg);
                    }
                    TempData["msg"] = "Data deleted!";
                    return RedirectToAction("Index");
                }
                return View();
            }
            else
            {
                var userInCookie = Request.Cookies["UserInfo"];
                if (userInCookie != null)
                {
                    return RedirectToAction("Index", "Home1");
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }


        }

        public ActionResult Login()
        {
            var adminInCookie = Request.Cookies["AdminInfo"];
            if (adminInCookie != null)
            {
                return RedirectToAction("Index", "Admin"); ;
            }
            else
            {
                var userInCookie = Request.Cookies["UserInfo"];
                if (userInCookie != null)
                {
                    return RedirectToAction("Index", "Home1");
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Login(Models.Admin_Tbl model)
        {
            using (var context = new Ecommerce_ProjectEntities())
            {
                bool isvalid = context.Admin_Tbl.Any(x => x.Email == model.Email
                && x.Password == model.Password);
                if (isvalid)
                {
                    HttpCookie cooskie = new HttpCookie("AdminInfo");
                    cooskie.Values["idAdmin"] = Convert.ToString(model.AdminId);
                    cooskie.Values["Email"] = Convert.ToString(model.Email);
                    cooskie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cooskie);
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }


        }
        public ActionResult Logout()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("AdminInfo"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["AdminInfo"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Login");
        }
        public ActionResult ListOrder()
        {
            Ecommerce_ProjectEntities db = new Ecommerce_ProjectEntities();
            var adminInCookie = Request.Cookies["AdminInfo"];
            if (adminInCookie != null)
            {
                double? t = 0;
                List<Tbl_order> order = db.Tbl_order.ToList<Tbl_order>();
                foreach (var item in order)
                {
                    t += item.O_bill;
                }
                TempData["OrderTotal"] = t;
                return View(order);
            }
            else
            {
                var userInCookie = Request.Cookies["UserInfo"];
                if (userInCookie != null)
                {
                    return RedirectToAction("Index", "Home1");
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
        }
        public ActionResult Invoice()
        {
            Ecommerce_ProjectEntities db = new Ecommerce_ProjectEntities();
            var adminInCookie = Request.Cookies["AdminInfo"];
            if (adminInCookie != null)
            {
                double? t = 0;
                List<Tbl_invoice> invoice = db.Tbl_invoice.ToList<Tbl_invoice>();

                foreach (var item in invoice)
                {
                    t += item.In_totalbill;


                }
                TempData["InvoiceTotal"] = t;
                return View(invoice);
            }
            else
            {
                var userInCookie = Request.Cookies["UserInfo"];
                if (userInCookie != null)
                {
                    return RedirectToAction("Index", "Home1");
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }

            }
        }



        //public ActionResult Multi()
        //{
        //    var std = GetTbl_Invoices();
        //    var tchr = GetTbl_Orders();

        //    MultiModel data = new MultiModel();
        //    data.My_Tbl_invoice = std;
        //    data.My_Tbl_order = tchr;

        //    return View(data);
        //}

        //public Tbl_invoice GetTbl_Invoices()
        //{

        //    return DB.Tbl_invoice.ToList();
        //}
        //public Tbl_order GetTbl_Orders()
        //{

        //    //return DB.Tbl_order.ToList();
        //}



    }
}
