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
    public class Technology_CareController : Controller
    {
        private FivePEntities db = new FivePEntities();

        // GET: Technology_Care
        public ActionResult Index()
        {
            var technology_Care = db.Technology_Care.Include(t => t.Technology).Include(t => t.User);
            return View(technology_Care.ToList());
        }

        // GET: Technology_Care/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technology_Care technology_Care = db.Technology_Care.Find(id);
            if (technology_Care == null)
            {
                return HttpNotFound();
            }
            return View(technology_Care);
        }

        // GET: Technology_Care/Create
        public ActionResult Create()
        {
            ViewBag.technology_id = new SelectList(db.Technologies, "technology_id", "technology_name");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass");
            return View();
        }

        // POST: Technology_Care/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "technology_care_id,user_id,technology_id")] Technology_Care technology_Care)
        {
            if (ModelState.IsValid)
            {
                db.Technology_Care.Add(technology_Care);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.technology_id = new SelectList(db.Technologies, "technology_id", "technology_name", technology_Care.technology_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", technology_Care.user_id);
            return View(technology_Care);
        }

        // GET: Technology_Care/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technology_Care technology_Care = db.Technology_Care.Find(id);
            if (technology_Care == null)
            {
                return HttpNotFound();
            }
            ViewBag.technology_id = new SelectList(db.Technologies, "technology_id", "technology_name", technology_Care.technology_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", technology_Care.user_id);
            return View(technology_Care);
        }

        // POST: Technology_Care/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "technology_care_id,user_id,technology_id")] Technology_Care technology_Care)
        {
            if (ModelState.IsValid)
            {
                db.Entry(technology_Care).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.technology_id = new SelectList(db.Technologies, "technology_id", "technology_name", technology_Care.technology_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_pass", technology_Care.user_id);
            return View(technology_Care);
        }

        // GET: Technology_Care/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technology_Care technology_Care = db.Technology_Care.Find(id);
            if (technology_Care == null)
            {
                return HttpNotFound();
            }
            return View(technology_Care);
        }

        // POST: Technology_Care/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Technology_Care technology_Care = db.Technology_Care.Find(id);
            db.Technology_Care.Remove(technology_Care);
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
