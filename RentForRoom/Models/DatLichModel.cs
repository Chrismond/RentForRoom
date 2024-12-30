using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentForRoom.Models
{
    public class DatLichModel
    {
        public int IDDatLich { get; set; }
        public Nullable<int> IDPhong { get; set; }
        public Nullable<int> SoNguoiO { get; set; }
        public Nullable<int> SoLuongXe { get; set; }
        public Nullable<System.DateTime> NgayXemPhong { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> NgayChuyenVao { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public Nullable<bool> ThuCung { get; set; }
        public Nullable<bool> Hide { get; set; }
        public string TieuDe { get; set; }
        public string DiaChi { get; set; }
        public Nullable<int> IDPhuong { get; set; }
        public Nullable<int> IDQuan { get; set; }
        public Nullable<int> IDTP { get; set; }
        public Nullable<float> GiaThue { get; set; }
        public string HinhAnh { get; set; }
    }

}