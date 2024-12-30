using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentForRoom.DBContext;
using RentForRoom.Models;

namespace RentForRoom.Areas.Admin.Controllers
{
    public class AXaPhuongsController : Controller
    {
        private QLNhaTroEntities db = new QLNhaTroEntities();

        // GET: Admin/AXaPhuongs

        public string GetQuanHuyen(int? id)
        {
            string html = "";

            List<QuanHuyenModel> lst = new List<QuanHuyenModel>();
            lst = (from grpo in db.tbQuanHuyens
                   where grpo.Hide != true
                   select (new QuanHuyenModel
                   {
                       IDQuan = grpo.IDQuan,
                       TenQuan = grpo.TenQuan
                   })).ToList();
            int tong = lst.Count;

            if (id != null)
            {
                html = "<option value= ''> ----- Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].IDQuan)
                    {
                        html += "<option selected value='" + lst[i].IDQuan + "'>" + lst[i].TenQuan + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].IDQuan + "'>" + lst[i].TenQuan + "</option>";

                    }
                }
            }
            else
            {
                html = "<option selected value= ''> ----- Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value='" + lst[i].IDQuan + "'>" + lst[i].TenQuan + "</option>";
                }
            }
            return html;

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
            return View(db.tbXaPhuongs.ToList());
        }

        // GET: Admin/AXaPhuongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbXaPhuong tbXaPhuong = db.tbXaPhuongs.Find(id);
            if (tbXaPhuong == null)
            {
                return HttpNotFound();
            }
            return View(tbXaPhuong);
        }

        // GET: Admin/AXaPhuongs/Create
        public ActionResult Create()
        {
            tbXaPhuong item = new tbXaPhuong();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbXaPhuong tbXa)
        {
            tbXaPhuong item = new tbXaPhuong();
            try
            {
                item.TenPhuong = tbXa.TenPhuong;
                item.IDQuan = tbXa.IDQuan;
                item.Hide = false;
                db.tbXaPhuongs.Add(item);
                db.SaveChanges();
                return Redirect("/Admin/AXaPhuongs");
            }
            catch
            {
                return View(tbXa);
            }
        }

        // GET: Admin/AXaPhuongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbXaPhuong tbXaPhuong = db.tbXaPhuongs.Find(id);
            if (tbXaPhuong == null)
            {
                return HttpNotFound();
            }
            return View(tbXaPhuong);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbXaPhuong tbXa)
        {
            try
            {
                tbXaPhuong item = new tbXaPhuong();

                item = db.tbXaPhuongs.Find(tbXa.IDPhuong);
                item.IDQuan = tbXa.IDQuan;
                item.TenPhuong = tbXa.TenPhuong;
                item.Hide = tbXa.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/AXaPhuongs");
            }
            catch (Exception ex)
            {
                return View(tbXa);
            }
        }

        // GET: Admin/AXaPhuongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbXaPhuong tbXaPhuong = db.tbXaPhuongs.Find(id);
            if (tbXaPhuong == null)
            {
                return HttpNotFound();
            }
            return View(tbXaPhuong);
        }

        // POST: Admin/AXaPhuongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbXaPhuong tbXaPhuong = db.tbXaPhuongs.Find(id);
            db.tbXaPhuongs.Remove(tbXaPhuong);
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
