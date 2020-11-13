using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Controllers
{
    public class PostQuestionController : Controller
    {
        // GET: PostQuestion
        FivePEntities db = new FivePEntities();
        public ActionResult IndexPostQuestion(int? id)
        {
            if (id == null)
            {
                ViewBag.dataTechnologys = db.Technologies.ToList();
                return View();
            }
            else
            {
                ViewBag.dataTechnologys = db.Technologies.ToList();
                ViewBag.dataTags = db.Tags.Where(n => n.post_id == id).ToList();
                Post post = db.Posts.FirstOrDefault(n => n.post_id == id);
                return View(post);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult IndexPostQuestion([Bind(Include = "post_id,post_content,post_datecreated,post_dateedit,user_id,post_activate,post_activate_admin,post_title,post_sum_reply,post_sum_comment,post_view,post_popular,post_calculate_medal,post_RecycleBin")] Post post,string[] postTags, int[] postTechnology)
        {
            User user = (User)Session["user"];
            if (post.post_datecreated != null && user != null)
            {
                //công nghệ
                List<Technology_Post> technology_Posts = db.Technology_Post.Where(n => n.post_id == post.post_id).ToList();
                if(technology_Posts == null)
                {
                    foreach (var item in postTechnology)
                    {
                        Technology_Post tp = new Technology_Post()
                        {
                            post_id = post.post_id,
                            technology_id = item
                        };
                        db.Technology_Post.Add(tp);
                    }
                }
                else
                {
                    //Xóa
                    int variable = 0;
                    foreach (var item in technology_Posts)
                    {
                        foreach (var item2 in postTechnology)
                        {
                            if (item.technology_id == item2)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if (variable == 0)
                        {
                            db.Technology_Post.Remove(db.Technology_Post.Find(item.post_technology_id));
                            db.SaveChanges();
                        }
                        variable = 0;
                    }

                    //Thêm
                    List<Technology_Post> technology_Posts1 = db.Technology_Post.Where(n => n.post_id == post.post_id).ToList();
                    variable = 0;
                    foreach (var item in postTechnology)
                    {
                        foreach (var item2 in technology_Posts1)
                        {
                            if (item == item2.technology_id)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if (variable == 0)
                        {
                            Technology_Post tp = new Technology_Post()
                            {
                                post_id = post.post_id,
                                technology_id = item
                            };
                            db.Technology_Post.Add(tp);
                        }
                        variable = 0;
                    }
                }
                //tag 
                List<Tag> tags = db.Tags.Where(n => n.post_id == post.post_id).ToList();
                if (tags == null)
                {
                    foreach (var item in postTags)
                    {
                        Tag tag = new Tag()
                        {
                            tags_name = item,
                            post_id = post.post_id,
                            tags_datetime = DateTime.Now
                        };
                        db.Tags.Add(tag);
                    }
                }
                else
                {
                    //Xóa
                    int variable = 0;
                    foreach (var item in tags)
                    {
                        foreach (var item2 in postTags)
                        {
                            if (item.tags_name == item2)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if (variable == 0)
                        {
                            db.Tags.Remove(db.Tags.Find(item.tags_id));
                            db.SaveChanges();
                        }
                        variable = 0;
                    }

                    //Thêm
                    List<Tag> tags1 = db.Tags.Where(n => n.post_id == post.post_id).ToList();
                    variable = 0;
                    foreach (var item in postTags)
                    {
                        foreach (var item2 in tags1)
                        {
                            if (item == item2.tags_name)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if (variable == 0)
                        {
                            Tag tag = new Tag()
                            {
                                tags_name = item,
                                post_id = post.post_id,
                                tags_datetime = DateTime.Now
                            };
                            db.Tags.Add(tag);
                        }
                        variable = 0;
                    }
                }

                post.post_RecycleBin = false;
                post.post_datecreated = post.post_datecreated;
                post.post_dateedit = DateTime.Now;
                post.user_id = user.user_id;
                post.post_activate = post.post_activate;
                post.post_activate_admin = post.post_activate_admin;
                post.post_sum_reply = post.post_sum_reply;
                post.post_sum_comment = post.post_sum_comment;
                post.post_view = post.post_view;
                post.post_popular = post.post_popular;
                post.post_calculate_medal = post.post_calculate_medal;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Center/IndexCenter");
            }
            else if (post.post_datecreated == null && user != null)
            {
                foreach (var item in postTechnology)
                {
                    Technology_Post tp = new Technology_Post()
                    {
                        post_id = post.post_id,
                        technology_id = item
                    };
                    db.Technology_Post.Add(tp);
                }
                foreach (var item in postTags)
                {
                    Tag tag = new Tag()
                    {
                        tags_name = item,
                        post_id = post.post_id,
                        tags_datetime = DateTime.Now
                    };
                    db.Tags.Add(tag);
                }
                post.post_RecycleBin = false;
                post.post_datecreated = DateTime.Now;
                post.post_dateedit = DateTime.Now;
                post.user_id = user.user_id;
                post.post_activate = true;
                post.post_activate_admin = true;
                post.post_sum_reply = 0;
                post.post_sum_comment = 0;
                post.post_view = 0;
                post.post_popular = 0;
                post.post_calculate_medal = 0;
                db.Posts.Add(post);
                db.SaveChanges();
                return Redirect("/Center/IndexCenter");
            }
            else
            {
                Response.StatusCode = 404;
                return null;
            }
        }
    }
}