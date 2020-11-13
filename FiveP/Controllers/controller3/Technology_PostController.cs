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
    public class Technology_PostController : Controller
    {
        private FivePEntities db = new FivePEntities();

        // GET: Technology_Post
        public ActionResult Index()
        {
            var technology_Post = db.Technology_Post.Include(t => t.Post).Include(t => t.Technology);
            return View(technology_Post.ToList());
        }

        // GET: Technology_Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technology_Post technology_Post = db.Technology_Post.Find(id);
            if (technology_Post == null)
            {
                return HttpNotFound();
            }
            return View(technology_Post);
        }

        // GET: Technology_Post/Create
        public ActionResult Create()
        {
            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content");
            ViewBag.technology_id = new SelectList(db.Technologies, "technology_id", "technology_name");
            return View();
        }

        // POST: Technology_Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "post_technology_id,post_id,technology_id")] Technology_Post technology_Post)
        {
            if (ModelState.IsValid)
            {
                db.Technology_Post.Add(technology_Post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content", technology_Post.post_id);
            ViewBag.technology_id = new SelectList(db.Technologies, "technology_id", "technology_name", technology_Post.technology_id);
            return View(technology_Post);
        }

        // GET: Technology_Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technology_Post technology_Post = db.Technology_Post.Find(id);
            if (technology_Post == null)
            {
                return HttpNotFound();
            }
            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content", technology_Post.post_id);
            ViewBag.technology_id = new SelectList(db.Technologies, "technology_id", "technology_name", technology_Post.technology_id);
            return View(technology_Post);
        }

        // POST: Technology_Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "post_technology_id,post_id,technology_id")] Technology_Post technology_Post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(technology_Post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.post_id = new SelectList(db.Posts, "post_id", "post_content", technology_Post.post_id);
            ViewBag.technology_id = new SelectList(db.Technologies, "technology_id", "technology_name", technology_Post.technology_id);
            return View(technology_Post);
        }

        // GET: Technology_Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technology_Post technology_Post = db.Technology_Post.Find(id);
            if (technology_Post == null)
            {
                return HttpNotFound();
            }
            return View(technology_Post);
        }

        // POST: Technology_Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Technology_Post technology_Post = db.Technology_Post.Find(id);
            db.Technology_Post.Remove(technology_Post);
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
