﻿using System.Web.Mvc;

namespace FashionShop.Areas.Quantri
{
    public class QuantriAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Quantri";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Quantri_default",
                "Quantri/{controller}/{action}/{id}",
                new { controller= "categoryFrists", action = "Index", id = UrlParameter.Optional },
                new[] { "FashionShop.Areas.Quantri.Controllers" }
            );
        }
    }
}