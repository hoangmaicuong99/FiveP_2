using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Areas.Admin.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Admin/Notification
        private FivePEntities db = new FivePEntities();
        public ActionResult Notification()
        {
            return View(db.Notifications.ToList());
        }
        public ActionResult NotificationAdmin()
        {
            return View(db.Admin_Notification.ToList());
        }
    }
}