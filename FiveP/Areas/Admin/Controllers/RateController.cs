using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Areas.Admin.Controllers
{
    public class RateController : Controller
    {
        // GET: Admin/Rate
        private FivePEntities db = new FivePEntities();
        public ActionResult RatePost()
        {
            return View(db.Rate_Post.ToList());
        }
        public ActionResult RateReplyPost()
        {
            return View(db.Rate_Reply_Post.ToList());
        }
        public ActionResult TickPost()
        {
            return View(db.Tick_Post.ToList());
        }
        public ActionResult ShowActivatePost()
        {
            return View(db.Show_Activate_Post.ToList());
        }
        public ActionResult ShowActivateReplyPost()
        {
            return View(db.Show_Activate_Reply_Post.ToList());
        }
    }
}