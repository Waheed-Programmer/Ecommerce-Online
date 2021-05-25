using Ecommerce_ProjectMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecommerce_ProjectMvc.Controllers
{
   
    public class Home1Controller : Controller
    {
       
        Ecommerce_ProjectEntities DB = new Ecommerce_ProjectEntities();
        // GET: Home1
        public ActionResult Index()
        {

            Tbl_user DBS = new Tbl_user();
            Session["U_id"] = DBS.U_id.ToString();
            if (TempData["Cart"] != null)
            {
                float x = 0;
                List<Cart> li2 = TempData["Cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                TempData["Total"] = x;
            }
            TempData.Keep();
            //var userInCookie = Request.Cookies["UserInfo"];
            //if (userInCookie != null)
            //{
                return View(DB.Tbl_product.OrderByDescending(x => x.Pro_id).ToList());
            //}
            //else
            //{
          
            //    {
            //        return RedirectToAction("Login", "Admin");
            //    }
            //}
            
        }

        public ActionResult Addtocart(int? id)
        {
            
            var userInCookie = Request.Cookies["UserInfo"];
            if (userInCookie != null)
            {
                Tbl_product p = DB.Tbl_product.Where(x => x.Pro_id == id).SingleOrDefault();
                return View(p);
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
                    return RedirectToAction("Login", "Register");
                }
            }
           
        }

        List<Cart> li = new List<Cart>();
        [HttpPost]
        public ActionResult Addtocart(Tbl_product pi, string qty, int Id)
        {
            Tbl_product p = DB.Tbl_product.Where(x => x.Pro_id == Id).SingleOrDefault();

            Cart c = new Cart();

            c.productid = p.Pro_id;
            c.productname = p.Pro_name;
            c.price = p.Pro_price;
            c.qty = Convert.ToInt32(qty);
            c.bill = c.price * c.qty;
            

            if (TempData["Cart"] == null)
            {
                li.Add(c);
                TempData["Cart"] = li;
            }
            else
            {
               


               
                List<Cart> li2 = TempData["Cart"] as List<Cart>;
                int flag =0;
                foreach (var item in li2)
                {
                    if (item.productid == c.productid)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;
                    }

                }
                if (flag == 0)
                {
                    li2.Add(c);
                }
                TempData["Cart"] = li2;
            }



            TempData.Keep();
            return RedirectToAction("Index");


        }
        public ActionResult Remove(int? id)
        {
            List<Cart> li2 = TempData["Cart"] as List<Cart>;
            Cart c =li2.Where(x=> x.productid==id).SingleOrDefault();
            li2.Remove(c);
            float h = 0;
            foreach (var item in li2)
            {
                h += item.bill;
            }
            TempData["Total"] = h;
            return RedirectToAction("checkout");
        }


        public ActionResult checkout()
        {
            TempData["Msg"] = "Transaction is Complete";
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult checkout(Tbl_order o)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int User_id;
            User_id = Convert.ToInt32(userInCookie["idUser"]);
            List<Cart> li = TempData["Cart"] as List<Cart>;
            Tbl_invoice iv = new Tbl_invoice();
            iv.In_fk_user = User_id;
            iv.In_date = System.DateTime.Now;
            iv.In_totalbill = (float)TempData["Total"];
            DB.Tbl_invoice.Add(iv);
            DB.SaveChanges();
            foreach (var item in li)
            {
                Tbl_order od = new Tbl_order();
                od.O_fk_pro = item.productid;
                od.O_fk_invoice = iv.Invoice_id;
                od.O_date = System.DateTime.Now;
                od.O_qty = item.qty;
                od.O_unitprice = (int)item.price;
                od.O_bill = item.bill;
                DB.Tbl_order.Add(od);
                DB.SaveChanges();

            }
            TempData.Remove("Total");
            TempData.Remove("Cart");
            TempData["Msg"] = "Transaction is Complete";
            TempData.Keep();
            return View();
           
        }


        [HttpGet] //contactform
        public ActionResult Contact()
        {
            return View();
        
        }
        [HttpPost] //contactform
        public ActionResult Contact(ContactUS_Tbl c)
        {
            using (var context = new Ecommerce_ProjectEntities())
            {
                context.ContactUS_Tbl.Add(c);
                context.SaveChanges();
            }
            return RedirectToAction("Contact");
        }

        public ActionResult Blog()
        {
            return View();

        }


        public ActionResult Faq()
        {
            return View();

        }

        public ActionResult Privacy()
        {
            return View();

        }
        public ActionResult Terms()
        {
            return View();

        }

        public ActionResult Disclamer()
        {
            return View();

        }




    }
}