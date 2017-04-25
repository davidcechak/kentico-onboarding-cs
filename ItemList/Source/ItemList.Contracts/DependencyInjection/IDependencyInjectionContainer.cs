using System;

namespace ItemList.Contracts.DependencyInjection
{
    public interface IDependencyInjectionContainer
    {
        IDependencyInjectionContainer RegisterRequestScoped<TType, TImplementation>() 
            where TImplementation : TType;

        IDependencyInjectionContainer RegisterRequestScoped<TType>(Func<TType> implementationFactory);

        IDependencyInjectionContainer RegisterSingleton<TType, TImplementation>()
            where TImplementation : TType;

        IDependencyInjectionContainer RegisterSingleton<TType>(Func<TType> implementationFactory);
    }
}