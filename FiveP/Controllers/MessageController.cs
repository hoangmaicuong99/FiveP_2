using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        FivePEntities db = new FivePEntities();
        public ActionResult Index(int? id)
        {
            return View();
        }
        public PartialViewResult BoxFiveP(string searchText)
        {
            System.Threading.Thread.Sleep(1000);
            Post post = db.Posts.FirstOrDefault(n => n.post_title.Contains(searchText));
            return PartialView(post);
        }
        public PartialViewResult Message(int? id)
        {
            ViewBag.id = id;
            User user = (User)Session["user"];
            List<Message> messages = db.Messages.Where(n => (n.user_id == user.user_id && n.user_friend_id == id) || (n.user_friend_id == user.user_id && n.user_id == id)).OrderBy(n => n.message_datetime).Take(10).ToList();
            return PartialView(messages);
        }
        [HttpPost]
        public ActionResult SaveMessage(Message message)
        {
            User user = (User)Session["user"];

            message.user_id = user.user_id;
            message.message_datetime = DateTime.Now;
            db.Messages.Add(message);
            db.SaveChanges();
            return View();
        }
        public ActionResult MessageTest(int? id)
        {
            ViewBag.id = id;
            User user = (User)Session["user"];
            List<Message> messages = db.Messages.Where(n => (n.user_id == user.user_id && n.user_friend_id == id) || (n.user_friend_id == user.user_id && n.user_id == id)).OrderByDescending(n => n.message_datetime).Take(10).ToList();
            return View(messages);
        }
        [HttpPost]
        public ActionResult SaveMessageUserVsBos(MessageUserVSBox messageUserVSBox,string content)
        {
            User user = (User)Session["user"];
            messageUserVSBox.content = content;
            messageUserVSBox.datecreate = DateTime.Now;
            messageUserVSBox.user_id = user.user_id;
            db.MessageUserVSBoxes.Add(messageUserVSBox);
            db.SaveChanges();
            return View();
        }
    }
}