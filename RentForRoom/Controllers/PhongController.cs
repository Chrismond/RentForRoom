using RentForRoom.Areas.Admin.Controllers;
using RentForRoom.DBContext;
using RentForRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentForRoom.Controllers
{
    public class PhongController : Controller
    {
        // GET: Phong
        private  QLNhaTroEntities db = new QLNhaTroEntities();
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
            try
            {
                List<PhongModel> ban = (from ab in db.tbChiTietPhongs
                                        where ab.TrangThaiXuLy == true
                                        join album in
                                   (from a in db.tbAlbums
                                    group a by a.IDPhong into g
                                    select g.FirstOrDefault())
                                   on ab.IDPhong equals album.IDPhong into albums
                                        from album in albums.DefaultIfEmpty() 
                                        select (new PhongModel
                                       {
                                           HinhAnh = album.HinhAnh,
                                           IDPhong = ab.IDPhong,
                                           MaTaiKhoan = ab.MaTaiKhoan,
                                           TieuDe = ab.TieuDe,
                                           DiaChi = ab.DiaChi,
                                           IDPhuong = ab.IDPhuong,
                                           IDQuan = ab.IDQuan,
                                           IDTP = ab.IDTP,
                                           GiaThue = (float)ab.GiaThue,
                                           MoTa = ab.MoTa,
                                           LinkBai = ab.LinkBai,
                                           GioGiac = ab.GioGiac,
                                           LinkMap = ab.LinkMap,
                                           TienDien = ab.TienDien,
                                           TienNuoc = ab.TienNuoc,
                                           TienDichVụ = ab.TienDichVụ,
                                           CuaSoBanCong = ab.CuaSoBanCong,
                                           WC = ab.WC,
                                           KeBep = ab.KeBep,
                                           MayLanh = ab.MayLanh,
                                           Giuong = ab.Giuong,
                                           TuDo = ab.TuDo,
                                           Nem = ab.Nem,
                                           TuLanh = ab.TuLanh,
                                           BanGhe = ab.BanGhe,
                                           MayGiat = ab.MayGiat,
                                           Sofa = ab.Sofa,
                                           SoPhongNgu = ab.SoPhongNgu,
                                           Thang = ab.Thang,
                                           PhuongTien = ab.PhuongTien,
                                           ThuCung = ab.ThuCung,
                                           TienCoc = ab.TienCoc,
                                           Hide = (bool)ab.Hide,
                                           NoiBat = (bool)ab.NoiBat
                                            })).ToList();
                return PartialView(ban);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }
        public JsonResult GetRoomInfo(int roomId)
        {
            var room = db.tbChiTietPhongs.FirstOrDefault(r => r.IDPhong == roomId);
            if (room == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var roomData = new
            {
                RoomId = room.IDPhong,
                //PropAvatar = room.HinhAnh,
                PropName = room.MoTa,
                PropPrice = room.GiaThue,
                PropAddress = room.DiaChi,
                PropTypeName = room.TieuDe
            };

            return Json(roomData, JsonRequestBehavior.AllowGet);
        }



    }
}