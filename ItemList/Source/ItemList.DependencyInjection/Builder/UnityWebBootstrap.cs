using System.Collections.Generic;
using System.Web.Http;
using ItemList.Contracts.DependencyInjection;
using ItemList.DependencyInjection.Adapters;
using Microsoft.Practices.Unity;

namespace ItemList.DependencyInjection.Builder
{
    public class UnityWebBootstrap : IBootstrap
    {
        private readonly HttpConfiguration _config;
        private readonly IEnumerable<IBootstrapper> _bootstrappers;

        public UnityWebBootstrap(HttpConfiguration config, IEnumerable<IBootstrapper> bootstrappers)
        {
            _config = config;
            _bootstrappers = bootstrappers;
        }

        public IBootstrap RegisterDependencies()
        {
            var container = new UnityContainer();

            var unityAdapter = new UnityAdapter(container);
            foreach (var instance in _bootstrappers)
            {
                instance.RegisterTypes(unityAdapter);
            }
            
            // Register this instance, so it works like singleton
            container.RegisterInstance<IBootstrap>(this);

            _config.DependencyResolver = new UnityResolver(container);

            return this;
        }
    }
}