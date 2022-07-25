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
    public class bannersController : BaseController
    {
        private fashionshopEntities db = new fashionshopEntities();

        // GET: Quantri/banners
        public ActionResult Index()
        {
            return View(db.banners.ToList());
        }

        // GET: Quantri/banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banner banner = db.banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Quantri/banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quantri/banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,imgsrc")] banner banner, HttpPostedFileBase fileUpload)
        {
            string path = Server.MapPath("~/Content/upload/images/");
            if (ModelState.IsValid)
            {
                if (fileUpload!=null&& fileUpload.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(fileUpload.FileName);
                    string finalFileName = DateTime.Now.Ticks.ToString() + extensionName;
                    // now save the selected file with new name
                    fileUpload.SaveAs(path + finalFileName);
                    banner.imgsrc = "/Content/upload/images/" + finalFileName;
                }
                db.banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: Quantri/banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banner banner = db.banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Quantri/banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,imgsrc")] banner banner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: Quantri/banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banner banner = db.banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Quantri/banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            banner banner = db.banners.Find(id);
            db.banners.Remove(banner);
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
