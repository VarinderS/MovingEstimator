using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MovingEstimator
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Api with extension",
                routeTemplate: "api/{controller}.{ext}/{id}",
                defaults: new { id = RouteParameter.Optional, ext = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
