using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models;

namespace FashionShop.Controllers
{
    public class CartsController : Controller
    {
        fashionshopEntities db = new fashionshopEntities();
        // GET: Carts
        public ActionResult Index()
        {
            if (Session["account"] != null)
            {
                var taikhoan = (account)Session["account"];
                var giohang = db.carts.Where(x => x.email.Equals(taikhoan.email));
                return View(giohang);
            }
            return Redirect("~/");
        }

        [HttpPost]
        public ActionResult Pay(int diaChi)
        {
            if (Session["account"] != null)
            {
                var taikhoan = (account)Session["account"];
                var taikhoancur=db.accounts.Find(taikhoan.email);
                var thongtinDiaChi = taikhoancur.infomationAccounts.FirstOrDefault(x => x.id == diaChi);
                var gioHang = taikhoancur.carts;
                Order order = new Order
                {
                    email = taikhoan.email,
                    isVerified = false,
                    addressOrder=thongtinDiaChi.AddressAccount,
                    phone=thongtinDiaChi.phone,
                    timecreate = DateTime.Now
                };
                db.Orders.Add(order);
                int count = db.SaveChanges();
                if (count > 0)
                {
                    foreach (var item in gioHang)
                    {
                        db.OrderDetails.Add(new OrderDetail
                        {
                            numberProduct = item.numberProduct,
                            OrderID=order.OrderID,
                            price=item.Product.price,
                            productID=item.productID,
                            size=item.size,
                        });
                    }
                    int countFinal=db.SaveChanges();
                    if (countFinal > 0)
                    {
                        db.carts.RemoveRange(gioHang);
                        db.SaveChanges();
                        return Json(new
                        {
                            status = 200,
                            message = "Đặt hàng thành công",
                            madh=order.OrderID
                        });
                    }
                    else
                    {
                        db.Orders.Remove(order);
                        db.SaveChanges();
                        return Json(new
                        {
                            status = 202,
                            message = "Đặt hàng thất bại"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        status = 201,
                        message = "Đặt hàng thất bại"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    status = 501,
                    message = "Bạn phải đăng nhập"
                });
            }
        }

        [HttpPost]
        public ActionResult Create(cart giohang, bool isUpdateModified = false)
        {
            if (Session["account"] == null)
            {
                return Json(new
                {
                    status = 501,
                    message = "Bạn cần phải đăng nhập"
                });
            }
            if (giohang != null)
            {

                var taikhoan = (account)Session["account"];
                giohang.email = taikhoan.email;
                var isCartHave = db.carts.Where(x => x.email.Equals(giohang.email) && x.size.Equals(giohang.size) && x.productID.Equals(giohang.productID));
                if (isCartHave.Count() > 0)
                {

                    db.Entry(giohang).State = System.Data.Entity.EntityState.Modified;
                    if (!isUpdateModified)
                    {
                        giohang.numberProduct += isCartHave.FirstOrDefault().numberProduct;
                    }
                    int count = db.SaveChanges();
                    if (count > 0)
                    {
                        giohang.Product= db.Products.Find(giohang.productID);
                        return Json(new
                        {
                            status = 200,
                            message = "Cập nhật thành công",
                            totalProduct=ThuVien.VietnamDong(giohang.numberProduct*giohang.Product.price),
                            totalCart=ThuVien.VietnamDong(db.carts.Where(x=>x.email.Equals(taikhoan.email)).Sum(x=>x.numberProduct*x.Product.price)??0)
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            status = 201,
                            message = "Cập nhật thất bại"
                        });
                    }
                }
                else
                {
                    db.carts.Add(giohang);
                    int count = db.SaveChanges();
                    if (count > 0)
                    {
                        return Json(new
                        {
                            status = 200,
                            message = "Thêm vào giỏ hàng thành công",
                            count=db.carts.Where(x=>x.email.Equals(taikhoan.email)).Count()
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            status = 201,
                            message = "Thêm vào giỏ hàng thất bại"
                        });
                    }
                }
            }
            return HttpNotFound();
        }

        [HttpDelete]
        public ActionResult Delete(string productID, string size, bool isDeleteAll = false)
        {
            if (Session["account"] != null)
            {
                var taikhoan = (account)Session["account"];
                string email = taikhoan.email;
                if (!string.IsNullOrEmpty(email) && isDeleteAll)
                {
                    var giohang = db.carts.Where(x => x.email.Equals(email));
                    int count = db.carts.RemoveRange(giohang).Count();
                    if (count > 0)
                    {
                        return Json(new
                        {
                            status = 200,
                            message = "Xoá giỏ hàng thành công",
                            totalMoney=0,
                            count=0
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            status = 200,
                            message = "Giỏ hàng trống"
                        });
                    }
                }
                if (!(string.IsNullOrEmpty(productID) && string.IsNullOrEmpty(email)))
                {
                    var cart = db.carts.Where(x => x.email.Equals(email) && x.size.Equals(size) && x.productID.Equals(productID));
                    if (cart.Count() > 0)
                    {
                       db.carts.Remove(cart.FirstOrDefault());
                        int count = db.SaveChanges();
                        if (count > 0)
                        {
                            return Json(new
                            {
                                status = 200,
                                message = $"Đã Xoá {productID} ra giỏ hàng",
                                totalMoney =ThuVien.VietnamDong(db.carts.Sum(x => x.numberProduct * x.Product.price)),
                                count= db.carts.Where(x => x.email.Equals(taikhoan.email)).Count()
                            });
                        }
                        else
                        {
                            return Json(new
                            {
                                status = 201,
                                message = "Xoá thất bại"
                            });
                        }
                    }
                }
                return Json(new
                {
                    status = 401,
                    message = "Lỗi, thử lại sau"
                });
            }
            else
            {
                return Json(new
                {
                    status = 501,
                    message = "Bạn không có quyền"
                });
            }
        }
    }
}