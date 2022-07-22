using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class CategorySecondController : Controller
    {
        fashionshopEntities db = new fashionshopEntities();
        // GET: CategoryFrists
        public ActionResult Index(string alias, string sort)
        {
            try
            {
                var categorysecond = db.categorySeconds.FirstOrDefault(x => x.alias.Trim().Equals(alias));
                return View(categorysecond);
            }
            catch
            {
                return Redirect("~/");
            }
        }
    }
}