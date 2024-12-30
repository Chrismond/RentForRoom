using RentForRoom.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentForRoom.Models
{
    public class AlbumModel
    {
        public int IDAlbum { get; set; }
        public Nullable<int> IDPhong { get; set; }
        public string TieuDe { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<bool> Hide { get; set; }
        public virtual tbChiTietPhong Room { get; set; }
    }
}