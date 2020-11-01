using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiveP.Controllers
{
    public class UserPageController : Controller
    {
        // GET: UserPage
        public ActionResult IndexUserPage()
        {
            return View();
        }
    }
}