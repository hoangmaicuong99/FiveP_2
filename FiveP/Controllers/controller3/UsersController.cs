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
    public class UsersController : Controller
    {
        private FivePEntities db = new FivePEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Commune).Include(u => u.District).Include(u => u.Provincial);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.commune_id = new SelectList(db.Communes, "commune_id", "commune_name");
            ViewBag.district_id = new SelectList(db.Districts, "district_id", "district_name");
            ViewBag.provincial_id = new SelectList(db.Provincials, "provincial_id", "provincial_name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,user_pass,user_nicename,user_email,user_datecreated,user_token,user_role,user_datelogin,user_activate,user_address,user_img,user_sex,user_link_facebok,user_link_github,user_hobby_work,user_hobby,user_activate_admin,user_date_born,user_popular,user_gold_medal,user_silver_medal,user_bronze_medal,user_vip_medal,provincial_id,district_id,commune_id,user_phone")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.commune_id = new SelectList(db.Communes, "commune_id", "commune_name", user.commune_id);
            ViewBag.district_id = new SelectList(db.Districts, "district_id", "district_name", user.district_id);
            ViewBag.provincial_id = new SelectList(db.Provincials, "provincial_id", "provincial_name", user.provincial_id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.commune_id = new SelectList(db.Communes, "commune_id", "commune_name", user.commune_id);
            ViewBag.district_id = new SelectList(db.Districts, "district_id", "district_name", user.district_id);
            ViewBag.provincial_id = new SelectList(db.Provincials, "provincial_id", "provincial_name", user.provincial_id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,user_pass,user_nicename,user_email,user_datecreated,user_token,user_role,user_datelogin,user_activate,user_address,user_img,user_sex,user_link_facebok,user_link_github,user_hobby_work,user_hobby,user_activate_admin,user_date_born,user_popular,user_gold_medal,user_silver_medal,user_bronze_medal,user_vip_medal,provincial_id,district_id,commune_id,user_phone")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.commune_id = new SelectList(db.Communes, "commune_id", "commune_name", user.commune_id);
            ViewBag.district_id = new SelectList(db.Districts, "district_id", "district_name", user.district_id);
            ViewBag.provincial_id = new SelectList(db.Provincials, "provincial_id", "provincial_name", user.provincial_id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
