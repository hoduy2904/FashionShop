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
    public class categorySecondsController : Controller
    {
        private fashionshopEntities db = new fashionshopEntities();

        // GET: Quantri/categorySeconds
        public ActionResult Index()
        {
            var categorySeconds = db.categorySeconds.Include(c => c.categoryFrist1);
            return View(categorySeconds.ToList());
        }

        // GET: Quantri/categorySeconds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categorySecond categorySecond = db.categorySeconds.Find(id);
            if (categorySecond == null)
            {
                return HttpNotFound();
            }
            return View(categorySecond);
        }

        // GET: Quantri/categorySeconds/Create
        public ActionResult Create()
        {
            ViewBag.CategoryFrist = new SelectList(db.categoryFrists, "id", "CategoryName");
            return View();
        }

        // POST: Quantri/categorySeconds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CategoryName,CategoryFrist,alias")] categorySecond categorySecond)
        {
            if (ModelState.IsValid)
            {
                db.categorySeconds.Add(categorySecond);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryFrist = new SelectList(db.categoryFrists, "id", "CategoryName", categorySecond.CategoryFrist);
            return View(categorySecond);
        }

        // GET: Quantri/categorySeconds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categorySecond categorySecond = db.categorySeconds.Find(id);
            if (categorySecond == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryFrist = new SelectList(db.categoryFrists, "id", "CategoryName", categorySecond.CategoryFrist);
            return View(categorySecond);
        }

        // POST: Quantri/categorySeconds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CategoryName,CategoryFrist,alias")] categorySecond categorySecond)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorySecond).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryFrist = new SelectList(db.categoryFrists, "id", "CategoryName", categorySecond.CategoryFrist);
            return View(categorySecond);
        }

        // GET: Quantri/categorySeconds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categorySecond categorySecond = db.categorySeconds.Find(id);
            if (categorySecond == null)
            {
                return HttpNotFound();
            }
            return View(categorySecond);
        }

        // POST: Quantri/categorySeconds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categorySecond categorySecond = db.categorySeconds.Find(id);
            db.categorySeconds.Remove(categorySecond);
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
