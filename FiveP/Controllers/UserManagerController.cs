using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using FiveP.Models;

namespace FiveP.Controllers
{
    public class UserManagerController : Controller
    {
        // GET: UserManager
        FivePEntities db = new FivePEntities();
        public ActionResult IndexUserManager()
        {
            User user = (User)Session["user"];
            User users = db.Users.SingleOrDefault(n => n.user_activate_admin == true && n.user_id == user.user_id);
            
            return View(users);
        }
        [HttpPost]
        public ActionResult ChangePassword(String user_pass, String oldPass)
        {
            User user = (User)Session["user"];
            if (EncodeMD5(oldPass) != user.user_pass)
            {

                Session["notification"] = "Mật khẩu củ sai!";
                return Redirect(Request.UrlReferrer.ToString());
            }

            Session["notification"] = null;
            db.Users.Find(user.user_id).user_pass = EncodeMD5(user_pass);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        public String EncodeMD5(String pass)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
    }
}