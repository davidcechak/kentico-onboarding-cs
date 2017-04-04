using System;
using System.Web.Http;
using ItemList.Contracts.Api;

namespace ItemList.Contracts.Bootstrap
{
    public interface IResolveBootrappersTypes : IIncludeBootstrapper<IResolveBootrappersTypes>
    {
        Action<HttpConfiguration> RegisterAll(IIoCContainer container);
    }
}