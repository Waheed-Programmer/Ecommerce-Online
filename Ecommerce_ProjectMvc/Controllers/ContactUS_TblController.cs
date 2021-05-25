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
    public class ContactUS_TblController : Controller
    {
        private Ecommerce_ProjectEntities db = new Ecommerce_ProjectEntities();

        // GET: ContactUS_Tbl
        public ActionResult Index()
        {
            return View(db.ContactUS_Tbl.ToList());
        }

    

        // GET: ContactUS_Tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUS_Tbl contactUS_Tbl = db.ContactUS_Tbl.Find(id);
            if (contactUS_Tbl == null)
            {
                return HttpNotFound();
            }
            return View(contactUS_Tbl);
        }

        // POST: ContactUS_Tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactUS_Tbl contactUS_Tbl = db.ContactUS_Tbl.Find(id);
            db.ContactUS_Tbl.Remove(contactUS_Tbl);
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
