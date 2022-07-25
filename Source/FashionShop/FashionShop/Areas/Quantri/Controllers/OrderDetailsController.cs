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
    public class OrderDetailsController : BaseController
    {
        private fashionshopEntities db = new fashionshopEntities();

        // GET: Quantri/OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order);
            return View(orderDetails.ToList());
        }



        // GET: Quantri/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID");
            ViewBag.ProductID = new SelectList(db.Products, "productID", "productName");
            return View();
        }

        // POST: Quantri/OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,OrderID,productID,numberProduct,price,size")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.price = db.Products.Find(orderDetail.productID).price;
                orderDetail.numberProduct = orderDetail.numberProduct ?? 0;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "email", orderDetail.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "productID", "productName");
            return View(orderDetail);
        }

        // GET: Quantri/OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "email", orderDetail.OrderID);
            return View(orderDetail);
        }

        // POST: Quantri/OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,OrderID,productID,numberProduct,price,size")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.Entry(orderDetail).Property(x => x.productID).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "email", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: Quantri/OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Quantri/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
