using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FiveP.Models;
using System.Text;


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
        public JsonResult GetValueSearch(string search)
        {
            List<PostSearch> postSearches = db.Posts.Where(n => n.post_title.Contains(search)).OrderByDescending(n => n.post_popular).Select(x => new PostSearch
            {
                post_id = x.post_id,
                post_title = x.post_title,
                post_popular = x.post_popular
            }).ToList();
            List<PostSearchEncode> postSearchEncodes = new List<PostSearchEncode>();
            foreach(var item in postSearches)
            {
                PostSearchEncode postSearchEncode = new PostSearchEncode()
                {
                    post_id = Convert.ToBase64String(Encoding.ASCII.GetBytes(item.post_id.ToString())),
                    post_title = item.post_title,
                    post_popular = item.post_popular
                };
                postSearchEncodes.Add(postSearchEncode);
            }
            return new JsonResult { Data = postSearchEncodes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public ActionResult Search(FormCollection f)
        {
            String Search = f["Search"].ToString();
            //tìm kiếm theo từ chính xác nhất của title post
            List<Post> posts3 = db.Technology_Post.Where(n => n.Post.post_title == Search).OrderByDescending(n => n.Post.post_popular).Select(n => n.Post).ToList();
            ViewBag.posts3 = posts3;

            // theo từ chính xác nhất của thẻ tag
            List<Post> posts4 = db.Tags.Where(n => n.tags_name == Search).GroupBy(x => x.post_id).Select(y => y.FirstOrDefault()).OrderByDescending(n => n.Post.post_popular).Select(n => n.Post).ToList();
            ViewBag.posts8 = posts4.Except(posts3).ToList();
            List<Post> posts9 = posts4.Union(posts3).ToList();


            // theo title mà độ dài nội dung ngắn gần bằng từ tìm kiếm
            List<Post> posts5 = db.Technology_Post.Where(n => n.Post.post_title.Length + 15 >= Search.Length && n.Post.post_title.Length - 15 <= Search.Length && n.Post.post_title.Contains(Search)).OrderByDescending(n => n.Post.post_popular).Select(n => n.Post).ToList();
            ViewBag.posts10 = posts5.Except(posts9).ToList();
            List<Post> posts11 = posts5.Union(posts9).ToList();

            // theo title mà độ dài nội dung ngắn gần bằng từ tìm kiếm (xa hơn miếng nữa ...)
            List<Post> posts = db.Posts.Where(n => n.post_title.Length + 25 >= Search.Length && n.post_title.Length - 25 <= Search.Length && n.post_title.Contains(Search)).OrderByDescending(n => n.post_popular).ToList();
            ViewBag.posts12 = posts.Except(posts11).ToList();
            List<Post> posts13 = posts.Union(posts11).ToList();


            // theo từ gần chính xác thẻ tag
            List<Post> posts6 = db.Tags.Where(n => n.tags_name.Contains(Search)).GroupBy(n => n.post_id).Select(n => n.FirstOrDefault()).OrderByDescending(n => n.Post.post_popular).Select(n => n.Post).ToList();
            ViewBag.posts14 = posts6.Except(posts13).ToList();
            List<Post> posts15 = posts6.Union(posts13).ToList();

            // theo nội dung, từ ngắn nhất tới dài nhất
            List<Post> posts1 = db.Posts.Where(n => n.post_content.Contains(Search)).OrderBy(n => n.post_content.Length).ToList();
            ViewBag.posts16 = posts1.Except(posts15).ToList();
            List<Post> posts17 = posts1.Union(posts15).ToList();

            // theo comment
            List<Post> posts7 = db.Comments.Where(n => n.comment_content.Contains(Search)).OrderByDescending(n => n.Reply_Post.Post.post_popular).Select(n => n.Reply_Post.Post).ToList();
            ViewBag.posts18 = posts7.Except(posts17).ToList();
            List<Post> posts19 = posts7.Union(posts17).ToList();

            // theo gần chính xác của title
            List<Post> posts2 = db.Posts.Where(n => n.post_title.Contains(Search)).OrderByDescending(n => n.post_popular).ToList();
            List<Post> posts20 = posts2.Except(posts19).ToList();
            return View(posts20);
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
            int size = 24;
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
            int size = 24;
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