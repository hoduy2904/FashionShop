using FashionShop.Models;
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
            var tk = System.Web.HttpContext.Current.Session["account"];
            if ( tk== null)
            {
               
                System.Web.HttpContext.Current.Response.Redirect("~/login?url=quantri");
            }
            else
            {
                if (!((account)tk).isAdmin.Value)
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/");
                }
            }
        }
    }
}