using RentForRoom.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentForRoom.Models
{
    public class ChiTietPhongModel
    {
        public tbChiTietPhong ChiTietPhong { get; set; }
        public List<AlbumModel> AlbumList { get; set; }

       
    }
}