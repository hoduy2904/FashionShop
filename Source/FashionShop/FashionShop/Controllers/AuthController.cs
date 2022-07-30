using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class AuthController : Controller
    {

        fashionshopEntities db = new fashionshopEntities(); 
        // GET: Auth
        public ActionResult Login()
        {
            if (Session["account"] != null)
                return Redirect("~/");
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email,string password,string url)
        {
            password=ThuVien.EncodeMD5(password);
            var account = db.accounts.Where(x => x.email.Trim().Equals(email) && x.pwd.Trim().Equals(password));
            if (account.Count() > 0)
            {
                var accountFirst = account.FirstOrDefault();
                if (accountFirst.isVerified.Value)
                {
                    Session["account"] = accountFirst;
                    if(string.IsNullOrEmpty(url))
                    return Redirect("~/");
                    return Redirect("~/"+url);
                }
                else
                {
                    ViewBag.error = "Tài khoản chưa xác thực";
                    return View();
                }
                
            }
            else
            {
                ViewBag.error = "Sai tài khoản hoặc mật khẩu";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return Redirect("~/");
        }

        public ActionResult Register()
        {
            if (Session["account"] != null)
                return Redirect("~/");
            return View();
        }

        public bool sendmail(string ToEmail, string subject, string body)
        {
            try
            {
                string emailForm = "support@3steam.net";
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(ToEmail));
                mail.From = new MailAddress(emailForm);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                // smtp.Host = "smtp.gmail.com";
                smtp.Host = "smtp.office365.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(emailForm, "3Steam2020");
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public ActionResult Verify(string email)
        {
            var account = db.accounts.Find(email);
            if (account != null)
            {
                db.accounts.Attach(account);
                account.isVerified = true;
                db.SaveChanges();
                return Redirect("~/login");
            }
            return Redirect("~/login");
        }

        [HttpPost]
        public ActionResult Register(account account, string passwordAgain)
        {
            if (account != null)
            {
                if (!passwordAgain.Equals(account.pwd))
                {
                    ViewBag.error = "Mật khẩu không giống nhau";
                    return View(account);
                }
                var act=db.accounts.Where(x=>x.email.Trim().Equals(account.email));
                if(act.Count() > 0)
                {
                    ViewBag.error = "Email đã tồn tại";
                }
                else
                {
                    account.timecreate = DateTime.Now;
                    account.isVerified = false;
                    account.isAdmin = false;
                    account.pwd=ThuVien.EncodeMD5(account.pwd);
                    db.accounts.Add(account);
                    db.SaveChanges();
                    sendmail(account.email, "Đăng ký tài khoản Nguyễn Thuý", $"link xác thực: <a href='{Request.Url.Scheme+"://" +Request.Url.Host}/auth/verify?email={account.email}'>Xác thực</a>");
                    ViewBag.error = "Đăng ký thành công, vui lòng xác thực tài khoản qua email";
                    return Redirect("~/login");
                }
            }
            return View(account);
        }
    }
}