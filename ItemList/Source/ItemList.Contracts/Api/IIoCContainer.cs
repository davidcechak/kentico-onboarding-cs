using System;

namespace ItemList.Contracts.Api
{
    public interface IIoCContainer
    {
        void RegisterRequestScoped<TType, TImplementation>() 
            where TImplementation : TType;

        void RegisterRequestScoped<TType>(Func<TType> implementationFactory);
    }
}