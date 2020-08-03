using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ONLINEAPP.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //        name: "ActionApi",
            //        routeTemplate: "api/{controller}/{action}/{id}",
            //        defaults: new { id = RouteParameter.Optional }
            // );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{module}/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.EnableCors();

            config.Routes.MapHttpRoute(
                    name: "ActionApi",
                    routeTemplate: "api/{module}/{controller}/{action}/{id}",
                    defaults: new { id = RouteParameter.Optional }
             );

            //UnityConfig.RegisterComponents();

            //RMSUnityConfig.RegisterComponents();

            //WebApiConfig.Register(UnityConfig.RegisterComponents());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
        }
    }
}
