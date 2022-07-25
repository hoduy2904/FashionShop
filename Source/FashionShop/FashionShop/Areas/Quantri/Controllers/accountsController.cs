using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models;

namespace FashionShop.Areas.Quantri.Controllers
{
    public class accountsController : BaseController
    {
        private fashionshopEntities db = new fashionshopEntities();

        // GET: Quantri/accounts
        public ActionResult Index()
        {
            return View(db.accounts.ToList());
        }

        // GET: Quantri/accounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Quantri/accounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Quantri/accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "email,pwd,timecreate,identityCard,isVerified,isAdmin,hoTen")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                if (string.IsNullOrEmpty(account.pwd))
                {
                    db.Entry(account).Property(x => x.pwd).IsModified = false;
                }
                if (string.IsNullOrEmpty(account.identityCard))
                {
                    db.Entry(account).Property(x => x.identityCard).IsModified = false;
                }

                if (string.IsNullOrEmpty(account.hoTen))
                {
                    db.Entry(account).Property(x => x.hoTen).IsModified = false;
                }
                db.Entry(account).Property(x => x.timecreate).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Quantri/accounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Quantri/accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            account account = db.accounts.Find(id);
            db.accounts.Remove(account);
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
