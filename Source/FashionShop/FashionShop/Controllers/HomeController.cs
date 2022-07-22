using FashionShop.Models;
using FashionShop.Models.ClassView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        private fashionshopEntities db=new fashionshopEntities();
        public ActionResult Index()
        { 
           HomeClassView homeClassView = new HomeClassView();
            homeClassView.Banner = db.banners.ToList();
            homeClassView.CategoryFrists=db.categoryFrists.ToList();
            return View(homeClassView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult FormGeneral(string alias)
        {
            var formChung = db.formpages.Where(x => x.alias.Equals(alias));
            if (formChung.Count() > 0)
            {
                return View(formChung.FirstOrDefault());
            }
            return Redirect("~/");
        }

        public ActionResult Search(string tukhoa)
        {
            try
            {
                ViewBag.tukhoa = tukhoa;
                var product = db.Products.Where(x => x.productID.Equals(tukhoa) || x.productName.Contains(tukhoa));
                return View(product);
            }
            catch
            {
                ViewBag.tukhoa = tukhoa;
                return View(new Product());
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Navbar()
        {
            var navbar = (db.categoryFrists.ToList());
            return View(navbar);
        }

    }
}