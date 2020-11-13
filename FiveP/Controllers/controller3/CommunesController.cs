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
    public class CommunesController : Controller
    {
        private FivePEntities db = new FivePEntities();

        // GET: Communes
        public ActionResult Index()
        {
            var communes = db.Communes.Include(c => c.District);
            return View(communes.ToList());
        }

        // GET: Communes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commune commune = db.Communes.Find(id);
            if (commune == null)
            {
                return HttpNotFound();
            }
            return View(commune);
        }

        // GET: Communes/Create
        public ActionResult Create()
        {
            ViewBag.district_id = new SelectList(db.Districts, "district_id", "district_name");
            return View();
        }

        // POST: Communes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commune_id,commune_name,commune_activate,commune_date,district_id")] Commune commune)
        {
            if (ModelState.IsValid)
            {
                db.Communes.Add(commune);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.district_id = new SelectList(db.Districts, "district_id", "district_name", commune.district_id);
            return View(commune);
        }

        // GET: Communes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commune commune = db.Communes.Find(id);
            if (commune == null)
            {
                return HttpNotFound();
            }
            ViewBag.district_id = new SelectList(db.Districts, "district_id", "district_name", commune.district_id);
            return View(commune);
        }

        // POST: Communes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commune_id,commune_name,commune_activate,commune_date,district_id")] Commune commune)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commune).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.district_id = new SelectList(db.Districts, "district_id", "district_name", commune.district_id);
            return View(commune);
        }

        // GET: Communes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commune commune = db.Communes.Find(id);
            if (commune == null)
            {
                return HttpNotFound();
            }
            return View(commune);
        }

        // POST: Communes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commune commune = db.Communes.Find(id);
            db.Communes.Remove(commune);
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
