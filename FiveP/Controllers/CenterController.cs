using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;
using PagedList;
using PagedList.Mvc;

namespace FiveP.Controllers
{
    public class CenterController : Controller
    {
        // GET: Center
        FivePEntities db = new FivePEntities();
        public ActionResult IndexCenter(int? page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true).OrderByDescending(n => n.post_datecreated).ToList();

            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return View(post.ToPagedList(number, size));
            }
            else if (page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
        }
        //phân trang center (tất cả câu hỏi)
        public PartialViewResult IndexCenterPage(int? page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true).OrderByDescending(n => n.post_datecreated).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            System.Threading.Thread.Sleep(2000);
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return PartialView(post.ToPagedList(number, size));
            }
            else if (countPost % size !=0 && page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else if(countPost % size == 0 && page > (countPost / size))
            {
                page = (countPost / size);

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
        }
        public ActionResult Hot(int? page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true).OrderByDescending(n => n.post_popular).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return View(post.ToPagedList(number, size));
            }
            else if (page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
        }
        public PartialViewResult HotPage(int? page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true).OrderByDescending(n => n.post_popular).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            System.Threading.Thread.Sleep(2000);
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return PartialView(post.ToPagedList(number, size));
            }
            else if (countPost % size != 0 && page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else if (countPost % size == 0 && page > (countPost / size))
            {
                page = (countPost / size);

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
        }
        public ActionResult View(int?page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true).OrderByDescending(n => n.post_view).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return View(post.ToPagedList(number, size));
            }
            else if (page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
        }
        public PartialViewResult ViewPage(int? page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true).OrderByDescending(n => n.post_view).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            System.Threading.Thread.Sleep(2000);
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return PartialView(post.ToPagedList(number, size));
            }
            else if (countPost % size != 0 && page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else if (countPost % size == 0 && page > (countPost / size))
            {
                page = (countPost / size);

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
        }
        public ActionResult Reply(int? page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true).OrderByDescending(n => n.post_sum_reply).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return View(post.ToPagedList(number, size));
            }
            else if (page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
        }
        public PartialViewResult ReplyPage(int? page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true).OrderByDescending(n => n.post_sum_reply).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            System.Threading.Thread.Sleep(2000);
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return PartialView(post.ToPagedList(number, size));
            }
            else if (countPost % size != 0 && page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else if (countPost % size == 0 && page > (countPost / size))
            {
                page = (countPost / size);

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
        }
        public ActionResult Week(int? page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_datecreated.Value.Year == DateTime.Now.Year && n.post_datecreated.Value.Month == DateTime.Now.Month).OrderByDescending(n => n.post_sum_reply).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            if(countPost ==0)
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return View(post.ToPagedList(number, size));
            }
            else if (page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(post.ToPagedList(number, size));
            }
        }
        public PartialViewResult WeekPage(int? page)
        {
            int size = 2;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_datecreated.Value.Year == DateTime.Now.Year && n.post_datecreated.Value.Month == DateTime.Now.Month).OrderByDescending(n => n.post_sum_reply).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            System.Threading.Thread.Sleep(2000);
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return PartialView(post.ToPagedList(number, size));
            }
            else if (countPost % size != 0 && page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else if (countPost % size == 0 && page > (countPost / size))
            {
                page = (countPost / size);

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
        }
        public PartialViewResult TopQuestion()
        {

            //Chưa có tài khoản?
            List<Post> posts = db.Posts.OrderByDescending(n => n.post_view).ThenByDescending(n => n.post_sum_reply).Take(4).ToList();
            return PartialView(posts);
        }
        public PartialViewResult TopTechnology()
        {
            List<Technology> technologies = db.Technologies.OrderByDescending(n => n.technology_popular).Take(3).ToList();
            return PartialView(technologies);
        }
        public PartialViewResult TopUser()
        {
            List<User> users = db.Users.OrderByDescending(n => n.user_vip_medal).Take(4).ToList();
            return PartialView(users);
        }
        public PartialViewResult MayBeInterested()
        {
            //bài viết công nghệ nổi tiếng khác
            List<Post> posts = db.Posts.OrderByDescending(n => n.post_popular).Take(4).ToList();
            return PartialView(posts);
        }
        public PartialViewResult SuggestionToMakeFriends()
        {
            //Gợi ý kết bạn có chung công nghệ, nhưng chưa kết bạn
            return PartialView();
        }
    }
}