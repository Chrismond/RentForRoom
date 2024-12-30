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

namespace RentForRoom.Areas.Admin.Controllers
{
    public class AGioiThieuxController : Controller
    {
        private QLNhaTroEntities db = new QLNhaTroEntities();

        // GET: Admin/AGioiThieux
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/gioithieu/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
            return View(db.tbGioiThieux.ToList());
        }
        [HttpPost]
        public JsonResult UpdateStatus(int id, bool hide)
        {
            var user = db.tbGioiThieux.Find(id);
            if (user != null)
            {
                user.Hide = hide;
                db.SaveChanges();
            }
            return Json(new { success = true });
        }

        // GET: Admin/AGioiThieux/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbGioiThieu tbGioiThieu = db.tbGioiThieux.Find(id);
            if (tbGioiThieu == null)
            {
                return HttpNotFound();
            }
            return View(tbGioiThieu);
        }

        // GET: Admin/AGioiThieux/Create
        public ActionResult Create()
        {
            tbGioiThieu tb = new tbGioiThieu();
            return View(tb);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbGioiThieu tbGio, HttpPostedFileBase file)
        {
            tbGioiThieu item = new tbGioiThieu();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);
                }
                else
                {
                    item.HinhAnh = tbGio.HinhAnh;
                }
                item.TieuDe = tbGio.TieuDe;
                item.NoiDung = tbGio.NoiDung;
                item.TieuDeDanhSach = tbGio.TieuDeDanhSach;
                item.NoiDungDanhSach = tbGio.NoiDungDanhSach;
                item.Hide = false;
                db.tbGioiThieux.Add(item);
                db.SaveChanges();
                return Redirect("/Admin/AGioiThieux");
            }
            catch
            {
                return View(tbGio);
            }
        }

        // GET: Admin/AGioiThieux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbGioiThieu tbGioiThieu = db.tbGioiThieux.Find(id);
            if (tbGioiThieu == null)
            {
                return HttpNotFound();
            }
            return View(tbGioiThieu);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbGioiThieu tbGio, HttpPostedFileBase Editfile)
        {
            try
            {
                tbGioiThieu item = new tbGioiThieu();

                item = db.tbGioiThieux.Find(tbGio.IDGioiThieu);
                if (Editfile != null)
                {
                    item.HinhAnh = UploadImage(Editfile);
                }
                item.TieuDe = tbGio.TieuDe;
                item.NoiDung = tbGio.NoiDung;
                item.TieuDeDanhSach = tbGio.TieuDeDanhSach;
                item.NoiDungDanhSach = tbGio.NoiDungDanhSach;
                item.Hide = tbGio.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/AGioiThieux");
            }
            catch (Exception ex)
            {
                return View(tbGio);
            }
        }

        // GET: Admin/AGioiThieux/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbGioiThieu tbGioiThieu = db.tbGioiThieux.Find(id);
            if (tbGioiThieu == null)
            {
                return HttpNotFound();
            }
            return View(tbGioiThieu);
        }

        // POST: Admin/AGioiThieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbGioiThieu tbGioiThieu = db.tbGioiThieux.Find(id);
            db.tbGioiThieux.Remove(tbGioiThieu);
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
