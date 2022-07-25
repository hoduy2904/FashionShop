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
    public class ProductsController : BaseController
    {
        private fashionshopEntities db = new fashionshopEntities();

        // GET: Quantri/Products
        public ActionResult Index()
        {
            ViewBag.categorySecond = new SelectList(db.categorySeconds, "id", "CategoryName");
            var products = db.Products.Include(p => p.categorySecond1);
            return View(products.ToList());
        }

        // GET: Quantri/Products/Details/5
        public ActionResult Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Quantri/Products/Create
        public ActionResult Create()
        {
            ViewBag.categorySecond = new SelectList(db.categorySeconds, "id", "CategoryName");
            
            return View();
        }

        // POST: Quantri/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "productID,productName,price,priceOld,size,categorySecond,noidung,views,alias")] Product product, IEnumerable<HttpPostedFileBase> fileUploads)
        {
            string path = Server.MapPath("~/Content/upload/images/");
            if (ModelState.IsValid)
            {
                product.price =product.price?? 0;
                product.priceOld = product.price ?? 0;
                product.views = 0;
                product.alias = ThuVien.convertToUnSign3(product.productName);
                db.Products.Add(product);
                int count=db.SaveChanges();
                if (count > 0)
                {
                    if (fileUploads.Count() > 0)
                    {
                        foreach(var file in fileUploads)
                        {
                            if (file.ContentLength > 0) 
                            {
                                string extensionName = System.IO.Path.GetExtension(file.FileName);
                                string finalFileName = DateTime.Now.Ticks.ToString() +extensionName;
                                // now save the selected file with new name
                                file.SaveAs(path + finalFileName);
                                db.subImages.Add(new subImage{
                                    imgsrc="/Content/upload/images/"+finalFileName,
                                    productID=product.productID
                                 });
                            }
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.categorySecond = new SelectList(db.categorySeconds, "id", "CategoryName", product.categorySecond);
            return View(product);
        }

        // GET: Quantri/Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.categorySecond = new SelectList(db.categorySeconds, "id", "CategoryName", product.categorySecond);
            return View(product);
        }

        // POST: Quantri/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "productID,productName,price,priceOld,size,categorySecond,noidung,views,alias")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.price = product.price ?? 0;
                product.priceOld = product.price ?? 0;
                product.alias=ThuVien.convertToUnSign3(product.productName);
                db.Entry(product).State = EntityState.Modified;
                db.Entry(product).Property(x => x.views).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categorySecond = new SelectList(db.categorySeconds, "id", "CategoryName", product.categorySecond);
            return View(product);
        }

        // GET: Quantri/Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Quantri/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
