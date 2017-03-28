using System.Web.Http;
using ItemList.DependencyInjection.Adapters;
using Microsoft.Practices.Unity;

namespace ItemList.DependencyInjection
{
    public class RegisterContainer
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            var unityAdapter = new UnityAdapter(container);
            Bootstrap.RegisterTypes(unityAdapter);
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}