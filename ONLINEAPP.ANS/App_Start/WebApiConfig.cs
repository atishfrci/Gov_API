using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity.WebApi;

namespace ONLINEAPP.ANS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            var _cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(_cors);

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{module}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            UnityConfig.RegisterComponents();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(config.Formatters.JsonFormatter);
        }
    }
}
