using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        // GET: Admin/Users
        private FivePEntities db = new FivePEntities();
        public ActionResult Index()
        {
            
            return View(db.Users.OrderByDescending(n=>n.user_datecreated).ToList());
        }
        public ActionResult TechnologyCare()
        {
            return View(db.Users.ToList());
        }
    }
}