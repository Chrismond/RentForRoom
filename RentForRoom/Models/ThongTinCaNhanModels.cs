using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentForRoom.Models
{
    public class ThongTinCaNhanModels
    {
        public int IDUser { get; set; }
        public Nullable<int> MaTaiKhoan { get; set; }
        public Nullable<int> Role { get; set; }
        public string SDT { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Gmail { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}