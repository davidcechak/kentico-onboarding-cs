using System;

namespace ItemList.Contracts.DependencyInjection
{
    public interface IDependencyInjectionContainer
    {
        void RegisterRequestScoped<TType, TImplementation>() 
            where TImplementation : TType;

        void RegisterRequestScoped<TType>(Func<TType> implementationFactory);

        void RegisterSingleton<TType, TImplementation>()
            where TImplementation : TType;
    }
}