using RentForRoom.DBContext;
using RentForRoom.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace RentForRoom.Controllers
{
    public class DatLichHenController : Controller
    {
        // GET: DatLichHen
        private QLNhaTroEntities db = new QLNhaTroEntities();
        public ActionResult Index()
        {
            try
            {
                DatLichModel ban = new DatLichModel();
                ban = (from ab in db.tbDatLiches
                       join qq in db.tbChiTietPhongs on ab.IDPhong equals qq.IDPhong into qqs
                       from qq in qqs.DefaultIfEmpty()
                       select (new DatLichModel
                       {
                           IDPhong = qq.IDPhong,
                           TieuDe = qq.TieuDe,
                           DiaChi = qq.DiaChi,
                           IDPhuong = qq.IDPhuong,
                           IDQuan = qq.IDQuan,
                           IDTP = qq.IDTP,
                           GiaThue = (float?)qq.GiaThue,
                       })).FirstOrDefault();
                return PartialView(ban);
            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }
        [HttpPost]
        public ActionResult Index(DatLichModel tbCon, int roomid)
        {
            try
            {
                    
                var contact = new tbDatLich
                {
                    SoNguoiO = tbCon.SoNguoiO,
                    NgayXemPhong = tbCon.NgayXemPhong,
                    GhiChu = tbCon.GhiChu,
                    NgayChuyenVao = tbCon.NgayChuyenVao,
                    HoTen = tbCon.HoTen,
                    SDT = tbCon.SDT,
                    Email = tbCon.Email,
                    ThuCung = tbCon.ThuCung,
                    SoLuongXe = tbCon.SoLuongXe,
                    Hide = false
                };
                db.tbDatLiches.Add(contact);
                db.SaveChanges();
                return Redirect("/Home/Index");
            }
            catch
            {
                return PartialView(tbCon);


            }

        }
        [HttpPost]
        public JsonResult GetJson(tbDatLich obj)
        {
            tbDatLich user = new tbDatLich();
            try
            {
                user = new tbDatLich
                {
                    SoNguoiO = obj.SoNguoiO,
                    NgayXemPhong = obj.NgayXemPhong,
                    GhiChu = obj.GhiChu,
                    NgayChuyenVao = obj.NgayChuyenVao,
                    HoTen = obj.HoTen,
                    SDT = obj.SDT,
                    Email = obj.Email,
                    SoLuongXe = obj.SoLuongXe,
                    ThuCung = obj.ThuCung,
                    Hide = false
            };
                db.tbDatLiches.Add(user);
                db.SaveChanges();
            }
            catch
            {
                user = new tbDatLich();
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}