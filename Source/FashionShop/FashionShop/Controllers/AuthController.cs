using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email,string password)
        {
            password=ThuVien.EncodeMD5(password);
            var account = db.accounts.Where(x => x.email.Trim().Equals(email) && x.pwd.Trim().Equals(password));
            if (account.Count() > 0)
            {
                var accountFirst = account.FirstOrDefault();
                if (accountFirst.isVerified.Value)
                {
                    Session["account"] = accountFirst;
                    return Redirect("~/");
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
            return View();
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
                    account.isVerified = true;
                    account.isAdmin = false;
                    account.pwd=ThuVien.EncodeMD5(account.pwd);
                    db.accounts.Add(account);
                    db.SaveChanges();
                    return Redirect("~/login");
                }
            }
            return View(account);
        }
    }
}