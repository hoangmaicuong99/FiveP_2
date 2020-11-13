using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        // GET: Admin/Post
        FivePEntities db = new FivePEntities();
        public ActionResult Index()
        {
            var posts = db.Posts.ToList();
            return View(posts);
        }
        public ActionResult Tags()
        {
            return View(db.Tags.ToList());
        }
        public ActionResult TechnologyPost()
        {
            return View(db.Technology_Post.ToList());
        }
    }
}