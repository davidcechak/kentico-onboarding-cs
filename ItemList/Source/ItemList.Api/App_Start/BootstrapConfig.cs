using System.Web.Http;
using ItemList.DependencyInjection;

namespace ItemList.Api
{
    public static class BootstrapConfig
    {
        public static void Register(HttpConfiguration config)
        {
            BootstrapFactory
                .Create(config)
                .RegisterDependencies();
        }
    }
}