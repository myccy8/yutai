using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Yutai.Admin
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //注册路由
            config.Routes.MapHttpRoute(
                name: "ApiByAction",
                routeTemplate: "api/{controller}/{action}",
                 defaults: new { action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
