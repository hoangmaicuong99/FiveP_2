using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Areas.Admin.Controllers
{
    public class AddressController : Controller
    {
        // GET: Admin/Address
        private FivePEntities db = new FivePEntities();
        public ActionResult Commune()
        {
            return View(db.Communes.ToList());
        }
        public ActionResult District()
        {
            return View(db.Districts.ToList());
        }
        public ActionResult Provincial()
        {
            return View(db.Provincials.ToList());
        }
    }
}