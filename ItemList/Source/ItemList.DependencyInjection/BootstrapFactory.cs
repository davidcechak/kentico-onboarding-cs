using System;
using System.Collections.Generic;
using System.Web.Http;
using ItemList.Contracts.Bootstrap;
using ItemList.DependencyInjection.Builder;

namespace ItemList.DependencyInjection
{
    public static class BootstrapFactory
    {
        private static readonly Lazy<IEnumerable<IBootstrapper>> Bootstrappers = new Lazy<IEnumerable<IBootstrapper>>(() =>
            BootstrappersBuilder
            .Create()
            .Include<Database.Bootstrapper>()
            .Include<Services.Bootstrapper>()
            .Include<Api.Services.Bootstrapper>()
            .AsEnumerable());

        public static IBootstrap Create(HttpConfiguration config) 
            => new UnityWebBootstrap(config, Bootstrappers.Value);
    }
}