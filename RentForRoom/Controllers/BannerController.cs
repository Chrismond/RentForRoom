using RentForRoom.DBContext;
using RentForRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentForRoom.Controllers
{
    public class BannerController : Controller
    {
        // GET: Banner
        private QLNhaTroEntities db = new QLNhaTroEntities();
        public ActionResult Index()
        {
            try
            {
                BannerModel ban = new BannerModel();
                ban = (from ab in db.tbBanners

                          select (new BannerModel
                          {
                              IDBanner = ab.IDBanner,
                              TieuDe = ab.TieuDe,
                              NoiDung = ab.NoiDung,
                              HinhBanner = ab.HinhBanner,
                              Hide = (bool)ab.Hide,
                          })).FirstOrDefault();
               
                return PartialView(ban);

            }
            catch (Exception ex)
            {
                return Redirect("/not-found");
            }
        }
    }
}