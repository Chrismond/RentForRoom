using RentForRoom.App_Start;
using RentForRoom.DBContext;
using RentForRoom.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentForRoom.Areas.Admin.Controllers
{
    public class HTaiKhoanController : Controller
    {
        // GET: Admin/HTaiKhoan
        QLNhaTroEntities db = new QLNhaTroEntities();
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/host/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
        public ActionResult Index()
        {
            string Gmail = Session["UserEmail"] as string;
            if (string.IsNullOrEmpty(Gmail))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var user = db.tbUsers
                             .Where(u => u.Gmail == Gmail)
                             .Select(ab => new ThongTinCaNhanModels
                             {
                                 MaTaiKhoan = ab.MaTaiKhoan,
                                 HinhAnh = ab.HinhAnh,
                                 HoTen = ab.HoTen,
                                 SDT = ab.SDT,
                                 Role = ab.Role
                             })
                             .FirstOrDefault();
            if (user == null)
            {
                return Redirect("/not-found");
            }
            if (string.IsNullOrEmpty(user.HoTen))
            {
                user.HoTen = "(Chưa có thông tin)";
            }
            if (string.IsNullOrEmpty(user.SDT))
            {
                user.SDT = "(Chưa có thông tin)";
            }
            if (string.IsNullOrEmpty(user.HinhAnh))
            {
                user.HinhAnh = "/Content/img/avatarfb.jpg";
            }
            Session["MaTaiKhoan"] = user.MaTaiKhoan;
            Session["HinhAnh"] = user.HinhAnh;
            Session["HoTen"] = user.HoTen;
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(ThongTinCaNhanModels model, HttpPostedFileBase Editfile)
        {
            if (ModelState.IsValid)
            {
                string Gmail = Session["UserEmail"] as string;
                if (string.IsNullOrEmpty(Gmail))
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                var user = db.tbUsers.FirstOrDefault(u => u.Gmail == Gmail);
                if (user != null)
                {
                    if (Editfile != null)
                    {
                        user.HinhAnh = UploadImage(Editfile);
                    }
                    user.HoTen = model.HoTen;
                    user.SDT = model.SDT;
                    db.Entry(user).State = EntityState.Modified;

                    db.SaveChanges();

                    Session["UserEmail"] = model.Gmail;

                    return RedirectToAction("Index", "HTaiKhoan", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy thông tin người dùng.");
                }
            }

            return View(model);
        }

        public ActionResult UpdateUserProfile()
        {
            string Gmail = Session["UserEmail"] as string;
            if (string.IsNullOrEmpty(Gmail))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var user = db.tbUsers
                             .Where(u => u.Gmail == Gmail)
                             .Select(ab => new ThongTinCaNhanModels
                             {
                                 MaTaiKhoan = ab.MaTaiKhoan,
                                 Gmail = ab.Gmail,
                                 MatKhau = ab.MatKhau,
                             })
                             .FirstOrDefault();
            if (user == null)
            {
                return Redirect("/not-found"); // Trường hợp không tìm thấy người dùng
            }
            Session["MaTaiKhoan"] = user.MaTaiKhoan;
            Session["MatKhau"] = user.MatKhau;
            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateUserProfile(ThongTinCaNhanModels model, string OldMatKhau, string NewMatKhau, string ConfirmNewMatKhau)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại!";
                    TempData["IsSuccess"] = false;
                    return View(model);
                }

                string Gmail = Session["UserEmail"] as string;
                if (string.IsNullOrEmpty(Gmail))
                {
                    TempData["Message"] = "Bạn chưa đăng nhập, vui lòng đăng nhập để tiếp tục.";
                    TempData["IsSuccess"] = false;
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                var user = db.tbUsers.FirstOrDefault(u => u.Gmail == Gmail);
                if (user == null)
                {
                    TempData["Message"] = "Không tìm thấy thông tin tài khoản.";
                    TempData["IsSuccess"] = false;
                    return View(model);
                }
                if (!string.IsNullOrEmpty(OldMatKhau) && user.MatKhau != OldMatKhau)
                {
                    TempData["Message"] = "Mật khẩu cũ không chính xác.";
                    TempData["IsSuccess"] = false;
                    return View(model);
                }
                if (!string.IsNullOrEmpty(NewMatKhau) && NewMatKhau != ConfirmNewMatKhau)
                {
                    TempData["Message"] = "Mật khẩu mới và mật khẩu xác nhận " + Environment.NewLine + "không khớp.";
                    TempData["IsSuccess"] = false;
                    return View(model);
                }
                user.Gmail = model.Gmail;
                if (!string.IsNullOrEmpty(NewMatKhau))
                {
                    user.MatKhau = NewMatKhau;
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Session["UserEmail"] = model.Gmail;

                TempData["Message"] = "Cập nhật thông tin thành công!";
                TempData["IsSuccess"] = true;

                return RedirectToAction("UpdateUserProfile", "HTaiKhoan", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Đã xảy ra lỗi: {ex.Message}";
                TempData["IsSuccess"] = false;
                return View(model);
            }
        }

    }
}