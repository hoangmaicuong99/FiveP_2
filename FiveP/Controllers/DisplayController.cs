using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FiveP.Models;


namespace FiveP.Controllers
{
    public class DisplayController : Controller
    {
        FivePEntities db = new FivePEntities();
        // GET: Display
        public PartialViewResult HeadCenter()
        {
            return PartialView();
        }
        public ActionResult Member(int? page)
        {
            int size = 2;
            List<User> users = db.Users.Where(n => n.user_activate == true && n.user_activate_admin == true).OrderBy(n => n.user_datecreated).ToList();

            int countUser = users.Count();
            ViewBag.demUser = countUser;
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return View(users.ToPagedList(number, size));
            }
            else if (page > (countUser / size) + 1)
            {
                page = (countUser / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(users.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(users.ToPagedList(number, size));
            }
        }
        public PartialViewResult MemberPage(int? page)
        {
            int size = 2;
            List<User> users = db.Users.Where(n => n.user_activate == true && n.user_activate_admin == true).OrderBy(n => n.user_datecreated).ToList();
            int countPost = users.Count();
            ViewBag.demCauHoi = countPost;
            System.Threading.Thread.Sleep(2000);
            if (countPost < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return PartialView(users.ToPagedList(number, size));
            }
            else if (countPost % size != 0 && page > (countPost / size) + 1)
            {
                page = (countPost / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(users.ToPagedList(number, size));
            }
            else if (countPost % size == 0 && page > (countPost / size))
            {
                page = (countPost / size);

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(users.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(users.ToPagedList(number, size));
            }
        }
        public ActionResult Technology(int? page)
        {
            int size = 2;
            List<Technology> technologies = db.Technologies.ToList();

            int countTechnology = technologies.Count();
            ViewBag.demTechnology = countTechnology;
            if (page < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return View(technologies.ToPagedList(number, size));
            }
            else if (page > (countTechnology / size) + 1)
            {
                page = (countTechnology / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(technologies.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return View(technologies.ToPagedList(number, size));
            }
        }
        public PartialViewResult TechnologyPage(int? page)
        {
            int size = 2;
            List<Technology> technologies = db.Technologies.ToList();
            int countTechnology = technologies.Count();
            ViewBag.demTechnology = countTechnology;

            System.Threading.Thread.Sleep(2000);
            if (countTechnology < 1)
            {
                page = 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;

                return PartialView(technologies.ToPagedList(number, size));
            }
            else if (countTechnology % size != 0 && page > (countTechnology / size) + 1)
            {
                page = (countTechnology / size) + 1;

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(technologies.ToPagedList(number, size));
            }
            else if (countTechnology % size == 0 && page > (countTechnology / size))
            {
                page = (countTechnology / size);

                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(technologies.ToPagedList(number, size));
            }
            else
            {
                ViewBag.size = size;
                int number = (page ?? 1);
                ViewBag.page = number;
                return PartialView(technologies.ToPagedList(number, size));
            }
        }
        public ActionResult ManagePost()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }
        public ActionResult Tick()
        {
            return View();
        }
        public ActionResult Notification()
        {
            return View();
        }
        public ActionResult Friend()
        {
            return View();
        }
    }
}