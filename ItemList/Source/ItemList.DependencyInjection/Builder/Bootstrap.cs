using System;
using System.Collections.Generic;
using System.Linq;
using ItemList.Contracts.DependencyInjection;

namespace ItemList.DependencyInjection.Builder
{
    internal class Bootstrap : IBootstrap
    {
        private readonly IEnumerable<IBootstrapper> _bootstrappers;
        private readonly IResolverBuilderInitializer _resolverBuilder;

        public Bootstrap(IEnumerable<IBootstrapper> bootstrappers, IResolverBuilderInitializer resolverBuilder)
        {
            _bootstrappers = bootstrappers;
            _resolverBuilder = resolverBuilder;
        }

        public IBootstrap RegisterDependencies()
        {
            var typesRegistrationMethods = _bootstrappers
                .Select<IBootstrapper, Action<IDependencyInjectionContainer>>(boostrapper 
                    => boostrapper.RegisterTypes);

            _resolverBuilder
                .RegisterDependencies(typesRegistrationMethods)
                .RegisterDependencies(RegisterBoostrap)
                .RegisterDependencyResolver();
            
            return this;
        }

        private void RegisterBoostrap(IDependencyInjectionContainer adapter) 
            => adapter.RegisterSingleton<IBootstrap>(() => this);
    }
}