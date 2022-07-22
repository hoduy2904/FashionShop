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
            return View();
        }

        [HttpPost]
        public ActionResult Create(cart giohang, bool isUpdateModified = false)
        {
            if (giohang != null)
            {
                var isCartHave = db.carts.Where(x => x.email.Equals(giohang.email) && x.productID.Equals(giohang.productID));
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
                        return Json(new
                        {
                            status = 200,
                            message = "Cập nhật thành công"
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
                    int count = db.SaveChanges();
                    if (count > 0)
                    {
                        return Json(new
                        {
                            status = 200,
                            message = "Thêm vào giỏ hàng thành công"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            status = 201,
                            message = "hêm vào giỏ hàng thất bại"
                        });
                    }
                }
            }
            return HttpNotFound();
        }

        [HttpDelete]
        public ActionResult Delete(string productID,string size, bool isDeleteAll = false)
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
                            message = "Xoá giỏ hàng thành công"
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
                    var cart = db.carts.Where(x => x.email.Equals(email)&&x.size.Equals(size) && x.productID.Equals(productID));
                    if(cart.Count() > 0)
                    {
                        db.carts.Remove(cart.FirstOrDefault());
                        int count=db.SaveChanges();
                        if(count > 0)
                        {
                            return Json(new
                            {
                                status = 200,
                                message = $"Đã Xoá {productID} ra giỏ hàng"
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