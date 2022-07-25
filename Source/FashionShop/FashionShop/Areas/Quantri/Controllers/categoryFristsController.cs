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
    public class categoryFristsController : BaseController
    {
        private fashionshopEntities db = new fashionshopEntities();

        // GET: Quantri/categoryFrists
        public ActionResult Index()
        {
            return View(db.categoryFrists.ToList());
        }

        // GET: Quantri/categoryFrists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoryFrist categoryFrist = db.categoryFrists.Find(id);
            if (categoryFrist == null)
            {
                return HttpNotFound();
            }
            return View(categoryFrist);
        }

        // GET: Quantri/categoryFrists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quantri/categoryFrists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CategoryName,alias")] categoryFrist categoryFrist)
        {
            if (ModelState.IsValid)
            {
                categoryFrist.alias = ThuVien.convertToUnSign3(categoryFrist.CategoryName);
                db.categoryFrists.Add(categoryFrist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryFrist);
        }

        // GET: Quantri/categoryFrists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoryFrist categoryFrist = db.categoryFrists.Find(id);
            if (categoryFrist == null)
            {
                return HttpNotFound();
            }
            return View(categoryFrist);
        }

        // POST: Quantri/categoryFrists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CategoryName,alias")] categoryFrist categoryFrist)
        {
            if (ModelState.IsValid)
            {
                categoryFrist.alias = ThuVien.convertToUnSign3(categoryFrist.CategoryName);
                db.Entry(categoryFrist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryFrist);
        }

        // GET: Quantri/categoryFrists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoryFrist categoryFrist = db.categoryFrists.Find(id);
            if (categoryFrist == null)
            {
                return HttpNotFound();
            }
            return View(categoryFrist);
        }

        // POST: Quantri/categoryFrists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryFrist categoryFrist = db.categoryFrists.Find(id);
            db.categoryFrists.Remove(categoryFrist);
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
