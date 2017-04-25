using System;
using System.Collections.Generic;
using System.Web.Http;
using ItemList.Contracts.DependencyInjection;
using ItemList.DependencyInjection.Builder;

namespace ItemList.DependencyInjection
{
    public static class BootstrapFactory
    {
        // Internal for test purpose
        internal static readonly Lazy<IEnumerable<IBootstrapper>> Bootstrappers = new Lazy<IEnumerable<IBootstrapper>>(() =>
            BootstrappersBuilder
            .Create()
            .Include<Database.Bootstrapper>()
            .Include<Services.Bootstrapper>()
            .Include<Api.Services.Bootstrapper>()
            .AsEnumerable());

        public static IBootstrap Create(HttpConfiguration config) 
            => new Bootstrap(Bootstrappers.Value, CreateResolverBuilder(config));

        private static IResolverBuilderInitializer CreateResolverBuilder(HttpConfiguration config) 
            => UnityWebResolverBuilder.Create(config);
    }
}