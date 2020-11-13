using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Controllers
{
    public class UserManagerController : Controller
    {
        // GET: UserManager
        FivePEntities db = new FivePEntities();
        public ActionResult IndexUserManager()
        {
            User user = (User)Session["user"];
            User users = db.Users.SingleOrDefault(n => n.user_activate_admin == true && n.user_id == user.user_id);
            return View(users);
        }
    }
}