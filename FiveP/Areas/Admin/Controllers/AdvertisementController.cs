using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Areas.Admin.Controllers
{
    public class AdvertisementController : Controller
    {
        // GET: Admin/Advertisement
        private FivePEntities db = new FivePEntities();
        public ActionResult Index()
        {
            return View(db.Advertisements.ToList());
        }
        public ActionResult Img()
        {
            return View(db.Image_Advertisement.ToList());
        }
    }
}