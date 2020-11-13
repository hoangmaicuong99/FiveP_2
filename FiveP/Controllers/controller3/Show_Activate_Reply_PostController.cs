using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Controllers.controller3
{
    public class Show_Activate_Reply_PostController : Controller
    {
        private FivePEntities db = new FivePEntities();

        // GET: Show_Activate_Reply_Post
        public ActionResult Index()
        {
            var show_Activate_Reply_Post = db.Show_Activate_Reply_Post.Include(s => s.Reply_Post).Include(s => s.User);
            return View(show_Activate_Reply_Post.ToList());
        }

        // GET: Show_Activate_Reply_Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show_Activate_Reply_Post show_Activate_Reply_Post = db.Show_Activate_Reply_Post.Find(id);
            if (show_Activate_Reply_Post == null)
            {
                return HttpNotFound();
            }
            return View(show_Activate_Reply_Post);
        }

        // GET: Show_Activate_Reply_Post/Create
        public ActionResult Create()
        {
            ViewBag.reply_post_id = new SelectList(db.Reply_Post, "reply_post_id", "reply_post_content");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass");
            return View();
        }

        // POST: Show_Activate_Reply_Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "show_activate_reply_post_id,reply_post_id,user_id,show_activate_reply_post_datetime,show_activate_reply_post_readed")] Show_Activate_Reply_Post show_Activate_Reply_Post)
        {
            if (ModelState.IsValid)
            {
                db.Show_Activate_Reply_Post.Add(show_Activate_Reply_Post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.reply_post_id = new SelectList(db.Reply_Post, "reply_post_id", "reply_post_content", show_Activate_Reply_Post.reply_post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", show_Activate_Reply_Post.user_id);
            return View(show_Activate_Reply_Post);
        }

        // GET: Show_Activate_Reply_Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show_Activate_Reply_Post show_Activate_Reply_Post = db.Show_Activate_Reply_Post.Find(id);
            if (show_Activate_Reply_Post == null)
            {
                return HttpNotFound();
            }
            ViewBag.reply_post_id = new SelectList(db.Reply_Post, "reply_post_id", "reply_post_content", show_Activate_Reply_Post.reply_post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", show_Activate_Reply_Post.user_id);
            return View(show_Activate_Reply_Post);
        }

        // POST: Show_Activate_Reply_Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "show_activate_reply_post_id,reply_post_id,user_id,show_activate_reply_post_datetime,show_activate_reply_post_readed")] Show_Activate_Reply_Post show_Activate_Reply_Post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(show_Activate_Reply_Post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.reply_post_id = new SelectList(db.Reply_Post, "reply_post_id", "reply_post_content", show_Activate_Reply_Post.reply_post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", show_Activate_Reply_Post.user_id);
            return View(show_Activate_Reply_Post);
        }

        // GET: Show_Activate_Reply_Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show_Activate_Reply_Post show_Activate_Reply_Post = db.Show_Activate_Reply_Post.Find(id);
            if (show_Activate_Reply_Post == null)
            {
                return HttpNotFound();
            }
            return View(show_Activate_Reply_Post);
        }

        // POST: Show_Activate_Reply_Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Show_Activate_Reply_Post show_Activate_Reply_Post = db.Show_Activate_Reply_Post.Find(id);
            db.Show_Activate_Reply_Post.Remove(show_Activate_Reply_Post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
