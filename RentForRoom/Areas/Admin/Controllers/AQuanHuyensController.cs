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
    public class AQuanHuyensController : Controller
    {
        private QLNhaTroEntities db = new QLNhaTroEntities();

        // GET: Admin/AQuanHuyens
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
                html = "<option value= ''> ----- Chọn -----</option>";
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
                html = "<option selected value= ''> ----- Chọn -----</option>";
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



        public ActionResult Index()
        {
            return View(db.tbQuanHuyens.ToList());
        }

        // GET: Admin/AQuanHuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbQuanHuyen tbQuanHuyen = db.tbQuanHuyens.Find(id);
            if (tbQuanHuyen == null)
            {
                return HttpNotFound();
            }
            return View(tbQuanHuyen);
        }

        // GET: Admin/AQuanHuyens/Create
        public ActionResult Create()
        {
            tbQuanHuyen item = new tbQuanHuyen();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbQuanHuyen tbQuan)
        {
            tbQuanHuyen item = new tbQuanHuyen();
            try
            {
                item.TenQuan = tbQuan.TenQuan;
                item.IDTP = tbQuan.IDTP;
                item.Hide = false;
                db.tbQuanHuyens.Add(item);
                db.SaveChanges();
                return Redirect("/Admin/AQuanHuyens");
            }
            catch
            {
                return View(tbQuan);
            }
        }

        // GET: Admin/AQuanHuyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbQuanHuyen tbQuanHuyen = db.tbQuanHuyens.Find(id);
            if (tbQuanHuyen == null)
            {
                return HttpNotFound();
            }
            return View(tbQuanHuyen);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbQuanHuyen tbQuan)
        {
            try
            {
                tbQuanHuyen item = new tbQuanHuyen();

                item = db.tbQuanHuyens.Find(tbQuan.IDQuan);
                item.IDTP = tbQuan.IDTP;
                item.TenQuan = tbQuan.TenQuan;
                item.Hide = tbQuan.Hide;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/AQuanHuyens");
            }
            catch (Exception ex)
            {
                return View(tbQuan);
            }
        }

        // GET: Admin/AQuanHuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbQuanHuyen tbQuanHuyen = db.tbQuanHuyens.Find(id);
            if (tbQuanHuyen == null)
            {
                return HttpNotFound();
            }
            return View(tbQuanHuyen);
        }

        // POST: Admin/AQuanHuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbQuanHuyen tbQuanHuyen = db.tbQuanHuyens.Find(id);
            db.tbQuanHuyens.Remove(tbQuanHuyen);
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
