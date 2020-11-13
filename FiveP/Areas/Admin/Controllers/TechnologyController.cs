using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Areas.Admin.Controllers
{
    public class TechnologyController : Controller
    {
        // GET: Admin/Technology
        private FivePEntities db = new FivePEntities();
        public ActionResult Index()
        {
            return View(db.Technologies.ToList());
        }
    }
}