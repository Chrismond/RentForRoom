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
    public class ATinhThanhPhoesController : Controller
    {
        private QLNhaTroEntities db = new QLNhaTroEntities();

        // GET: Admin/ATinhThanhPhoes
        public ActionResult Index()
        {
            return View(db.tbTinhThanhPhoes.ToList());
        }

        // GET: Admin/ATinhThanhPhoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTinhThanhPho tbTinhThanhPho = db.tbTinhThanhPhoes.Find(id);
            if (tbTinhThanhPho == null)
            {
                return HttpNotFound();
            }
            return View(tbTinhThanhPho);
        }

        // GET: Admin/ATinhThanhPhoes/Create
        public ActionResult Create()
        {
            tbTinhThanhPho item = new tbTinhThanhPho();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbTinhThanhPho tbTinh)
        {
            tbTinhThanhPho item = new tbTinhThanhPho();
            try
            {
                item.TenTP = tbTinh.TenTP;
                item.Hide = false;
                db.tbTinhThanhPhoes.Add(item);
                db.SaveChanges();
                return Redirect("/Admin/ATinhThanhPhoes");
            }
            catch
            {
                return View(tbTinh);
            }
        }

        // GET: Admin/ATinhThanhPhoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTinhThanhPho tbTinhThanhPho = db.tbTinhThanhPhoes.Find(id);
            if (tbTinhThanhPho == null)
            {
                return HttpNotFound();
            }
            return View(tbTinhThanhPho);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbTinhThanhPho tbTinh)
        {
            try
            {
                tbTinhThanhPho item = new tbTinhThanhPho();

                item = db.tbTinhThanhPhoes.Find(tbTinh.IDTP);
                item.TenTP = tbTinh.TenTP;
                item.Hide = tbTinh.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/ATinhThanhPhoes");
            }
            catch (Exception ex)
            {
                return View(tbTinh);
            }
        }

        // GET: Admin/ATinhThanhPhoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTinhThanhPho tbTinhThanhPho = db.tbTinhThanhPhoes.Find(id);
            if (tbTinhThanhPho == null)
            {
                return HttpNotFound();
            }
            return View(tbTinhThanhPho);
        }

        // POST: Admin/ATinhThanhPhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTinhThanhPho tbTinhThanhPho = db.tbTinhThanhPhoes.Find(id);
            db.tbTinhThanhPhoes.Remove(tbTinhThanhPho);
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
