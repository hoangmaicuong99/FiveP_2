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
    public class Rate_PostController : Controller
    {
        private FivePEntities db = new FivePEntities();

        // GET: Rate_Post
        public ActionResult Index()
        {
            var rate_Post = db.Rate_Post.Include(r => r.Post).Include(r => r.User);
            return View(rate_Post.ToList());
        }

        // GET: Rate_Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate_Post rate_Post = db.Rate_Post.Find(id);
            if (rate_Post == null)
            {
                return HttpNotFound();
            }
            return View(rate_Post);
        }

        // GET: Rate_Post/Create
        public ActionResult Create()
        {
            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass");
            return View();
        }

        // POST: Rate_Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rate_post_id,user_id,post_id,rate_post1,rate_post_datetime")] Rate_Post rate_Post)
        {
            if (ModelState.IsValid)
            {
                db.Rate_Post.Add(rate_Post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content", rate_Post.post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", rate_Post.user_id);
            return View(rate_Post);
        }

        // GET: Rate_Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate_Post rate_Post = db.Rate_Post.Find(id);
            if (rate_Post == null)
            {
                return HttpNotFound();
            }
            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content", rate_Post.post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", rate_Post.user_id);
            return View(rate_Post);
        }

        // POST: Rate_Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rate_post_id,user_id,post_id,rate_post1,rate_post_datetime")] Rate_Post rate_Post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rate_Post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content", rate_Post.post_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", rate_Post.user_id);
            return View(rate_Post);
        }

        // GET: Rate_Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate_Post rate_Post = db.Rate_Post.Find(id);
            if (rate_Post == null)
            {
                return HttpNotFound();
            }
            return View(rate_Post);
        }

        // POST: Rate_Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rate_Post rate_Post = db.Rate_Post.Find(id);
            db.Rate_Post.Remove(rate_Post);
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
