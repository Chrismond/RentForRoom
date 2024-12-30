using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentForRoom.DBContext;

namespace RentForRoom.Areas.Admin.Controllers
{
    public class AQuyensController : Controller
    {
        private QLNhaTroEntities db = new QLNhaTroEntities();

        // GET: Admin/AQuyens
        public ActionResult Index()
        {
            return View(db.tbQuyens.ToList());
        }

        // GET: Admin/AQuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbQuyen tbQuyen = db.tbQuyens.Find(id);
            if (tbQuyen == null)
            {
                return HttpNotFound();
            }
            return View(tbQuyen);
        }

        // GET: Admin/AQuyens/Create
        public ActionResult Create()
        {
            tbQuyen item = new tbQuyen();
            return View(item);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbQuyen tbQuy)
        {
            tbQuyen item = new tbQuyen();
            try
            {
                item.TenQuyen = tbQuy.TenQuyen;
                item.Hide = false;
                db.tbQuyens.Add(item);
                db.SaveChanges();
                return Redirect("/Admin/AQuyens");
            }
            catch
            {
                return View(tbQuy);
            }
        }

        // GET: Admin/AQuyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbQuyen tbQuyen = db.tbQuyens.Find(id);
            if (tbQuyen == null)
            {
                return HttpNotFound();
            }
            return View(tbQuyen);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbQuyen tbQuy)
        {
            try
            {
                tbQuyen item = new tbQuyen();

                item = db.tbQuyens.Find(tbQuy.IDQuyen);
                item.TenQuyen = tbQuy.TenQuyen;
                item.Hide = tbQuy.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/AQuyens");
            }
            catch (Exception ex)
            {
                return View(tbQuy);
            }
        }

        // GET: Admin/AQuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbQuyen tbQuyen = db.tbQuyens.Find(id);
            if (tbQuyen == null)
            {
                return HttpNotFound();
            }
            return View(tbQuyen);
        }

        // POST: Admin/AQuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbQuyen tbQuyen = db.tbQuyens.Find(id);
            db.tbQuyens.Remove(tbQuyen);
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
