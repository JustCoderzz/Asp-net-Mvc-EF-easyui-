using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentMange
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //new {id=@"\d +"}
                //路由里面也可以添加限制constrans 如@”\d+”要求id必须为数字
            );
        }
    }
}
