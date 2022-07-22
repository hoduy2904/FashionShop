using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FashionShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Login",
               url: "login",
               defaults: new { controller = "Auth", action = "Login", id = UrlParameter.Optional },
               new[] { "FashionShop.Controllers" }
           );

            routes.MapRoute(
               name: "Register",
               url: "register",
               defaults: new { controller = "Auth", action = "Register", id = UrlParameter.Optional },
               new[] { "FashionShop.Controllers" }
           );

            routes.MapRoute(
               name: "Logout",
               url: "logout",
               defaults: new { controller = "Auth", action = "Logout", id = UrlParameter.Optional },
               new[] { "FashionShop.Controllers" }
           );

            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new { controller = "Home", action = "Search", id = UrlParameter.Optional },
                new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                name: "FormGeneral",
                url: "danhmuc/{alias}",
                defaults: new { controller = "Home", action = "FormGeneral", id = UrlParameter.Optional },
                new[] { "FashionShop.Controllers" }
            );


            routes.MapRoute(
                name: "Categoryfirsts",
                url: "{alias}",
                defaults: new { controller = "CategoryFrists", action = "Index", id = UrlParameter.Optional },
                new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                name: "CategorySecond",
                url: "{categoryfirst}/{alias}",
                defaults: new { controller = "CategorySecond", action = "Index", id = UrlParameter.Optional },
                new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
               name: "Product",
               url: "{categoryfirst}/{categorysecond}/{alias}",
               defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
               new[] { "FashionShop.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "FashionShop.Controllers" }
            );
        }
    }
}
