using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using FiveP.Models;

namespace FiveP.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        FivePEntities db = new FivePEntities();
        String AllQuestion = "/Center/IndexCenter";
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login( FormCollection f)
        {
            String sEmail = f["user_email"].ToString();
            String sPass = f["user_pass"].ToString();

            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(sPass));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            String pass_encode = strBuilder.ToString();

            User user = db.Users.Where(n => n.user_activate == true && n.user_role == 0 && n.user_activate_admin == true).SingleOrDefault(n => n.user_email == sEmail && n.user_pass == pass_encode);
            if (user != null)
            {
                Session["user"] = user;
                db.Users.Find(user.user_id).user_datelogin = DateTime.Now;
                db.Users.Find(user.user_id).user_token = Guid.NewGuid().ToString();
                db.SaveChanges();
                Session["notLogin"] = null;
                return Redirect(AllQuestion);
            }
            else
            {
                User user1 = db.Users.Where(n => n.user_activate == true && n.user_role == 1).SingleOrDefault(n => n.user_email == sEmail && n.user_pass == pass_encode);
                if(user1 != null)
                {
                    Session["admin"] = user1;
                    db.Users.Find(user1.user_id).user_datelogin = DateTime.Now;
                    db.Users.Find(user1.user_id).user_token = Guid.NewGuid().ToString();
                    db.SaveChanges();
                    Session["notLogin"] = null;
                    return Redirect("/Admin/HomeAdmin/Index");
                }

                Session["notLogin"] = "<p class='remember' style='color:#721c24; background-color:#f8d7da; border-color:#f5c6cb; padding: .75rem 1.25rem; border: 1px solid transparent; border-radius: .25rem; line-height: 1.5;'>Sai tài khoản hoặc mật khẩu</p>";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
        public ActionResult LogOut()
        {
            Session["user"] = null;
            return Redirect(AllQuestion);
        }
        
        
        //Đăng ký thông tin tài khoản
        public ActionResult RegisterPersonalInformation()
        {
            User users = (User)Session["user"];
            User users2 = db.Users.Find(users.user_id);
            return View(users2);
        }
        [HttpPost]
        public ActionResult RegisterPersonalInformation([Bind(Include = "user_id,user_pass,user_nicename,user_email,user_datecreated,user_token,user_role,user_datelogin,user_activate,user_address,user_img,user_sex,user_link_facebok,user_link_github,user_hobby_work,user_hobby,user_activate_admin,user_date_born,user_popular,user_gold_medal,user_silver_medal,user_bronze_medal,user_vip_medal,provincial_id,district_id,commune_id,user_phone")] User user, int[] tagsTechnology, HttpPostedFileBase fileImg)
        {
            User user1 = (User)Session["user"];
            if(fileImg == null)
            {
                List<Technology_Care> technology_Cares = db.Technology_Care.Where(n => n.user_id == user1.user_id).ToList();
                if(technology_Cares == null)
                {
                    foreach (var item in tagsTechnology)
                    {
                        Technology_Care tag = new Technology_Care()
                        {
                            technology_id = item,
                            user_id = user1.user_id,
                        };
                        db.Technology_Care.Add(tag);
                        db.SaveChanges();
                    }
                }
                else
                {
                    int variable = 0;
                    foreach (var item in technology_Cares)
                    {
                        foreach (var item2 in tagsTechnology)
                        {
                            if (item.technology_id == item2)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if(variable == 0)
                        {
                            db.Technology_Care.Remove(db.Technology_Care.Find(item.technology_care_id));
                            db.SaveChanges();
                        }
                        variable = 0;
                    }

                    List<Technology_Care> technology_Care2 = db.Technology_Care.Where(n => n.user_id == user1.user_id).ToList();
                    variable = 0;
                    foreach(var item in tagsTechnology)
                    {
                        foreach (var item2 in technology_Care2)
                        {
                            if(item == item2.technology_id)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if(variable == 0)
                        {
                            Technology_Care tag = new Technology_Care()
                            {
                                technology_id = item,
                                user_id = user1.user_id,
                            };
                            db.Technology_Care.Add(tag);
                        }
                        variable = 0;
                    }
                }
                

                user.user_img = user1.user_img;
                user.user_id = user1.user_id;
                user.user_email = user1.user_email;
                user.user_pass = user1.user_pass;
                user.user_datecreated = user1.user_datecreated;
                user.user_token = user1.user_token;
                user.user_role = user1.user_role;
                user.user_datelogin = user1.user_datelogin;
                user.user_activate = user1.user_activate;
                user.user_activate_admin = user1.user_activate_admin;
                user.user_popular = user1.user_popular;
                user.user_bronze_medal = user1.user_bronze_medal;
                user.user_gold_medal = user1.user_gold_medal;
                user.user_silver_medal = user1.user_silver_medal;
                user.user_vip_medal = user1.user_vip_medal;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(AllQuestion);
            }
            var varFileImg = Path.GetFileName(fileImg.FileName);
            //Lưu file
            var pa = Path.Combine(Server.MapPath("~/Content/LayoutCenter/ImgUser"), varFileImg);
            if (System.IO.File.Exists(pa))
            {
                Random random = new Random();
                var varFileImg2 = Path.GetFileName(random.Next() + fileImg.FileName);
                var pa2 = Path.Combine(Server.MapPath("~/Content/LayoutCenter/ImgUser"), varFileImg2);
                
                fileImg.SaveAs(pa2);

                List<Technology_Care> technology_Cares = db.Technology_Care.Where(n => n.user_id == user1.user_id).ToList();
                if (technology_Cares == null)
                {
                    foreach (var item in tagsTechnology)
                    {
                        Technology_Care tag = new Technology_Care()
                        {
                            technology_id = item,
                            user_id = user1.user_id,
                        };
                        db.Technology_Care.Add(tag);
                        db.SaveChanges();
                    }
                }
                else
                {
                    int variable = 0;
                    foreach (var item in technology_Cares)
                    {
                        foreach (var item2 in tagsTechnology)
                        {
                            if (item.technology_id == item2)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if (variable == 0)
                        {
                            db.Technology_Care.Remove(db.Technology_Care.Find(item.technology_care_id));
                            db.SaveChanges();
                        }
                        variable = 0;
                    }
                    variable = 0;
                    foreach (var item in tagsTechnology)
                    {
                        foreach (var item2 in technology_Cares)
                        {
                            if (item == item2.technology_care_id)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if (variable == 0)
                        {
                            Technology_Care tag = new Technology_Care()
                            {
                                technology_id = item,
                                user_id = user1.user_id,
                            };
                            db.Technology_Care.Add(tag);
                            db.SaveChanges();
                        }
                        variable = 0;
                    }
                }

                user.user_img = random.Next() + fileImg.FileName;
                user.user_id = user1.user_id;
                user.user_email = user1.user_email;
                user.user_pass = user1.user_pass;
                user.user_datecreated = user1.user_datecreated;
                user.user_token = user1.user_token;
                user.user_role = user1.user_role;
                user.user_datelogin = user1.user_datelogin;
                user.user_activate = user1.user_activate;
                user.user_activate_admin = user1.user_activate_admin;
                user.user_popular = user1.user_popular;
                user.user_bronze_medal = user1.user_bronze_medal;
                user.user_gold_medal = user1.user_gold_medal;
                user.user_silver_medal = user1.user_silver_medal;
                user.user_vip_medal = user1.user_vip_medal;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(AllQuestion);

            }
            else
            {
                List<Technology_Care> technology_Cares = db.Technology_Care.Where(n => n.user_id == user1.user_id).ToList();
                if (technology_Cares == null)
                {
                    foreach (var item in tagsTechnology)
                    {
                        Technology_Care tag = new Technology_Care()
                        {
                            technology_id = item,
                            user_id = user1.user_id,
                        };
                        db.Technology_Care.Add(tag);
                        db.SaveChanges();
                    }
                }
                else
                {
                    int variable = 0;
                    foreach (var item in technology_Cares)
                    {
                        foreach (var item2 in tagsTechnology)
                        {
                            if (item.technology_id == item2)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if (variable == 0)
                        {
                            db.Technology_Care.Remove(db.Technology_Care.Find(item.technology_care_id));
                            db.SaveChanges();
                        }
                        variable = 0;
                    }
                    variable = 0;
                    foreach (var item in tagsTechnology)
                    {
                        foreach (var item2 in technology_Cares)
                        {
                            if (item == item2.technology_care_id)
                            {
                                variable = 1;
                                break;
                            }
                        }
                        if (variable == 0)
                        {
                            Technology_Care tag = new Technology_Care()
                            {
                                technology_id = item,
                                user_id = user1.user_id,
                            };
                            db.Technology_Care.Add(tag);
                            db.SaveChanges();
                        }
                        variable = 0;
                    }
                }

                fileImg.SaveAs(pa);
                user.user_img = fileImg.FileName;
                user.user_id = user1.user_id;
                user.user_email = user1.user_email;
                user.user_pass = user1.user_pass;
                user.user_datecreated = user1.user_datecreated;
                user.user_token = user1.user_token;
                user.user_role = user1.user_role;
                user.user_datelogin = user1.user_datelogin;
                user.user_activate = user1.user_activate;
                user.user_activate_admin = user1.user_activate_admin;
                user.user_popular = user1.user_popular;
                user.user_bronze_medal = user1.user_bronze_medal;
                user.user_gold_medal = user1.user_gold_medal;
                user.user_silver_medal = user1.user_silver_medal;
                user.user_vip_medal = user1.user_vip_medal;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(AllQuestion);
            }
            
        }

        //Lọc địa chỉ
        public PartialViewResult District(int? id)
        {
            List<District> dsHuyen = new List<District>();
            if (id == null)
            {
                dsHuyen = db.Districts.ToList();
            }
            else
            {
                dsHuyen = db.Districts.Where(n => n.provincial_id == id).ToList();
            }
            return PartialView(dsHuyen);
        }
        public PartialViewResult Commune(int? id)
        {
            List<Commune> dsXa = new List<Commune>();
            if (id == null)
            {
                return PartialView();

            }
            else
            {
                dsXa = db.Communes.Where(n => n.district_id == id).ToList();
            }
            return PartialView(dsXa);
        }

        // Đăng ký tài khoản
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind(Include = "user_id,user_pass,user_nicename,user_email,user_datecreated,user_token,user_role,user_datelogin,user_activate,user_address,user_img,user_sex,user_link_facebok,user_link_github,user_hobby_work,user_hobby,user_activate_admin,user_date_born,user_popular,user_gold_medal,user_silver_medal,user_bronze_medal,user_vip_medal,provincial_id,district_id,commune_id,user_phone")] User user, ConfirmGmail confirmGmail)
        {
            User ruser = db.Users.SingleOrDefault(n => n.user_email == user.user_email);

            if(ruser == null)
            {

                try
                {
                    WebMail.SmtpServer = "smtp.gmail.com";//Máy chủ gmail.
                    WebMail.SmtpPort = 587; // Cổng
                    WebMail.SmtpUseDefaultCredentials = true;
                    //Gửi gmail với giao thức bảo mật.
                    WebMail.EnableSsl = true;
                    //Tài khoản dùng để đăng nhập vào gmail để gửi.
                    WebMail.UserName = "cuongembaubang@gmail.com";
                    WebMail.Password = "trung2010203";
                    // Nội dung gửi.
                    WebMail.From = "cuongembaubang@gmail.com";

                    Random random = new Random();

                    confirmGmail.pass = user.user_pass;
                    confirmGmail.strEmailReceived = user.user_email;
                    confirmGmail.strTitle = "Mã xác nhận : ";
                    confirmGmail.strContent = random.Next(1000,9999).ToString();
                    Session["confirmemail"] = confirmGmail;

                    //Gửi gmail.
                    WebMail.Send(to: confirmGmail.strEmailReceived, subject: confirmGmail.strTitle, body: confirmGmail.strContent, isBodyHtml: true);
                    ViewBag.thongbao = "Gmail được gửi thành công";
                    return Redirect("/Account/ConfirmEmail");
                }
                catch (Exception)
                {
                    ViewBag.notification = "Không gửi được email";
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            else
            {
                Session["NotRegistration"] = "<p class='remember' style='color:#721c24; background-color:#f8d7da; border-color:#f5c6cb; padding: .75rem 1.25rem; border: 1px solid transparent; border-radius: .25rem; line-height: 1.5;'>Emai này đã được đăng ký !</p> ";
                return Redirect(Request.UrlReferrer.ToString());
            }

            
        }



        public ActionResult ConfirmEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmEmail(User user, string code)
        {
            ConfirmGmail confirmGmail = (ConfirmGmail)Session["confirmemail"];


            if( confirmGmail.strContent == code)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(confirmGmail.pass));

                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }

                user.user_pass = strBuilder.ToString();

                user.user_email = confirmGmail.strEmailReceived;

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
                Session["user"] = user;
                Session["NotRegistration"] = null;
                return RedirectToAction("RegisterPersonalInformation");
            }
            else
            {
                Session["notification"] = "Sai mã xác nhận";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            User user = db.Users.SingleOrDefault(n => n.user_email == forgotPassword.EmailReceived);
            if(user == null)
            {
                Session["Notification"] = "Email không tồn tại";
                return Redirect(Request.UrlReferrer.ToString());
            }
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";//Máy chủ gmail.
                WebMail.SmtpPort = 587; // Cổng
                WebMail.SmtpUseDefaultCredentials = true;
                //Gửi gmail với giao thức bảo mật.
                WebMail.EnableSsl = true;
                //Tài khoản dùng để đăng nhập vào gmail để gửi.
                WebMail.UserName = "cuongembaubang@gmail.com";
                WebMail.Password = "trung2010203";
                // Nội dung gửi.
                WebMail.From = "cuongembaubang@gmail.com";
                forgotPassword.Title = "Xác nhận mật khẩu Web ";
                forgotPassword.Content = "Xác Nhận: https://localhost:44351/Account/ChangePassword?id=" + user.user_id + "&Token=" + user.user_token;
                //Gửi gmail.
                WebMail.Send(to: forgotPassword.EmailReceived, subject: forgotPassword.Title, body: forgotPassword.Content, isBodyHtml: true);
                Session["Notification"] = "Gmail được gửi thành công";
                return Redirect(Request.UrlReferrer.ToString());

            }
            catch (Exception)
            {
                Session["Notification"] = "mất mạng";
                return Redirect(Request.UrlReferrer.ToString());

            }
        }
        public ActionResult ChangePassword(int? id, string Token)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (Token != user.user_token)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["Notification"] = null;
            return View(user);
        }
        [HttpPost]
        public ActionResult ChangePassword(String NewPassword, int? id)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(NewPassword));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            db.Users.Find(id).user_pass = strBuilder.ToString();


            db.Users.Find(id).user_token = Guid.NewGuid().ToString();
            db.SaveChanges();
            return Redirect("/Center/IndexCenter");
        }


    }
}