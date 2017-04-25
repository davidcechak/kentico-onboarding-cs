using System;
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
        public IDependencyInjectionContainer RegisterRequestScoped<TInterface, TImplementation>()
            where TImplementation : TInterface 
            => RegisterInterfaceImplementation<TInterface, TImplementation>(new HierarchicalLifetimeManager());

        public IDependencyInjectionContainer RegisterRequestScoped<TType>(Func<TType> implementationFactory) 
            => RegisterType<TType>(new HierarchicalLifetimeManager(), GetNewInjectionFactory(implementationFactory));

        public IDependencyInjectionContainer RegisterSingleton<TInterface, TImplementation>() 
            where TImplementation : TInterface 
            => RegisterInterfaceImplementation<TInterface, TImplementation>(new ContainerControlledLifetimeManager());

        public IDependencyInjectionContainer RegisterSingleton<TType>(Func<TType> implementationFactory) 
            => RegisterType<TType>(new ContainerControlledLifetimeManager(), GetNewInjectionFactory(implementationFactory));

        private IDependencyInjectionContainer RegisterInterfaceImplementation<TInterface, TImplementation>(LifetimeManager lifetimeManager) 
            where TImplementation : TInterface
        {
            _container.RegisterType<TInterface, TImplementation>(lifetimeManager);
            return this;
        }

        private IDependencyInjectionContainer RegisterType<TType>(LifetimeManager lifetimeManager, InjectionFactory injectionFactory)
        {
            _container.RegisterType<TType>(lifetimeManager, injectionFactory);
            return this;
        }

        private static InjectionFactory GetNewInjectionFactory<TType>(Func<TType> implementationFactory)
        {
            return new InjectionFactory(_ => implementationFactory());
        }
    }
}