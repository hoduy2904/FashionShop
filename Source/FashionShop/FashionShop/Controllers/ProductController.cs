using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        fashionshopEntities db=new fashionshopEntities();
        // GET: DetailProduct
        public ActionResult Index(string alias)
        {
            try
            {
                var product = db.Products.FirstOrDefault(x => x.alias.Trim().Equals(alias));
                return View(product);
            }
            catch
            {
                return Redirect("~/");
            }
        }
    }
}