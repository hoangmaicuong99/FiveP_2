using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiveP.Controllers
{
    public class DisplayController : Controller
    {
        // GET: Display
        public ActionResult Member()
        {
            return View();
        }
        public ActionResult Technology()
        {
            return View();
        }
        public ActionResult ManagePost()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }
        public ActionResult Tick()
        {
            return View();
        }
        public ActionResult Notification()
        {
            return View();
        }
        public ActionResult Friend()
        {
            return View();
        }
    }
}