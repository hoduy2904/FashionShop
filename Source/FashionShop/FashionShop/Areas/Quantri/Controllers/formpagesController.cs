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
    public class formpagesController : Controller
    {
        private fashionshopEntities db = new fashionshopEntities();

        // GET: Quantri/formpages
        public ActionResult Index()
        {
            return View(db.formpages.ToList());
        }

        // GET: Quantri/formpages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quantri/formpages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,noidung,alias")] formpage formpage)
        {
            if (ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(formpage.alias))
                formpage.alias = ThuVien.convertToUnSign3(formpage.title);
                db.formpages.Add(formpage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(formpage);
        }

        // GET: Quantri/formpages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formpage formpage = db.formpages.Find(id);
            if (formpage == null)
            {
                return HttpNotFound();
            }
            return View(formpage);
        }

        // POST: Quantri/formpages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,title,noidung,alias")] formpage formpage)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(formpage.alias))
                    formpage.alias = ThuVien.convertToUnSign3(formpage.title);
                db.Entry(formpage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formpage);
        }

        // GET: Quantri/formpages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formpage formpage = db.formpages.Find(id);
            if (formpage == null)
            {
                return HttpNotFound();
            }
            return View(formpage);
        }

        // POST: Quantri/formpages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            formpage formpage = db.formpages.Find(id);
            db.formpages.Remove(formpage);
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
