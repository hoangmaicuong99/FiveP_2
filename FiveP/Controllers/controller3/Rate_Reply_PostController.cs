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
    public class Rate_Reply_PostController : Controller
    {
        private FivePEntities db = new FivePEntities();

        // GET: Rate_Reply_Post
        public ActionResult Index()
        {
            var rate_Reply_Post = db.Rate_Reply_Post.Include(r => r.Reply_Post).Include(r => r.User);
            return View(rate_Reply_Post.ToList());
        }

        // GET: Rate_Reply_Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate_Reply_Post rate_Reply_Post = db.Rate_Reply_Post.Find(id);
            if (rate_Reply_Post == null)
            {
                return HttpNotFound();
            }
            return View(rate_Reply_Post);
        }

        // GET: Rate_Reply_Post/Create
        public ActionResult Create()
        {
            ViewBag.reply_post_id = new SelectList(db.Reply_Post, "reply_post_id", "reply_post_content");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass");
            return View();
        }

        // POST: Rate_Reply_Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rate_reply_post_id,user_id,reply_post_id,rate_reply_post1,rate_reply_post_datetime")] Rate_Reply_Post rate_Reply_Post)
        {
            if (ModelState.IsValid)
            {
                db.Rate_Reply_Post.Add(rate_Reply_Post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.reply_post_id = new SelectList(db.Reply_Post, "reply_post_id", "reply_post_content", rate_Reply_Post.reply_post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", rate_Reply_Post.user_id);
            return View(rate_Reply_Post);
        }

        // GET: Rate_Reply_Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate_Reply_Post rate_Reply_Post = db.Rate_Reply_Post.Find(id);
            if (rate_Reply_Post == null)
            {
                return HttpNotFound();
            }
            ViewBag.reply_post_id = new SelectList(db.Reply_Post, "reply_post_id", "reply_post_content", rate_Reply_Post.reply_post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", rate_Reply_Post.user_id);
            return View(rate_Reply_Post);
        }

        // POST: Rate_Reply_Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rate_reply_post_id,user_id,reply_post_id,rate_reply_post1,rate_reply_post_datetime")] Rate_Reply_Post rate_Reply_Post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rate_Reply_Post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.reply_post_id = new SelectList(db.Reply_Post, "reply_post_id", "reply_post_content", rate_Reply_Post.reply_post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", rate_Reply_Post.user_id);
            return View(rate_Reply_Post);
        }

        // GET: Rate_Reply_Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate_Reply_Post rate_Reply_Post = db.Rate_Reply_Post.Find(id);
            if (rate_Reply_Post == null)
            {
                return HttpNotFound();
            }
            return View(rate_Reply_Post);
        }

        // POST: Rate_Reply_Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rate_Reply_Post rate_Reply_Post = db.Rate_Reply_Post.Find(id);
            db.Rate_Reply_Post.Remove(rate_Reply_Post);
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
