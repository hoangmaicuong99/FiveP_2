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
            return View(db.Technologies.OrderByDescending(n=>n.technology_datetime).ToList());
        }
        [HttpPost]
        public ActionResult AddTechnology([Bind(Include = "technology_id,technology_name,technology_datetime,technology_popular,technology_content")] Technology technology)
        {
            technology.technology_datetime = DateTime.Now;
            db.Technologies.Add(technology);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}