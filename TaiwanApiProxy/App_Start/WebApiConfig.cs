namespace TaiwanApiProxy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Mvc;
    using System.Web.Routing;
    using TaiwanApiProxy.Utilities;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IHttpActionSelector), new ApiActionSelector());

            config.Routes.MapHttpRoute(
                name: "DefaultApiPost",
                routeTemplate: "Api/{action}",
                defaults: new { controller = "ProxyPost", action = "Index" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post.Method) });

            config.Routes.MapHttpRoute(
                name: "DefaultApiGet",
                routeTemplate: "Api/{apiaction}",
                defaults: new { controller = "ProxyGet", action = "Detail" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get.Method) });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "Api",
                defaults: new { controller = "ProxyGet", action = "List" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get.Method) });

            config.Routes.MapHttpRoute(
                name: "Default_RuleGet",
                routeTemplate: "Rule/{action}/{id}",
                defaults: new { controller = "RuleGet", action = "Index", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get.Method) });

            config.Routes.MapHttpRoute(
                name: "Default_RulePost",
                routeTemplate: "Rule/{action}",
                defaults: new { controller = "RulePost", action = "Index" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post.Method) });

            config.Routes.MapHttpRoute(
                name: "Default_LogGet",
                routeTemplate: "Log/{action}",
                defaults: new { controller = "LogGet", action = "Index" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get.Method) });
        }
    }
}
