using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentForRoom.App_Start;
using RentForRoom.DBContext;
using RentForRoom.Models;

namespace RentForRoom.Areas.Admin.Controllers
{
    public class ABannersController : Controller
    {
        private QLNhaTroEntities db = new QLNhaTroEntities();

        // GET: Admin/ABanners
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/banner/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
        private string fileName = "";
        public string UploadImage(HttpPostedFileBase upload)
        {
            fileName = Path.GetFileName(upload.FileName);
            fileName = Processing.UrlImages(fileName);
            bool exsits = System.IO.Directory.Exists(Server.MapPath(pathFile));
            if (!exsits)
                System.IO.Directory.CreateDirectory(Server.MapPath(pathFile));
            var path = Path.Combine(Server.MapPath(pathFile), fileName);
            upload.SaveAs(path);
            return pathFile + fileName;
        }
        #endregion

        #endregion
        
        public ActionResult Index()
        {
            return View(db.tbBanners.ToList());
        }
        [HttpPost]
        public JsonResult UpdateStatus(int id, bool hide)
        {
            var user = db.tbBanners.Find(id);
            if (user != null)
            {
                user.Hide = hide;
                db.SaveChanges();
            }
            return Json(new { success = true });
        }

        // GET: Admin/ABanners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanner tbBanner = db.tbBanners.Find(id);
            if (tbBanner == null)
            {
                return HttpNotFound();
            }
            return View(tbBanner);
        }

        // GET: Admin/ABanners/Create
        public ActionResult Create()
        {
            tbBanner item = new tbBanner();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbBanner tbBan, HttpPostedFileBase file)
        {
            tbBanner item = new tbBanner();
            try
            {
                if (file != null)
                {
                    item.HinhBanner = UploadImage(file);
                }
                else
                {
                    item.HinhBanner = tbBan.HinhBanner;
                }
                item.TieuDe = tbBan.TieuDe;
                item.NoiDung = tbBan.NoiDung;
                item.Hide = false;
                db.tbBanners.Add(item);
                db.SaveChanges();
                return Redirect("/Admin/ABanners");
            }
            catch
            {
                return View(tbBan);
            }
        }

        // GET: Admin/ABanners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanner tbBanner = db.tbBanners.Find(id);
            if (tbBanner == null)
            {
                return HttpNotFound();
            }
            return View(tbBanner);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbBanner tbBan, HttpPostedFileBase Editfile)
        {
            try
            {
                tbBanner item = new tbBanner();

                item = db.tbBanners.Find(tbBan.IDBanner);
                if (Editfile != null)
                {
                    item.HinhBanner = UploadImage(Editfile);
                }
                item.TieuDe = tbBan.TieuDe;
                item.NoiDung = tbBan.NoiDung;
                item.Hide = tbBan.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/ABanners");
            }
            catch (Exception ex)
            {
                return View(tbBan);
            }
        }

        // GET: Admin/ABanners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanner tbBanner = db.tbBanners.Find(id);
            if (tbBanner == null)
            {
                return HttpNotFound();
            }
            return View(tbBanner);
        }

        // POST: Admin/ABanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBanner tbBanner = db.tbBanners.Find(id);
            db.tbBanners.Remove(tbBanner);
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
