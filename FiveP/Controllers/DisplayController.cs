using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FiveP.Models;


namespace FiveP.Controllers
{
    public class DisplayController : Controller
    {
        FivePEntities db = new FivePEntities();
        // GET: Display
        public PartialViewResult HeadCenter()
        {
            return PartialView();
        }
        public ActionResult Member(int? page)
        {
            int size = 2;
            List<User> users = db.Users.Where(n => n.user_activate == true && n.user_activate_admin == true).OrderBy(n => n.user_datecreated).ToList();

            int countUser = users.Count();
            ViewBag.demUser = countUser;
            ViewBag.size = size;
            int number = (page ?? 1);
            ViewBag.page = number;

            return View(users.ToPagedList(number, size));
        }
        public PartialViewResult MemberPage(int? page)
        {
            int size = 2;
            List<User> users = db.Users.Where(n => n.user_activate == true && n.user_activate_admin == true).OrderBy(n => n.user_datecreated).ToList();
            int countPost = users.Count();
            ViewBag.demCauHoi = countPost;
            System.Threading.Thread.Sleep(2000);
            if (countPost < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return PartialView(users.ToPagedList(number, size));
            }
            else if (countPost % size != 0 && page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(users.ToPagedList(number, size));
            }
            else if (countPost % size == 0 && page > (countPost / size))
            {
                page = (countPost / size);

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(users.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(users.ToPagedList(number, size));
            }
        }
        public ActionResult Technology(int? page)
        {
            int size = 2;
            List<Technology> technologies = db.Technologies.ToList();

            int countTechnology = technologies.Count();
            ViewBag.demTechnology = countTechnology;
            ViewBag.size = size;
            int number = (page ?? 1);
            ViewBag.page = number;

            return View(technologies.ToPagedList(number, size));
        }
        public PartialViewResult TechnologyPage(int? page)
        {
            int size = 2;
            List<Technology> technologies = db.Technologies.ToList();
            int countTechnology = technologies.Count();
            ViewBag.demTechnology = countTechnology;

            System.Threading.Thread.Sleep(2000);
            if (countTechnology < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return PartialView(technologies.ToPagedList(number, size));
            }
            else if (countTechnology % size != 0 && page > (countTechnology / size) + 1)
            {
                page = (countTechnology / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(technologies.ToPagedList(number, size));
            }
            else if (countTechnology % size == 0 && page > (countTechnology / size))
            {
                page = (countTechnology / size);

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(technologies.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(technologies.ToPagedList(number, size));
            }
        }
        public ActionResult ManagePost()
        {
            User user = (User)Session["user"];
            if (user == null)
            {
                return Redirect("/Center/IndexCenter");
            }
            List<Post> post = db.Posts.Where(n => n.user_id == user.user_id && n.post_activate_admin == true && n.post_RecycleBin == false).ToList();
            
            return View(post);
        }
        public ActionResult RecycleBinPost(int? id)
        {
            db.Posts.Find(id).post_RecycleBin = true;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }


        public ActionResult History()
        {
            return View();
        }
        public ActionResult Tick()
        {
            User user = (User)Session["user"];
            List<Tick_Post> tick_Posts = db.Tick_Post.Where(n=>n.tick_recyclebin == false && n.user_id == user.user_id).OrderByDescending(n => n.tick_post_datetime).ToList();
            return View(tick_Posts);
        }
        public ActionResult RecycleBinTick(int? id)
        {
            db.Tick_Post.Find(id).tick_recyclebin = true;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Notification()
        {
            User user = (User)Session["user"];
            List<Notification> notifications = db.Notifications.Where(n => n.user_id == user.user_id && n.notification_status == true).ToList();
            return View(notifications);
        }
        public ActionResult RecycleBinNotification(int? id)
        {
            db.Notifications.Find(id).notification_status = false;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Friend()
        {
            User user = (User)Session["user"];
            List<Friend> friends = db.Friends.Where(n => n.user_id == user.user_id && n.friend_status == true || n.user_friend_id == user.user_id && n.friend_status == true).ToList();
            return View(friends);
        }
        public ActionResult FriendRecently()
        {
            User user = (User)Session["user"];
            List<Friend> friends = db.Friends.Where(n => n.user_id == user.user_id && n.friend_status == true || n.user_friend_id == user.user_id && n.friend_status == true).OrderByDescending(n=>n.friend_datecreate).Take(30).ToList();
            return View(friends);
        }
        public ActionResult FriendInvitation()
        {
            User user = (User)Session["user"];
            List<Friend> friends = db.Friends.Where(n => n.user_friend_id == user.user_id && n.friend_status == false).ToList();
            return View(friends);
        }
        public ActionResult FriendSend()
        {
            User user = (User)Session["user"];
            List<Friend> friends = db.Friends.Where(n => n.user_id == user.user_id && n.friend_status == false).ToList();
            return View(friends);
        }
    }
}