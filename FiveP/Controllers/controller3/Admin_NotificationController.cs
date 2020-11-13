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
    public class Admin_NotificationController : Controller
    {
        private FivePEntities db = new FivePEntities();

        // GET: Admin_Notification
        public ActionResult Index()
        {
            return View(db.Admin_Notification.ToList());
        }

        // GET: Admin_Notification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Notification admin_Notification = db.Admin_Notification.Find(id);
            if (admin_Notification == null)
            {
                return HttpNotFound();
            }
            return View(admin_Notification);
        }

        // GET: Admin_Notification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_Notification/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "admin_notification_id,admin_notification_content,admin_notification_datecreate,admin_notification_status")] Admin_Notification admin_Notification)
        {
            if (ModelState.IsValid)
            {
                db.Admin_Notification.Add(admin_Notification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_Notification);
        }

        // GET: Admin_Notification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Notification admin_Notification = db.Admin_Notification.Find(id);
            if (admin_Notification == null)
            {
                return HttpNotFound();
            }
            return View(admin_Notification);
        }

        // POST: Admin_Notification/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "admin_notification_id,admin_notification_content,admin_notification_datecreate,admin_notification_status")] Admin_Notification admin_Notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_Notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_Notification);
        }

        // GET: Admin_Notification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Notification admin_Notification = db.Admin_Notification.Find(id);
            if (admin_Notification == null)
            {
                return HttpNotFound();
            }
            return View(admin_Notification);
        }

        // POST: Admin_Notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin_Notification admin_Notification = db.Admin_Notification.Find(id);
            db.Admin_Notification.Remove(admin_Notification);
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
