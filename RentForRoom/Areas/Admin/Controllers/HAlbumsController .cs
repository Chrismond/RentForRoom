using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using RentForRoom.App_Start;
using RentForRoom.DBContext;
using RentForRoom.Models;

namespace RentForRoom.Areas.Admin.Controllers
{
    public class HAlbumsController : Controller
    {
        private QLNhaTroEntities db = new QLNhaTroEntities();

        // GET: Admin/HAlbums
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/album/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
        public string GetIDPhong(int? id)
        {
            string html = "";

            List<IDPhongModel> lst = new List<IDPhongModel>();
            lst = (from grpo in db.tbChiTietPhongs
                   where grpo.Hide != true
                   select (new IDPhongModel
                   {
                       Id = grpo.IDPhong,
                       Name = grpo.IDPhong,
                       MaTaiKhoan = grpo.MaTaiKhoan,
                   })).ToList();
            int tong = lst.Count;

            if (id != null)
            {
                html = "<option value= ''> ----- Chọn MTK - ID Phòng -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].Id)
                    {
                        html += "<option selected value='" + lst[i].Id + "'>" + "MTK: "+ lst[i].MaTaiKhoan + " - ID Phòng: " + lst[i].Name + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";

                    }
                }
            }
            else
            {
                html = "<option selected value= ''> ----- Chọn MTK - ID Phòng -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value='" + lst[i].Id + "'>"+"MTK: " + lst[i].MaTaiKhoan + " - ID Phòng: " + lst[i].Name + "</option>";
                }
            }
            return html;

        }
        public String GetIDPhongName(int id)

        {
            try
            {
                return db.tbChiTietPhongs.Find(id).TieuDe;
            }
            catch
            {
                return "";
            }
        }
        public ActionResult Index()
        {
            return View(db.tbAlbums.ToList());
        }
        [HttpPost]
        public JsonResult UpdateStatus(int id, bool hide)
        {
            var user = db.tbAlbums.Find(id);
            if (user != null)
            {
                user.Hide = hide;
                db.SaveChanges();
            }
            return Json(new { success = true });
        }

        // GET: Admin/HAlbums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAlbum tbAlbum = db.tbAlbums.Find(id);
            if (tbAlbum == null)
            {
                return HttpNotFound();
            }
            return View(tbAlbum);
        }

        // GET: Admin/HAlbums/Create
        public ActionResult Create()
        {
            tbAlbum item = new tbAlbum();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbAlbum tbAlb, HttpPostedFileBase file)
        {
            tbAlbum item = new tbAlbum();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);
                }
                else
                {
                    item.HinhAnh = tbAlb.HinhAnh;
                }
                item.TieuDe = tbAlb.TieuDe;
                item.IDPhong = tbAlb.IDPhong;
                item.Hide = false;
                db.tbAlbums.Add(item);
                db.SaveChanges();
                return Redirect("/Admin/HAlbums");
            }
            catch
            {
                return View(tbAlb);
            }
        }

        // GET: Admin/HAlbums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAlbum tbAlbum = db.tbAlbums.Find(id);
            if (tbAlbum == null)
            {
                return HttpNotFound();
            }
            return View(tbAlbum);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbAlbum tbAlb, HttpPostedFileBase Editfile)
        {
            try
            {
                tbAlbum item = new tbAlbum();

                item = db.tbAlbums.Find(tbAlb.IDAlbum);
                if (Editfile != null)
                {
                    item.HinhAnh = UploadImage(Editfile);
                }
                item.TieuDe = tbAlb.TieuDe;
                item.IDPhong = tbAlb.IDPhong;
                item.Hide = tbAlb.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/HAlbums");
            }
            catch (Exception ex)
            {
                return View(tbAlb);
            }
        }

        // GET: Admin/HAlbums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAlbum tbAlbum = db.tbAlbums.Find(id);
            if (tbAlbum == null)
            {
                return HttpNotFound();
            }
            return View(tbAlbum);
        }

        // POST: Admin/HAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAlbum tbAlbum = db.tbAlbums.Find(id);
            db.tbAlbums.Remove(tbAlbum);
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
