using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Controllers
{
    public class DetailQuestionController : Controller
    {
        // GET: DetailQuestion
        FivePEntities db = new FivePEntities();
        public ActionResult IndexDetailQuestion(string id)
        {
            //mã hóa
            string encodes = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(id));
            int id_encode = int.Parse(encodes);

            //Thêm view
            db.Posts.Find(id_encode).post_view++;
            db.Posts.Find(id_encode).post_popular++;
            db.SaveChanges();
            Post post = db.Posts.SingleOrDefault(n => n.post_id == id_encode && n.post_activate == true && n.post_activate_admin == true && n.post_RecycleBin == false);
            return View(post);
        }
        //vow post trong chi tiết bài post
        [HttpPost]
        public ActionResult RatePostT(Rate_Post rate_Post)
        {
            User user = (User)Session["user"];
            Rate_Post ratePost = db.Rate_Post.Where(n => n.post_id == rate_Post.post_id && n.user_id == user.user_id).SingleOrDefault();
            if (ratePost == null)
            {
                db.Posts.Find(rate_Post.post_id).post_popular++;
                db.Posts.Find(rate_Post.post_id).post_calculate_medal++;
                db.SaveChanges();
                //tính huy chương đưa vào user
                var postCalulateMedal = db.Posts.Find(rate_Post.post_id).post_calculate_medal;
                if (postCalulateMedal == 4)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal++;
                }
                else if (postCalulateMedal == 8)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal--;
                }
                else if (postCalulateMedal == 15)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal--;
                }
                else if (postCalulateMedal == 30)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_vip_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal--;
                }
                //Lưu đánh giá của bài viết
                rate_Post.user_id = user.user_id;
                rate_Post.rate_post1 = true;
                rate_Post.rate_post_datetime = DateTime.Now;
                db.Rate_Post.Add(rate_Post);
                db.SaveChanges();
                return View();
            }
            else if (ratePost.rate_post1 == true)
            {
                db.Posts.Find(rate_Post.post_id).post_calculate_medal--;
                db.SaveChanges();
                //tính huy chương đưa vào user
                var postCalulateMedal = db.Posts.Find(rate_Post.post_id).post_calculate_medal;
                if (postCalulateMedal == 3)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal--;
                }
                else if (postCalulateMedal == 7)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal++;
                }
                else if (postCalulateMedal == 14)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal++;
                }
                else if (postCalulateMedal == 29)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_vip_medal--;
                }
                //Đánh giá
                db.Posts.Find(rate_Post.post_id).post_popular--;
                db.Rate_Post.Find(ratePost.rate_post_id).rate_post1 = null;
                db.SaveChanges();
                return View();
            }
            else if (ratePost.rate_post1 == null)
            {
                db.Posts.Find(rate_Post.post_id).post_calculate_medal++;
                db.SaveChanges();
                //tính huy chương đưa vào user
                var postCalulateMedal = db.Posts.Find(rate_Post.post_id).post_calculate_medal;
                if (postCalulateMedal == 4)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal++;
                }
                else if (postCalulateMedal == 8)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal--;
                }
                else if (postCalulateMedal == 15)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal--;
                }
                else if (postCalulateMedal == 30)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_vip_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal--;
                }
                // lưu đánh giá
                db.Posts.Find(rate_Post.post_id).post_popular++;
                db.Rate_Post.Find(ratePost.rate_post_id).rate_post1 = true;
                db.SaveChanges();
                return View();
            }
            else
            {
                db.Posts.Find(rate_Post.post_id).post_calculate_medal += 2;
                db.SaveChanges();
                var postCalulateMedal = db.Posts.Find(rate_Post.post_id).post_calculate_medal;
                if (postCalulateMedal == 4 || postCalulateMedal == 5)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal++;
                }
                else if (postCalulateMedal == 8 || postCalulateMedal == 9)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal--;
                }
                else if (postCalulateMedal == 15 || postCalulateMedal == 16)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal--;
                }
                else if (postCalulateMedal == 30 || postCalulateMedal == 31)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_vip_medal++;
                }
                db.Posts.Find(rate_Post.post_id).post_popular += 2;
                db.Rate_Post.Find(ratePost.rate_post_id).rate_post1 = true;
                db.SaveChanges();
                return View();
            }
        }
        [HttpPost]
        public ActionResult RatePostF(Rate_Post rate_Post)
        {
            User user = (User)Session["user"];
            Rate_Post ratePost = db.Rate_Post.Where(n => n.post_id == rate_Post.post_id && n.user_id == user.user_id).SingleOrDefault();
            if (ratePost == null)
            {
                //tính huy chương user
                db.Posts.Find(rate_Post.post_id).post_calculate_medal--;
                db.SaveChanges();
                var postCalulateMedal = db.Posts.Find(rate_Post.post_id).post_calculate_medal;
                if (postCalulateMedal == 3)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal--;
                }
                else if (postCalulateMedal == 7)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal++;
                }
                else if (postCalulateMedal == 14)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal++;
                }
                else if (postCalulateMedal == 29)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_vip_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal++;
                }
                //Lưu đánh giá
                db.Posts.Find(rate_Post.post_id).post_popular--;
                rate_Post.user_id = user.user_id;
                rate_Post.rate_post1 = false;
                rate_Post.rate_post_datetime = DateTime.Now;
                db.Rate_Post.Add(rate_Post);
                db.SaveChanges();
                return View();
            }
            else if (ratePost.rate_post1 == false)
            {
                //Tính huy chương cho user
                db.Posts.Find(rate_Post.post_id).post_calculate_medal++;
                db.SaveChanges();
                var postCalulateMedal = db.Posts.Find(rate_Post.post_id).post_calculate_medal;
                if (postCalulateMedal == 4)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal++;
                }
                else if (postCalulateMedal == 8)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal--;
                }
                else if (postCalulateMedal == 15)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal--;
                }
                else if (postCalulateMedal == 30)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_vip_medal++;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal--;
                }
                //Lưu đánh giá
                db.Posts.Find(rate_Post.post_id).post_popular++;
                db.Rate_Post.Find(ratePost.rate_post_id).rate_post1 = null;
                db.SaveChanges();
                return View();
            }
            else if (ratePost.rate_post1 == null)
            {
                //Lưu Huy chương user
                db.Posts.Find(rate_Post.post_id).post_calculate_medal--;
                db.SaveChanges();
                var postCalulateMedal = db.Posts.Find(rate_Post.post_id).post_calculate_medal;
                if (postCalulateMedal == 3)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal--;
                }
                else if (postCalulateMedal == 7)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal++;
                }
                else if (postCalulateMedal == 14)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal++;
                }
                else if (postCalulateMedal == 29)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_vip_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal++;
                }
                //Lưu đánh giá
                db.Posts.Find(rate_Post.post_id).post_popular--;
                db.Rate_Post.Find(ratePost.rate_post_id).rate_post1 = false;
                db.SaveChanges();
                return View();
            }
            else
            {
                //tính huy chương user
                db.Posts.Find(rate_Post.post_id).post_calculate_medal -= 2;
                db.SaveChanges();
                var postCalulateMedal = db.Posts.Find(rate_Post.post_id).post_calculate_medal;
                if (postCalulateMedal == 3 || postCalulateMedal == 2)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal--;
                }
                else if (postCalulateMedal == 7 || postCalulateMedal == 6)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_bronze_medal++;
                }
                else if (postCalulateMedal == 14 || postCalulateMedal == 13)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_silver_medal++;
                }
                else if (postCalulateMedal == 29 || postCalulateMedal == 28)
                {
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_vip_medal--;
                    db.Users.Find(db.Posts.Find(rate_Post.post_id).user_id).user_gold_medal++;
                }
                //Luu đánh giá
                db.Posts.Find(rate_Post.post_id).post_popular -= 2;
                db.Rate_Post.Find(ratePost.rate_post_id).rate_post1 = false;
                db.SaveChanges();
                return View();
            }

        }
        //Đánh giấu bài viết tick post
        [HttpPost]
        public ActionResult SaveTickPost(Tick_Post tick_Post)
        {
            User user = (User)Session["user"];
            Tick_Post tickPost = db.Tick_Post.Where(n => n.post_id == tick_Post.post_id && n.user_id == user.user_id).FirstOrDefault();
            if (tickPost == null)
            {
                db.Posts.Find(tick_Post.post_id).post_popular++;
                tick_Post.user_id = user.user_id;
                tick_Post.tick_recyclebin = false;
                tick_Post.tick_post_datetime = DateTime.Now;
                db.Tick_Post.Add(tick_Post);
                db.SaveChanges();
                return View();
            }
            else
            {
                db.Posts.Find(tick_Post.post_id).post_popular--;
                db.Tick_Post.Remove(db.Tick_Post.Find(tickPost.tick_post_id));
                db.SaveChanges();
                return View();
            }
        }
        //Hiển thị hoạt động của bài viết
        [HttpPost]
        public ActionResult SaveShowActivatePost(Show_Activate_Post show_Activate_Post)
        {
            User user = (User)Session["user"];
            Show_Activate_Post showActivatePost = db.Show_Activate_Post.FirstOrDefault(n => n.post_id == show_Activate_Post.post_id && n.user_id == user.user_id);
            if (showActivatePost == null)
            {
                db.Posts.Find(show_Activate_Post.post_id).post_popular++;
                show_Activate_Post.user_id = user.user_id;
                show_Activate_Post.show_activate_post_datetime = DateTime.Now;
                show_Activate_Post.show_activate_post_Readed = true;
                db.Show_Activate_Post.Add(show_Activate_Post);
                db.SaveChanges();
                return View();
            }
            else
            {
                db.Posts.Find(show_Activate_Post.post_id).post_popular--;
                db.Show_Activate_Post.Remove(db.Show_Activate_Post.Find(showActivatePost.show_activate_post_id));
                db.SaveChanges();
                return View();
            }

        }
        //Hiển thị câu trả lời ở chi tiết câu trả lời
        public PartialViewResult ReplyPost(int? intPostId, int? intUserId)
        {
            ViewBag.userid = intUserId;
            List<Reply_Post> listReplyPost = db.Reply_Post.Where(n => n.post_id == intPostId && n.reply_post_activate == true).ToList();
            return PartialView(listReplyPost);
        }
        [HttpPost]
        public ActionResult RateReplyPostT(Rate_Reply_Post rate_Reply_Post)
        {
            User user = (User)Session["user"];
            Rate_Reply_Post rateReplyPost = db.Rate_Reply_Post.Where(n => n.reply_post_id == rate_Reply_Post.reply_post_id && n.user_id == user.user_id).SingleOrDefault();
            if (rateReplyPost == null)
            {
                //Lưu đánh giá huy chương
                db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal++;
                db.SaveChanges();
                var replyPostCalculateMedal = db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal;
                if (replyPostCalculateMedal == 4)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal++;
                }
                else if (replyPostCalculateMedal == 8)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal--;
                }
                else if (replyPostCalculateMedal == 15)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal--;
                }
                else if (replyPostCalculateMedal == 30)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_vip_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal--;
                }
                //lưu đánh giá bài trả lời
                var idRepLyPost = rate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idRepLyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular++;
                rate_Reply_Post.user_id = user.user_id;
                rate_Reply_Post.rate_reply_post1 = true;
                rate_Reply_Post.rate_reply_post_datetime = DateTime.Now;
                db.Rate_Reply_Post.Add(rate_Reply_Post);
                db.SaveChanges();
                return View();
            }
            else if (rateReplyPost.rate_reply_post1 == true)
            {
                //Lưu huy chương người dùng
                db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal--;
                db.SaveChanges();
                var replyPostCalculateMedal = db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal;
                if (replyPostCalculateMedal == 3)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal--;
                }
                else if (replyPostCalculateMedal == 7)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal++;
                }
                else if (replyPostCalculateMedal == 14)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal++;
                }
                else if (replyPostCalculateMedal == 29)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_vip_medal--;
                }
                //Lưu đánh giá trả lời bài viết
                var idRepLyPost = rate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idRepLyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular--;
                db.Rate_Reply_Post.Find(rateReplyPost.rate_reply_post_id).rate_reply_post1 = null;
                db.SaveChanges();
                return View();
            }
            else if (rateReplyPost.rate_reply_post1 == null)
            {
                //Lưu huy chương người dùng
                db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal++;
                db.SaveChanges();
                var replyPostCalculateMedal = db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal;
                if (replyPostCalculateMedal == 4)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal++;
                }
                else if (replyPostCalculateMedal == 8)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal--;
                }
                else if (replyPostCalculateMedal == 15)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal--;
                }
                else if (replyPostCalculateMedal == 30)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_vip_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal--;
                }
                //Lưu đánh giá bài viết
                var idRepLyPost = rate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idRepLyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular++;
                db.Rate_Reply_Post.Find(rateReplyPost.rate_reply_post_id).rate_reply_post1 = true;
                db.SaveChanges();
                return View();
            }
            else
            {
                //Lưu huy chương
                db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal += 2;
                db.SaveChanges();
                var replyPostCalculateMedal = db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal;
                if (replyPostCalculateMedal == 4 || replyPostCalculateMedal == 5)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal++;
                }
                else if (replyPostCalculateMedal == 8 || replyPostCalculateMedal == 9)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal--;
                }
                else if (replyPostCalculateMedal == 15 || replyPostCalculateMedal == 16)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal--;
                }
                else if (replyPostCalculateMedal == 30 || replyPostCalculateMedal == 31)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_vip_medal++;
                }
                //Lưu đánh giá 
                var idRepLyPost = rate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idRepLyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular += 2;
                db.Rate_Reply_Post.Find(rateReplyPost.rate_reply_post_id).rate_reply_post1 = true;
                db.SaveChanges();
                return View();
            }
        }
        [HttpPost]
        public ActionResult RateReplyPostF(Rate_Reply_Post rate_Reply_Post)
        {
            User user = (User)Session["user"];
            Rate_Reply_Post rateReplyPost = db.Rate_Reply_Post.Where(n => n.reply_post_id == rate_Reply_Post.reply_post_id && n.user_id == user.user_id).SingleOrDefault();
            if (rateReplyPost == null)
            {
                //Lưu huy chương
                db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal--;
                db.SaveChanges();
                var replyPostCalculateMedal = db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal;
                if (replyPostCalculateMedal == 3)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal--;
                }
                else if (replyPostCalculateMedal == 7)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal++;
                }
                else if (replyPostCalculateMedal == 14)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal++;
                }
                else if (replyPostCalculateMedal == 29)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_vip_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal++;
                }
                //Lưu đánh giá
                var idRepLyPost = rate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idRepLyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular--;
                rate_Reply_Post.user_id = user.user_id;
                rate_Reply_Post.rate_reply_post1 = false;
                rate_Reply_Post.rate_reply_post_datetime = DateTime.Now;
                db.Rate_Reply_Post.Add(rate_Reply_Post);
                db.SaveChanges();
                return View();
            }
            else if (rateReplyPost.rate_reply_post1 == false)
            {
                //Lưu huy chương
                db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal++;
                db.SaveChanges();
                var replyPostCalculateMedal = db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal;
                if (replyPostCalculateMedal == 4)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal++;
                }
                else if (replyPostCalculateMedal == 8)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal--;
                }
                else if (replyPostCalculateMedal == 15)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal--;
                }
                else if (replyPostCalculateMedal == 30)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_vip_medal++;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal--;
                }
                //Lưu đánh giá
                var idRepLyPost = rate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idRepLyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular++;
                db.Rate_Reply_Post.Find(rateReplyPost.rate_reply_post_id).rate_reply_post1 = null;
                db.SaveChanges();
                return View();
            }
            else if (rateReplyPost.rate_reply_post1 == null)
            {
                //Lưu huy chương
                db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal--;
                db.SaveChanges();
                var replyPostCalculateMedal = db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal;
                if (replyPostCalculateMedal == 3)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal--;
                }
                else if (replyPostCalculateMedal == 7)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal++;
                }
                else if (replyPostCalculateMedal == 14)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal++;
                }
                else if (replyPostCalculateMedal == 29)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_vip_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal++;
                }
                //Lưu đánh giá
                var idRepLyPost = rate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idRepLyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular--;
                db.Rate_Reply_Post.Find(rateReplyPost.rate_reply_post_id).rate_reply_post1 = false;
                db.SaveChanges();
                return View();
            }
            else
            {
                //Lưu huy chương
                db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal -= 2;
                db.SaveChanges();
                var replyPostCalculateMedal = db.Reply_Post.Find(rate_Reply_Post.reply_post_id).reply_post__calculate_medal;
                if (replyPostCalculateMedal == 3 || replyPostCalculateMedal == 2)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal--;
                }
                else if (replyPostCalculateMedal == 7 || replyPostCalculateMedal == 6)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_bronze_medal++;
                }
                else if (replyPostCalculateMedal == 14 || replyPostCalculateMedal == 13)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_silver_medal++;
                }
                else if (replyPostCalculateMedal == 29 || replyPostCalculateMedal == 28)
                {
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_vip_medal--;
                    db.Users.Find(db.Reply_Post.Find(rate_Reply_Post.reply_post_id).user_id).user_gold_medal++;
                }
                //Lưu đánh giá
                var idRepLyPost = rate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idRepLyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular -= 2;
                db.Rate_Reply_Post.Find(rateReplyPost.rate_reply_post_id).rate_reply_post1 = false;
                db.SaveChanges();
                return View();
            }
        }
        [HttpPost]
        public ActionResult ShowActivateReplyPost(Show_Activate_Reply_Post show_Activate_Reply_Post)
        {
            User user = (User)Session["user"];
            Show_Activate_Reply_Post showActivateReplyPost = db.Show_Activate_Reply_Post.Where(n => n.reply_post_id == show_Activate_Reply_Post.reply_post_id && n.user_id == user.user_id).FirstOrDefault();
            if (showActivateReplyPost == null)
            {
                var idReplyPost = show_Activate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idReplyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular++;
                show_Activate_Reply_Post.show_activate_reply_post_datetime = DateTime.Now;
                show_Activate_Reply_Post.show_activate_reply_post_readed = true;
                show_Activate_Reply_Post.user_id = user.user_id;
                db.Show_Activate_Reply_Post.Add(show_Activate_Reply_Post);
                db.SaveChanges();
                return View();
            }
            else
            {
                var idReplyPost = show_Activate_Reply_Post.reply_post_id;
                var idPost = db.Reply_Post.Find(idReplyPost).post_id.Value;
                db.Posts.Find(idPost).post_popular--;
                db.Show_Activate_Reply_Post.Remove(db.Show_Activate_Reply_Post.Find(showActivateReplyPost.show_activate_reply_post_id));
                db.SaveChanges();
                return View();
            }

        }
        public PartialViewResult Comment(int? id)
        {
            List<Comment> comment = db.Comments.Where(n => n.reply_post_id == id).OrderByDescending(n=>n.comment_datecreated).ToList();
            return PartialView(comment);
        }
        [HttpPost]
        public ActionResult Comment([Bind(Include = "comment_id,comment_content,comment_datecreated,comment_dateedit,user_id,comment_option,reply_post_id")] Comment comment, Notification notification)
        {

            User user = (User)Session["user"];
            //sum comment
            var idReplyPost = comment.reply_post_id;
            var idPost = db.Reply_Post.Find(idReplyPost).post_id;
            db.Posts.Find(idPost).post_sum_comment++;
            db.Posts.Find(idPost).post_popular++;

            //Notification
            List<Show_Activate_Reply_Post> show_Activate_Reply_Posts = db.Show_Activate_Reply_Post.Where(n => n.reply_post_id == comment.reply_post_id).ToList();
            foreach (var item in show_Activate_Reply_Posts)
            {
                if (item.user_id != user.user_id)
                {
                    notification.user_id = item.user_id;
                    notification.post_id = item.Reply_Post.post_id;
                    notification.notification_datecreate = DateTime.Now;
                    notification.notification_content = user.user_nicename + " Đã comment " + db.Reply_Post.Find(comment.reply_post_id).reply_post_content.Substring(0,27);
                    notification.notification_status = true;
                    db.Notifications.Add(notification);
                    db.SaveChanges();
                }
            }
            //Thông báo cho ai là người đăng câu trả lời này
            var idUserPost = db.Reply_Post.Find(comment.reply_post_id);
            if (idUserPost.user_id != user.user_id)
            {
                notification.user_id = idUserPost.user_id;
                notification.post_id = idUserPost.post_id;
                notification.notification_datecreate = DateTime.Now;
                //chưa làm xong
                notification.notification_content = user.user_nicename + " Đã trả lời bài viết " + db.Posts.Find(idUserPost.post_id).post_title.Substring(0, 27);
                notification.notification_status = true;
                db.Notifications.Add(notification);
            }

            //comment
            comment.comment_datecreated = DateTime.Now;
            comment.comment_dateedit = DateTime.Now;
            comment.comment_option = 0;
            comment.user_id = user.user_id;
            db.Comments.Add(comment);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ReplyPost([Bind(Include = "reply_post_id,reply_post_content,reply_post_datecreated,reply_post_dateedit,user_id,reply_post_activate,post_id")] Reply_Post reply_Post, Notification notification)
        {
            User user = (User)Session["user"];
            Reply_Post userReplyPost = db.Reply_Post.FirstOrDefault(n => n.user_id == user.user_id && n.post_id == reply_Post.post_id);

            //kiểm tra nếu đã trả lời rồi không cho trả lời nữa
            if (userReplyPost != null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            //user
            if (userReplyPost == null)
            {
                db.Users.Find(user.user_id).user_popular += db.Posts.Find(reply_Post.post_id).post_popular;
            }
            //Bài viết
            db.Posts.Find(reply_Post.post_id).post_sum_reply++;
            db.Posts.Find(reply_Post.post_id).post_popular++;
            //Thông báo 
            List<Show_Activate_Post> show_Activate_Posts = db.Show_Activate_Post.Where(n => n.post_id == reply_Post.post_id).ToList();
            foreach (var item in show_Activate_Posts)
            {
                if (item.user_id != user.user_id)
                {
                    notification.user_id = item.user_id;
                    notification.post_id = item.post_id;
                    notification.notification_datecreate = DateTime.Now;
                    notification.notification_content = user.user_nicename + " Đã trả lời bài viết " + db.Posts.Find(reply_Post.post_id).post_title;
                    notification.notification_status = true;
                    db.Notifications.Add(notification);
                    db.SaveChanges();
                }
            }
            // thông báo ai trả lời bài viết cho người viết bài
            var idUserPost = db.Posts.Find(reply_Post.post_id).user_id;
            if (idUserPost != user.user_id)
            {
                notification.user_id = idUserPost;
                notification.post_id = reply_Post.post_id;
                notification.notification_datecreate = DateTime.Now;
                notification.notification_content = user.user_nicename + " Đã trả lời bài viết " + db.Posts.Find(reply_Post.post_id).post_title;
                notification.notification_status = true;
                db.Notifications.Add(notification);
            }
            //Trả lời bài viết
            reply_Post.reply_post_popular = 0;
            reply_Post.reply_post__calculate_medal = 0;
            reply_Post.reply_post_datecreated = DateTime.Now;
            reply_Post.reply_post_dateedit = DateTime.Now;
            reply_Post.user_id = user.user_id;
            reply_Post.reply_post_activate = true;
            db.Reply_Post.Add(reply_Post);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}