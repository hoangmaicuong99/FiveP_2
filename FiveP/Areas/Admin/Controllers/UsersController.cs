using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        // GET: Admin/Users
        private FivePEntities db = new FivePEntities();
        public ActionResult Index()
        {
            return View(db.Users.OrderByDescending(n=>n.user_datelogin).ToList());
        }
        public ActionResult TechnologyCare()
        {
            return View(db.Users.ToList());
        }
        public ActionResult Friend()
        {
            return View(db.Friends.ToList());
        }
        [HttpPost]
        public ActionResult AddUser([Bind(Include = "user_id,user_pass,user_nicename,user_email,user_datecreated,user_token,user_role,user_datelogin,user_activate,user_address,user_img,user_sex,user_link_facebok,user_link_github,user_hobby_work,user_hobby,user_activate_admin,user_date_born,user_popular,user_gold_medal,user_silver_medal,user_bronze_medal,user_vip_medal,provincial_id,district_id,commune_id,user_phone")] User user)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(user.user_pass));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            user.user_pass = strBuilder.ToString();

            user.user_nicename = null;
            user.user_datecreated = DateTime.Now;
            user.user_token = Guid.NewGuid().ToString();
            user.user_role = 0;
            user.user_datelogin = DateTime.Now;
            user.user_activate = true;
            user.user_phone = "0";
            user.user_address = null;
            user.user_img = null;
            user.user_sex = null;
            user.user_link_facebok = null;
            user.user_link_github = null;
            user.user_hobby_work = null;
            user.user_hobby = null;
            user.user_activate_admin = true;
            user.user_date_born = null;
            user.user_popular = 0;
            user.user_gold_medal = 0;
            user.user_silver_medal = 0;
            user.user_bronze_medal = 0;
            user.user_vip_medal = 0;
            user.provincial_id = null;
            user.district_id = null;
            user.commune_id = null;
            db.Users.Add(user);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());

        }
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

        // lọc theo các trường
        public PartialViewResult MostFamous()
        {
            return PartialView(db.Users.OrderByDescending(n=>n.user_popular).ToList());
        }
        public PartialViewResult LessFamous()
        {
            return PartialView(db.Users.OrderBy(n => n.user_popular).ToList());
        }
        public PartialViewResult ManyVipMedals()
        {
            return PartialView(db.Users.OrderByDescending(n => n.user_vip_medal).ToList());
        }
        public PartialViewResult LeastVipMedals()
        {
            return PartialView(db.Users.OrderBy(n => n.user_vip_medal).ToList());
        }
        public PartialViewResult ManyGoldMedals()
        {
            return PartialView(db.Users.OrderByDescending(n => n.user_gold_medal).ToList());
        }
        public PartialViewResult LeastGoldMedals()
        {
            return PartialView(db.Users.OrderBy(n => n.user_gold_medal).ToList());
        }
        public PartialViewResult ManySilverMedals()
        {
            return PartialView(db.Users.OrderByDescending(n => n.user_silver_medal).ToList());
        }
        public PartialViewResult LeastSilverMedals()
        {
            return PartialView(db.Users.OrderBy(n => n.user_silver_medal).ToList());
        }
        public PartialViewResult ManyBronzeMedals()
        {
            return PartialView(db.Users.OrderByDescending(n => n.user_bronze_medal).ToList());
        }
        public PartialViewResult LeastBronzeMedals()
        {
            return PartialView(db.Users.OrderBy(n => n.user_bronze_medal).ToList());
        }
    }
}