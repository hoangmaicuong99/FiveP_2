using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            
            int size = 15;
            int number = (page ?? 1);

            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_RecycleBin == false).OrderByDescending(n => n.post_datecreated).ToList();
            int countPost = post.Count();

            ViewBag.demCauHoi = countPost;
            ViewBag.size = size;
            ViewBag.page = number;
            return View(db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_RecycleBin == false).OrderByDescending(n => n.post_datecreated).ToPagedList(number, size));
        }
        //phân trang center (tất cả câu hỏi)
        public PartialViewResult IndexCenterPage(int? page)
        {
            int size = 15;
            
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_RecycleBin == false).OrderByDescending(n => n.post_datecreated).ToList();
            
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;

            System.Threading.Thread.Sleep(2000);

            ViewBag.size = size;
            
            if(page <1)
            {
                page = 1;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else if( page > (countPost / size) && (countPost % size) == 0)
            {
                // nếu lớn hơn sô trang thì cho trả về trang lớn nhất (page chẵng)
                page = (countPost / size);
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else if(page > (countPost / size) + 1 && (countPost % size) != 0)
            {
                // nếu lớn hơn sô trang thì cho trả về trang lớn nhất (page lẽ)

                page = (countPost / size)+1;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            else
            {
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(post.ToPagedList(number, size));
            }
            
        }
        public ActionResult Hot(int? page)
        {
            int size = 15;
            int number = (page ?? 1);

            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_RecycleBin == false).OrderByDescending(n => n.post_popular).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            ViewBag.size = size;
            ViewBag.page = number;

            return View(post.ToPagedList(number, size));
        }
        public PartialViewResult HotPage(int? page)
        {
            int size = 15;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_RecycleBin == false).OrderByDescending(n => n.post_popular).ToList();
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
            int size = 15;
            int number = (page ?? 1);
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_RecycleBin == false).OrderByDescending(n => n.post_view).ToList();
            int countPost = post.Count();

            ViewBag.demCauHoi = countPost;
            ViewBag.size = size;
            ViewBag.page = number;

            return View(post.ToPagedList(number, size));
        }
        public PartialViewResult ViewPage(int? page)
        {
            int size = 15;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_RecycleBin == false).OrderByDescending(n => n.post_view).ToList();
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
            int size = 15;
            int number = (page ?? 1);
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_RecycleBin == false).OrderByDescending(n => n.post_sum_reply).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            ViewBag.size = size;
            ViewBag.page = number;

            return View(post.ToPagedList(number, size));
        }
        public PartialViewResult ReplyPage(int? page)
        {
            int size = 15;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_RecycleBin == false).OrderByDescending(n => n.post_sum_reply).ToList();
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
            int size = 15;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_datecreated.Value.Year == DateTime.Now.Year && n.post_datecreated.Value.Month == DateTime.Now.Month && n.post_RecycleBin == false).OrderByDescending(n => n.post_sum_reply).ToList();
            int countPost = post.Count();
            ViewBag.demCauHoi = countPost;
            if(countPost ==0)
            {
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
            int size = 15;
            List<Post> post = db.Posts.Where(n => n.post_activate_admin == true && n.post_activate == true && n.post_datecreated.Value.Year == DateTime.Now.Year && n.post_datecreated.Value.Month == DateTime.Now.Month && n.post_RecycleBin == false).OrderByDescending(n => n.post_sum_reply).ToList();
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
            User user = (User)Session["user"];
            Technology_Care technology_Care = db.Technology_Care.FirstOrDefault(n => n.user_id == user.user_id);
            if(technology_Care != null)
            {
                List<Technology_Care> technology_Cares = db.Technology_Care.Where(n => n.technology_id == technology_Care.technology_id).GroupBy(x => x.user_id).Select(y => y.FirstOrDefault()).Take(7).ToList();
                return PartialView(technology_Cares);
            }
            else
            {
                List<Technology_Care> technology_Cares = db.Technology_Care.GroupBy(x => x.user_id).Select(y => y.FirstOrDefault()).Take(7).ToList();
                return PartialView(technology_Cares);
            }
            
        }
        public ActionResult DeletePost(int? id)
        {
            db.Posts.Find(id).post_RecycleBin = true;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult EditPost(int? id)
        {
            db.Posts.Find(id).post_RecycleBin = true;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult HiddenPost(int? id)
        {
            db.Posts.Find(id).post_activate_admin = false;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}