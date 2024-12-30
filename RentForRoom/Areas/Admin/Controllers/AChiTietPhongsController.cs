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
    public class AChiTietPhongsController : Controller
    {
        private QLNhaTroEntities db = new QLNhaTroEntities();

        // GET: Admin/AChiTietPhongs
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/album/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
        public List<string> UploadImages(IEnumerable<HttpPostedFileBase> uploads)
        {
            List<string> filePaths = new List<string>();

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            bool exists = System.IO.Directory.Exists(Server.MapPath(pathFile));
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(pathFile));

            // Duyệt qua danh sách file được upload
            foreach (var file in uploads)
            {
                if (file != null && file.ContentLength > 0)
                {
                    // Xử lý tên file và lưu vào thư mục
                    string fileName = Path.GetFileName(file.FileName);
                    fileName = Processing.UrlImages(fileName); // Tùy chọn xử lý tên file
                    string fullPath = Path.Combine(Server.MapPath(pathFile), fileName);
                    file.SaveAs(fullPath);

                    // Lưu đường dẫn file vào danh sách
                    filePaths.Add(pathFile + fileName);
                }
            }

            return filePaths; // Trả về danh sách đường dẫn file
        }
        #endregion
        #endregion

        public string GetMaTaiKhoan(int? id)
        {
            string html = "";

            List<MaTaiKhoanModel> lst = new List<MaTaiKhoanModel>();
            lst = (from grpo in db.tbUsers
                   where grpo.Hide != true && grpo.Role == 2
                   select (new MaTaiKhoanModel
                   {
                       Id = grpo.MaTaiKhoan,
                       MaTaiKhoan = grpo.MaTaiKhoan,
                       Name = grpo.HoTen
                   })).ToList();
            int tong = lst.Count;

            if (id != null)
            {
                html = "<option value= ''> ----- Chọn Mã Tài Khoản -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].Id)
                    {
                        html += "<option selected value='" + lst[i].Id + "'>" + lst[i].MaTaiKhoan + " - " + lst[i].Name + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].Id + "'>" + lst[i].MaTaiKhoan + " - " + lst[i].Name + "</option>";

                    }
                }
            }
            else
            {
                html = "<option selected value= ''> ----- Chọn Mã Tài Khoản -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value='" + lst[i].Id + "'>" + lst[i].MaTaiKhoan + " - " + lst[i].Name + "</option>";
                }
            }
            return html;

        }
        public string GetMaTaiKhoanName(int? id)

        {
            try
            {
                var user = db.tbUsers.FirstOrDefault(u => u.MaTaiKhoan == id);
                return user?.HoTen ?? "";
                //return db.tbUsers.Find(mataikhoan).HoTen;
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
                html = "<option value= ''>--- Chọn Thành Phố ---</option>";
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
                html = "<option selected value= ''>--- Chọn Thành Phố ---</option>";
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
            string html = "<option value=''>--- Chọn Quận/Huyện ---</option>";

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
            string html = "<option value=''>--- Chọn Phường/Xã ---</option>";

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
            return View(db.tbChiTietPhongs.ToList());
        }
        [HttpPost]
        public JsonResult UpdateStatus(int id, bool trangthaixuly)
        {
            var user = db.tbChiTietPhongs.Find(id);
            if (user != null)
            {
                user.TrangThaiXuLy = trangthaixuly;
                db.SaveChanges();
            }
            return Json(new { success = false });
        }

        // GET: Admin/AChiTietPhongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbChiTietPhong tbChiTietPhong = db.tbChiTietPhongs.Find(id);
            if (tbChiTietPhong == null)
            {
                return HttpNotFound();
            }
            // Lấy danh sách hình ảnh liên kết
            var images = db.tbAlbums.Where(a => a.IDPhong == id && a.Hide == false).ToList();
            ViewBag.RoomImages = images;

            return View(tbChiTietPhong);
        }

        // GET: Admin/AChiTietPhongs/Create
        public ActionResult Create()
        {
            tbChiTietPhong item = new tbChiTietPhong();
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbChiTietPhong tbChi, IEnumerable<HttpPostedFileBase> RoomImages)
        {
            tbChiTietPhong item = new tbChiTietPhong();
            try 
            { 
                item.MaTaiKhoan = tbChi.MaTaiKhoan;
                item.TieuDe = tbChi.TieuDe;
                item.DiaChi = tbChi.DiaChi;
                item.IDPhuong = tbChi.IDPhuong;
                item.IDQuan = tbChi.IDQuan;
                item.IDTP = tbChi.IDTP;
                item.GiaThue = tbChi.GiaThue*1000;
                item.MoTa = tbChi.MoTa;
                item.LinkBai = tbChi.LinkBai;
                item.GioGiac = tbChi.GioGiac;
                item.LinkMap = tbChi.LinkMap;
                item.TienDien = tbChi.TienDien;
                item.TienNuoc = tbChi.TienNuoc;
                item.TienDichVụ = tbChi.TienDichVụ;
                item.CuaSoBanCong = tbChi.CuaSoBanCong;
                item.WC = tbChi.WC;
                item.KeBep = tbChi.KeBep;
                item.MayGiat = tbChi.MayGiat;
                item.MayLanh = tbChi.MayLanh;
                item.Giuong = tbChi.Giuong;
                item.TuDo = tbChi.TuDo;
                item.Nem = tbChi.Nem;
                item.TuLanh = tbChi.TuLanh;
                item.BanGhe = tbChi.BanGhe;
                item.Sofa = tbChi.Sofa;
                item.SoPhongNgu = tbChi.SoPhongNgu;
                item.PhuongTien = tbChi.PhuongTien;
                item.ThuCung = tbChi.ThuCung;
                item.TienCoc = tbChi.TienCoc;
                item.Thang = tbChi.Thang;
                item.Hide = false;
                item.NoiBat = false;
                item.TrangThaiXuLy = false;
                db.tbChiTietPhongs.Add(item);
                db.SaveChanges();
                if (RoomImages != null && RoomImages.Any())
                {
                    var uploadedPaths = UploadImages(RoomImages); // Lấy danh sách đường dẫn ảnh

                    foreach (var path in uploadedPaths)
                    {
                        // Thêm thông tin ảnh vào bảng Album
                        var album = new tbAlbum
                        {
                            IDPhong = item.IDPhong, // Liên kết với phòng vừa tạo
                            HinhAnh = path,        // Đường dẫn file ảnh
                            Hide = false           // Mặc định hiển thị
                        };

                        db.tbAlbums.Add(album);
                    }

                    db.SaveChanges(); // Lưu tất cả ảnh vào bảng Album
                }

                    return Redirect("/Admin/AChiTietPhongs");
            }
            catch
            {
                return View(tbChi);
            }
        }

        // GET: Admin/AChiTietPhongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbChiTietPhong tbChiTietPhong = db.tbChiTietPhongs.Find(id);
            if (tbChiTietPhong == null)
            {
                return HttpNotFound();
            }
            // Lấy danh sách hình ảnh liên quan
            var images = db.tbAlbums.Where(a => a.IDPhong == id && a.Hide == false).ToList();
            ViewBag.RoomImages = images;

            return View(tbChiTietPhong);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbChiTietPhong tbChi, IEnumerable<HttpPostedFileBase> RoomImages, int[] DeleteImages)
        {
            try
            {
                tbChiTietPhong item = new tbChiTietPhong();

                item = db.tbChiTietPhongs.Find(tbChi.IDPhong);
                item.MaTaiKhoan = tbChi.MaTaiKhoan;
                item.TieuDe = tbChi.TieuDe;
                item.DiaChi = tbChi.DiaChi;
                item.IDPhuong = tbChi.IDPhuong;
                item.IDQuan = tbChi.IDQuan;
                item.IDTP = tbChi.IDTP;
                item.GiaThue = tbChi.GiaThue*1000;
                item.MoTa = tbChi.MoTa;
                item.LinkBai = tbChi.LinkBai;
                item.GioGiac = tbChi.GioGiac;
                item.LinkMap = tbChi.LinkMap;
                item.TienDien = tbChi.TienDien;
                item.TienNuoc = tbChi.TienNuoc;
                item.TienDichVụ = tbChi.TienDichVụ;
                item.CuaSoBanCong = tbChi.CuaSoBanCong;
                item.WC = tbChi.WC;
                item.KeBep = tbChi.KeBep;
                item.MayGiat = tbChi.MayGiat;
                item.MayLanh = tbChi.MayLanh;
                item.Giuong = tbChi.Giuong;
                item.TuDo = tbChi.TuDo;
                item.Nem = tbChi.Nem;
                item.TuLanh = tbChi.TuLanh;
                item.BanGhe = tbChi.BanGhe;
                item.Sofa = tbChi.Sofa;
                item.SoPhongNgu = tbChi.SoPhongNgu;
                item.PhuongTien = tbChi.PhuongTien;
                item.ThuCung = tbChi.ThuCung;
                item.TienCoc = tbChi.TienCoc;
                item.Thang = tbChi.Thang;
                item.Hide = tbChi.Hide;
                item.NoiBat = tbChi.NoiBat;
                item.TrangThaiXuLy = tbChi.TrangThaiXuLy;
                // Xóa hình ảnh được chọn
                if (DeleteImages != null && DeleteImages.Length > 0)
                {
                    var imagesToDelete = db.tbAlbums.Where(a => DeleteImages.Contains(a.IDAlbum)).ToList();
                    foreach (var image in imagesToDelete)
                    {
                        db.tbAlbums.Remove(image);
                    }
                    db.SaveChanges();
                }

                // Thêm ảnh mới
                if (RoomImages != null && RoomImages.Any())
                {
                    var uploadedPaths = UploadImages(RoomImages);
                    foreach (var path in uploadedPaths)
                    {
                        var album = new tbAlbum
                        {
                            IDPhong = item.IDPhong,
                            HinhAnh = path,
                            Hide = false
                        };
                        db.tbAlbums.Add(album);
                    }
                }


                //// Kiểm tra nếu có upload hình ảnh mới
                //if (RoomImages != null && RoomImages.Any())
                //{
                //    // Xóa hình ảnh cũ trong bảng Album (nếu cần thay thế toàn bộ ảnh cũ)
                //    var oldImages = db.tbAlbums.Where(a => a.IDPhong == tbChi.IDPhong).ToList();
                //    foreach (var oldImage in oldImages)
                //    {
                //        db.tbAlbums.Remove(oldImage);
                //    }
                //    db.SaveChanges();

                //    // Thêm hình ảnh mới vào bảng Album
                //    var uploadedPaths = UploadImages(RoomImages);
                //    foreach (var path in uploadedPaths)
                //    {
                //        var album = new tbAlbum
                //        {
                //            IDPhong = item.IDPhong,
                //            HinhAnh = path,
                //            Hide = false
                //        };
                //        db.tbAlbums.Add(album);
                //    }
                //    db.SaveChanges();
                //}

                db.Entry(item).State = EntityState.Modified;               
                db.SaveChanges();
                return Redirect("/Admin/AChiTietPhongs");
            }
            catch (Exception ex)
            {
                return View(tbChi);
            }
        }

        // GET: Admin/AChiTietPhongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbChiTietPhong tbChiTietPhong = db.tbChiTietPhongs.Find(id);
            if (tbChiTietPhong == null)
            {
                return HttpNotFound();
            }
            return View(tbChiTietPhong);
        }

        // POST: Admin/AChiTietPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbChiTietPhong tbChiTietPhong = db.tbChiTietPhongs.Find(id);
            db.tbChiTietPhongs.Remove(tbChiTietPhong);
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
