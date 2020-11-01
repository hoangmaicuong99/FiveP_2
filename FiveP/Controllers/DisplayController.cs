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
    }
}