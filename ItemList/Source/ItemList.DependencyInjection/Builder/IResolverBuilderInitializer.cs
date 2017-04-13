using System;
using System.Collections.Generic;
using ItemList.Contracts.DependencyInjection;

namespace ItemList.DependencyInjection.Builder
{
    internal interface IResolverBuilderInitializer
    {
        IResolverBuilder RegisterDependencies(IEnumerable<Action<IDependencyInjectionContainer>> typesRegistrationMethods);
        IResolverBuilder RegisterDependencies(Action<IDependencyInjectionContainer> typesRegistrationMethod);
    }
}