using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Controllers
{
    public class UserPageController : Controller
    {
        // GET: UserPage
        FivePEntities db = new FivePEntities();
        public ActionResult IndexUserPage(int? id)
        {
            User user = db.Users.SingleOrDefault(n => n.user_activate_admin == true && n.user_id == id && n.user_activate == true);
            return View(user);
        }
    }
}