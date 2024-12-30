using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using RentForRoom.DBContext;
using RentForRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentForRoom.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private QLNhaTroEntities db = new QLNhaTroEntities();

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

        public ActionResult Register()
        {
            var lstPhanQuyen = (from grpo in db.tbQuyens
                                where grpo.Hide != true && grpo.IDQuyen != 1
                                select new PhanQuyenModel
                                {
                                    Id = grpo.IDQuyen,
                                    Name = grpo.TenQuyen
                                }).ToList();

            // Tạo model cho view
            var model = new RegisterModel();

            // Gán giá trị mặc định cho Role nếu cần (có thể để trống cho phép người dùng chọn)
            model.Role =  0; // Gán giá trị mặc định nếu cần

            // Truyền danh sách quyền vào View
            ViewBag.Roles = lstPhanQuyen;

            return View(model);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel tbTTin)
        {
            try
            {
                Random random = new Random();
                int first4 = random.Next(1000, 9999);
                int last3 = random.Next(100, 999);

                var Register = new tbUser
                {
                    HoTen = tbTTin.HoTen,
                    SDT = tbTTin.SDT,
                    Gmail = tbTTin.Gmail,
                    MatKhau = tbTTin.MatKhau,
                    Role = tbTTin.Role,
                    Hide = true,
                };
                db.tbUsers.Add(Register);
                db.SaveChanges();
                string MaTaiKhoan = $"{first4}{Register.IDUser}{last3}";
                Register.MaTaiKhoan = int.Parse(MaTaiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch
            {
                return View(tbTTin);


            }

        }
        [HttpPost]
        public JsonResult GetJson(tbUser obj)
        {
            tbUser user = new tbUser();
            try
            {
                Random random = new Random();
                int first4 = random.Next(1000, 9999);
                int last3 = random.Next(100, 999);
                user = new tbUser
                {
                    HoTen = obj.HoTen,
                    Gmail = obj.Gmail,
                    MatKhau = obj.MatKhau,
                    SDT = obj.SDT,
                    Role = obj.Role,
                    Hide = obj.Hide
                };
                db.tbUsers.Add(user);
                db.SaveChanges();
                string MaTaiKhoan = $"{first4}{user.IDUser}{last3}";
                user.MaTaiKhoan = int.Parse(MaTaiKhoan);
                db.SaveChanges();

            }
            catch
            {
                user = new tbUser();
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CheckAccountExist1(string email)
        {
            bool exists = db.tbUsers.Any(u => u.Gmail == email);

            if (exists)
            {
                return Json(new { success = false, message = "Email này đã tồn tại" });
            }
            else
            {
                return Json(new { success = true, message = "Email hợp lệ" });
            }
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string Gmail, string MatKhau)
        {
            if (string.IsNullOrWhiteSpace(Gmail) || string.IsNullOrWhiteSpace(MatKhau))
            {
                if (string.IsNullOrWhiteSpace(Gmail))
                {
                    ViewBag.EmailError = "Vui lòng nhập email.";
                }

                if (string.IsNullOrWhiteSpace(MatKhau))
                {
                    ViewBag.PasswordError = "Vui lòng nhập mật khẩu.";
                }
                return View();
            }

            var user = db.tbUsers.FirstOrDefault(u => u.Gmail == Gmail && u.Hide == false);

            if (user == null)
            {
                ViewBag.EmailError = "Email không tồn tại.";
                return View();
            }

            if (user.MatKhau != MatKhau)
            {
                ViewBag.PasswordError = "Mật khẩu không đúng.";
                return View();
            }
            if (user.Role == 2 )
            {
                Session["HinhAnh"] = user.HinhAnh;
                Session["UserEmail"] = user.Gmail;
                Session["MaTaiKhoan"] = user.MaTaiKhoan;
                Session["HoTen"] = user.HoTen;
                return RedirectToAction("Index", "HomeHost", new { area = "Admin" });
            }
            if (user.Role == 1)
            {
                Session["HinhAnh"] = user.HinhAnh;
                Session["UserEmail"] = user.Gmail;
                Session["MaTaiKhoan"] = user.MaTaiKhoan;
                Session["HoTen"] = user.HoTen;
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            Session["HinhAnh"] = user.HinhAnh;
            Session["UserEmail"] = user.Gmail;
            Session["MaTaiKhoan"] = user.MaTaiKhoan;
            Session["HoTen"] = user.HoTen;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}