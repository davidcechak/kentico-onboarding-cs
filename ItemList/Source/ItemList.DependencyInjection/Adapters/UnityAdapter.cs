using System;
using ItemList.Contracts.Api;
using ItemList.Contracts.DependencyInjection;
using Microsoft.Practices.Unity;

namespace ItemList.DependencyInjection.Adapters
{
    internal class UnityAdapter : IDependencyInjectionContainer
    {
        private readonly UnityContainer _container;

        public UnityAdapter(UnityContainer container)
        {
            _container = container;
        }
        public void RegisterRequestScoped<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _container.RegisterType<TInterface, TImplementation>(new HierarchicalLifetimeManager());
        }

        public void RegisterRequestScoped<TType>(Func<TType> implementationFactory)
        {
            _container.RegisterType<TType>(new InjectionFactory(_ => implementationFactory()));
        }
    }
}