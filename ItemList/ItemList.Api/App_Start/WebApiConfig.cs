using System.Web.Http;
using ItemList.Api.DependecyInjection;
using ItemList.IoCBootstraper;
using Microsoft.Practices.Unity;

namespace ItemList.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            config.DependencyResolver = new UnityResolver(container);

            var unityAdapter = new UnityAdapter(container);
            Bootstraper.RegisterTypes(unityAdapter);

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
