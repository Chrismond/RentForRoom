using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using RentForRoom.App_Start;
using RentForRoom.DBContext;
using RentForRoom.Models;
using Microsoft.Win32;

namespace RentForRoom.Areas.Admin.Controllers
{
    public class AThongTinCaNhansController : Controller
    {
        private QLNhaTroEntities db = new QLNhaTroEntities();

        // GET: Admin/AThongTinCaNhans
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/Infor/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

        public string GetPhanQuyen(int? id)
        {
            string html = "";

            List<PhanQuyenModel> lst = new List<PhanQuyenModel>();
            lst = (from grpo in db.tbQuyens
                   where grpo.Hide != true
                   select (new PhanQuyenModel
                   {
                       Id = grpo.IDQuyen,
                       Name = grpo.TenQuyen
                   })).ToList();
            int tong = lst.Count;

            if (id != null)
            {
                html = "<option value= ''> ----- Chọn Quyền -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].Id)
                    {
                        html += "<option selected value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";

                    }
                }
            }
            else
            {
                html = "<option selected value= ''> ----- Chọn Quyền -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                }
            }
            return html;

        }
        public String GetPhanQuyenName(int id)

        {
            try
            {
                return db.tbQuyens.Find(id).TenQuyen;
            }
            catch
            {
                return "";
            }
        }


        public string GetThanhPho(int? id)
        {
            string html = "";

            List<ThanhPhoModel> lst = new List<ThanhPhoModel>();
            lst = (from grpo in db.tbTinhThanhPhoes
                   where grpo.Hide != true
                   select (new ThanhPhoModel
                   {
                       Id = grpo.IDTP,
                       Name = grpo.TenTP
                   })).ToList();
            int tong = lst.Count;

            if (id != null)
            {
                html = "<option value= ''> ----- Chọn Thành Phố -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].Id)
                    {
                        html += "<option selected value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";

                    }
                }
            }
            else
            {
                html = "<option selected value= ''> ----- Chọn Thành Phố -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                }
            }
            return html;

        }
        public String GetThanhPhoName(int id)

        {
            try
            {
                return db.tbTinhThanhPhoes.Find(id).TenTP;
            }
            catch
            {
                return "";
            }
        }


        public string GetDanhSachQuan(int? idThanhPho, int? idQuan)
        {
            string html = "<option value=''>----- Chọn Quận/Huyện -----</option>";

            if (idThanhPho == null)
            {
                return html;
            }

            List<QuanHuyenModel> lstQuan = (from qh in db.tbQuanHuyens
                                            where qh.Hide != true && qh.IDTP == idThanhPho
                                            select new QuanHuyenModel
                                            {
                                                IDQuan = qh.IDQuan,
                                                TenQuan = qh.TenQuan
                                            }).ToList();

            foreach (var qh in lstQuan)
            {
                if (idQuan == qh.IDQuan)
                {
                    html += $"<option selected value='{qh.IDQuan}'>{qh.TenQuan}</option>";
                }
                else
                {
                    html += $"<option value='{qh.IDQuan}'>{qh.TenQuan}</option>";
                }
            }

            return html;
        }
        public String GetQuanHuyenName(int id)

        {
            try
            {
                return db.tbQuanHuyens.Find(id).TenQuan;
            }
            catch
            {
                return "";
            }
        }


        public string GetDanhSachPhuong(int? idPhuong, int? idQuan)
        {
            string html = "<option value=''>----- Chọn Phường/Xã -----</option>";

            if (idQuan == null)
            {
                return html;
            }

            List<PhuongXaModel> lstPhuong = (from qh in db.tbXaPhuongs
                                             where qh.Hide != true && qh.IDQuan == idQuan
                                             select new PhuongXaModel
                                             {
                                                 IDPhuong = qh.IDPhuong,
                                                 TenPhuong = qh.TenPhuong
                                             }).ToList();

            foreach (var qh in lstPhuong)
            {
                if (idPhuong == qh.IDPhuong)
                {
                    html += $"<option selected value='{qh.IDPhuong}'>{qh.TenPhuong}</option>";
                }
                else
                {
                    html += $"<option value='{qh.IDPhuong}'>{qh.TenPhuong}</option>";
                }
            }

            return html;
        }
        public String GetPhuongXaName(int id)

        {
            try
            {
                return db.tbXaPhuongs.Find(id).TenPhuong;
            }
            catch
            {
                return "";
            }
        }
        
        
        public ActionResult Index()
        {
            return View(db.tbUsers.ToList());
        }
        [HttpPost]
        public JsonResult UpdateStatus(int id, bool hide)
        {
            var user = db.tbUsers.Find(id);
            if (user != null)
            {
                user.Hide = hide;
                db.SaveChanges();
            }
            return Json(new { success = true });
        }

        // GET: Admin/AThongTinCaNhans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUser tbUser = db.tbUsers.Find(id);
            if (tbUser == null)
            {
                return HttpNotFound();
            }
            return View(tbUser);
        }

        // GET: Admin/AThongTinCaNhans/Create
        public ActionResult Create()
        {
            tbUser item = new tbUser();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbUser tbTho, HttpPostedFileBase file)
        {
            tbUser item = new tbUser();
            try
            {
                if (file != null)
                {
                    item.HinhAnh = UploadImage(file);
                }
                else
                {
                    item.HinhAnh = tbTho.HinhAnh;
                }
                Random random = new Random();
                int first4 = random.Next(1000, 9999);
                int last3 = random.Next(100, 999);
                item.Role = tbTho.Role;
                item.HoTen = tbTho.HoTen;
                item.SDT = tbTho.SDT;
                item.Gmail = tbTho.Gmail;
                item.MatKhau = tbTho.MatKhau;
                item.Hide = false;
                db.tbUsers.Add(item);
                db.SaveChanges();
                string MaTaiKhoan = $"{first4}{item.IDUser}{last3}";
                item.MaTaiKhoan = int.Parse(MaTaiKhoan);
                db.SaveChanges();
                return Redirect("/Admin/AThongTinCaNhans");
            }
            catch
            {
                return View(tbTho);
            }
        }

        // GET: Admin/AThongTinCaNhans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUser tbUser = db.tbUsers.Find(id);
            if (tbUser == null)
            {
                return HttpNotFound();
            }
            return View(tbUser);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbUser tbTho, HttpPostedFileBase Editfile)
        {
            try
            {
                tbUser item = new tbUser();

                item = db.tbUsers.Find(tbTho.IDUser);
                if (Editfile != null)
                {
                    item.HinhAnh = UploadImage(Editfile);
                }
                item.HoTen = tbTho.HoTen;
                item.SDT = tbTho.SDT;
                item.Gmail = tbTho.Gmail;
                item.MatKhau = tbTho.MatKhau;
                item.Role = tbTho.Role;
                item.Hide = tbTho.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/AThongTinCaNhans");
            }
            catch (Exception ex)
            {
                return View(tbTho);
            }
        }

        // GET: Admin/AThongTinCaNhans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUser tbUser = db.tbUsers.Find(id);
            if (tbUser == null)
            {
                return HttpNotFound();
            }
            return View(tbUser);
        }

        // POST: Admin/AThongTinCaNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUser tbUser = db.tbUsers.Find(id);
            db.tbUsers.Remove(tbUser);
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
