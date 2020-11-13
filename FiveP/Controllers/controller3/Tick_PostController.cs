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
    public class Tick_PostController : Controller
    {
        private FivePEntities db = new FivePEntities();

        // GET: Tick_Post
        public ActionResult Index()
        {
            var tick_Post = db.Tick_Post.Include(t => t.Post).Include(t => t.User);
            return View(tick_Post.ToList());
        }

        // GET: Tick_Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tick_Post tick_Post = db.Tick_Post.Find(id);
            if (tick_Post == null)
            {
                return HttpNotFound();
            }
            return View(tick_Post);
        }

        // GET: Tick_Post/Create
        public ActionResult Create()
        {
            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass");
            return View();
        }

        // POST: Tick_Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tick_post_id,post_id,user_id,tick_post_datetime,tick_recyclebin")] Tick_Post tick_Post)
        {
            if (ModelState.IsValid)
            {
                db.Tick_Post.Add(tick_Post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content", tick_Post.post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", tick_Post.user_id);
            return View(tick_Post);
        }

        // GET: Tick_Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tick_Post tick_Post = db.Tick_Post.Find(id);
            if (tick_Post == null)
            {
                return HttpNotFound();
            }
            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content", tick_Post.post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", tick_Post.user_id);
            return View(tick_Post);
        }

        // POST: Tick_Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tick_post_id,post_id,user_id,tick_post_datetime,tick_recyclebin")] Tick_Post tick_Post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tick_Post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content", tick_Post.post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", tick_Post.user_id);
            return View(tick_Post);
        }

        // GET: Tick_Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tick_Post tick_Post = db.Tick_Post.Find(id);
            if (tick_Post == null)
            {
                return HttpNotFound();
            }
            return View(tick_Post);
        }

        // POST: Tick_Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tick_Post tick_Post = db.Tick_Post.Find(id);
            db.Tick_Post.Remove(tick_Post);
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
