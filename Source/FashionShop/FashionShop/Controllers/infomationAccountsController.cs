using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models;

namespace FashionShop.Controllers
{
    public class infomationAccountsController : Controller
    {
        private fashionshopEntities db = new fashionshopEntities();

        // GET: infomationAccounts
        public ActionResult Index()
        {
            if (Session["account"] != null)
            {
                var taikhoan = (account)Session["account"];
                return View(db.infomationAccounts.Where(x=>x.email.Equals(taikhoan.email)));
            }
            else
            {
                return Redirect("~/");
            }
        }


        // GET: infomationAccounts/Create
        public ActionResult Create()
        {
            if (Session["account"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/");
            }
        }

        // POST: infomationAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,email,AddressAccount,phone")] infomationAccount infomationAccount)
        {
            if (ModelState.IsValid)
            {
                if (Session["account"] != null)
                {
                    var taikhoan = (account)Session["account"];
                    infomationAccount.email = taikhoan.email;
                    db.infomationAccounts.Add(infomationAccount);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(infomationAccount);
        }

        // GET: infomationAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infomationAccount infomationAccount = db.infomationAccounts.Find(id);
            if (infomationAccount == null)
            {
                return HttpNotFound();
            }
            return View(infomationAccount);
        }

        // POST: infomationAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,email,AddressAccount,phone")] infomationAccount infomationAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(infomationAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.email = new SelectList(db.accounts, "email", "pwd", infomationAccount.email);
            return View(infomationAccount);
        }

        // GET: infomationAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infomationAccount infomationAccount = db.infomationAccounts.Find(id);
            if (infomationAccount == null)
            {
                return HttpNotFound();
            }
            return View(infomationAccount);
        }

        // POST: infomationAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            infomationAccount infomationAccount = db.infomationAccounts.Find(id);
            db.infomationAccounts.Remove(infomationAccount);
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
