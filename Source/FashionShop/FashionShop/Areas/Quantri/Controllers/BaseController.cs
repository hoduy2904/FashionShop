using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Areas.Quantri.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session["account"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/");
            }
        }
    }
}