using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentForRoom.Models
{
    public class PhongModel
    {
        public string HinhAnh { get; set; }
        public int IDPhong { get; set; }
        public Nullable<int> MaTaiKhoan { get; set; }
        public string TieuDe { get; set; }
        public string DiaChi { get; set; }
        public Nullable<int> IDPhuong { get; set; }
        public Nullable<int> IDQuan { get; set; }
        public Nullable<int> IDTP { get; set; }
        public Nullable<float> GiaThue { get; set; }
        public string MoTa { get; set; }
        public string LinkBai { get; set; }
        public string GioGiac { get; set; }
        public string LinkMap { get; set; }
        public Nullable<double> TienDien { get; set; }
        public Nullable<double> TienNuoc { get; set; }
        public Nullable<double> TienDichVụ { get; set; }
        public Nullable<bool> CuaSoBanCong { get; set; }
        public Nullable<bool> WC { get; set; }
        public Nullable<bool> KeBep { get; set; }
        public Nullable<int> MayLanh { get; set; }
        public Nullable<int> Giuong { get; set; }
        public Nullable<int> TuDo { get; set; }
        public Nullable<int> Nem { get; set; }
        public Nullable<int> TuLanh { get; set; }
        public Nullable<int> BanGhe { get; set; }
        public Nullable<bool> MayGiat { get; set; }
        public Nullable<int> Sofa { get; set; }
        public Nullable<int> SoPhongNgu { get; set; }
        public Nullable<bool> Thang { get; set; }
        public string PhuongTien { get; set; }
        public Nullable<bool> ThuCung { get; set; }
        public Nullable<bool> TienCoc { get; set; }
        public Nullable<bool> Hide { get; set; }
        public Nullable<bool> NoiBat { get; set; }
        public Nullable<bool> TrangThaiXuLy { get; set; }
    }
}