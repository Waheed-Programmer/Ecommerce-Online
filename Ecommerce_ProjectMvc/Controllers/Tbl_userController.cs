using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce_ProjectMvc.Models;

namespace Ecommerce_ProjectMvc.Controllers
{
    public class Tbl_userController : Controller
    {
        private Ecommerce_ProjectEntities db = new Ecommerce_ProjectEntities();

        // GET: Tbl_user
        public ActionResult Index()
        {
            return View(db.Tbl_user.ToList());
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_user tbl_user = db.Tbl_user.Find(id);
            if (tbl_user == null)
            {
                return HttpNotFound();
            }
            return View(tbl_user);
        }

        // POST: Tbl_user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_user tbl_user = db.Tbl_user.Find(id);
            db.Tbl_user.Remove(tbl_user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
