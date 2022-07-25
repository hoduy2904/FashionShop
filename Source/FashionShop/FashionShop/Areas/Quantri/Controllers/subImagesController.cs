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
    public class subImagesController : BaseController
    {
        private fashionshopEntities db = new fashionshopEntities();

        // GET: Quantri/subImages
        public ActionResult Index(string productID)
        {
            try {
                var subImages = db.subImages.Where(x => x.productID.Equals(productID)).Include(s => s.Product);
                return View(subImages.ToList());
            }
            catch
            {
                return Redirect("~/quantri");
            }
            }

        // GET: Quantri/subImages/Create
        public ActionResult Create()
        {
            ViewBag.productID = new SelectList(db.Products, "productID", "productName");
            return View();
        }

        // POST: Quantri/subImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,imgsrc,productID")] subImage subImage, HttpPostedFileBase fileUpload)
        {
            string path = Server.MapPath("~/Content/upload/images/");
            if (ModelState.IsValid)
            {
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(fileUpload.FileName);
                    string finalFileName = DateTime.Now.Ticks.ToString() + extensionName;
                    // now save the selected file with new name
                    fileUpload.SaveAs(path + finalFileName);
                    subImage.imgsrc = "/Content/upload/images/" + finalFileName;
                    db.subImages.Add(subImage);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.productID = new SelectList(db.Products, "productID", "productName", subImage.productID);
            return View(subImage);
        }

        // GET: Quantri/subImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subImage subImage = db.subImages.Find(id);
            if (subImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.productID = new SelectList(db.Products, "productID", "productName", subImage.productID);
            return View(subImage);
        }

        // POST: Quantri/subImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,imgsrc,productID")] subImage subImage, HttpPostedFileBase fileUpload)
        {
            string path = Server.MapPath("~/Content/upload/images/");

            if (ModelState.IsValid)
            {
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(fileUpload.FileName);
                    string finalFileName = DateTime.Now.Ticks.ToString() + extensionName;
                    // now save the selected file with new name
                    fileUpload.SaveAs(path + finalFileName);
                    subImage.imgsrc = "/Content/upload/images/" + finalFileName;
                    db.Entry(subImage).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.productID = new SelectList(db.Products, "productID", "productName", subImage.productID);
            return View(subImage);
        }

        // GET: Quantri/subImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subImage subImage = db.subImages.Find(id);
            if (subImage == null)
            {
                return HttpNotFound();
            }
            return View(subImage);
        }

        // POST: Quantri/subImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subImage subImage = db.subImages.Find(id);
            db.subImages.Remove(subImage);
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
