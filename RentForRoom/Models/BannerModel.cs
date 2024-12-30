using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentForRoom.Models
{
    public class BannerModel
    {
        public int IDBanner { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string HinhBanner { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}