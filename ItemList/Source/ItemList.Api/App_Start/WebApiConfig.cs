using System.Net.Http;
using System.Web;
using System.Web.Http;
using ItemList.IoCBootstraper;
using ItemList.IoCBootstraper.Adapters;
using Microsoft.Practices.Unity;

namespace ItemList.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );  
        }
    }
}
