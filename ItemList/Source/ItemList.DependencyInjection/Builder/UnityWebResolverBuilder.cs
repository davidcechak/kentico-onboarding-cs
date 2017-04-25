using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dependencies;
using ItemList.Contracts.DependencyInjection;
using ItemList.DependencyInjection.Adapters;
using Microsoft.Practices.Unity;

namespace ItemList.DependencyInjection.Builder
{
    internal class UnityWebResolverBuilder : IResolverBuilder
    {
        private readonly HttpConfiguration _config;
        private readonly IDependencyResolver _resolver;
        private readonly IDependencyInjectionContainer _adapter;

        public static IResolverBuilderInitializer Create(HttpConfiguration config)
            => new UnityWebResolverBuilder(config);

        private UnityWebResolverBuilder(HttpConfiguration config)
        {
            var container = new UnityContainer();
            _adapter = new UnityAdapter(container);
            _resolver = new UnityResolver(container);

            _config = config;
        }

        public void RegisterDependencyResolver()
        {
            _config.DependencyResolver = _resolver;
        }

        public IResolverBuilder RegisterDependencies(Action<IDependencyInjectionContainer> typesRegistrationMethod)
        {
            typesRegistrationMethod(_adapter);
            return this;
        }

        public IResolverBuilder RegisterDependencies(IEnumerable<Action<IDependencyInjectionContainer>> typesRegistrationMethods)
        {
            foreach (var typesRegistrationMethod in typesRegistrationMethods)
            {
                RegisterDependencies(typesRegistrationMethod);
            }
            return this;
        }
    }
}