using System;
using System.Collections.Generic;
using ItemList.Contracts.DependencyInjection;

namespace ItemList.DependencyInjection.Tests
{
    internal class ContainerMock : IDependencyInjectionContainer
    {
        private readonly ICollection<string> _registeredContracts;

        public IEnumerable<string> RegisteredContracts 
            => _registeredContracts;

        public ContainerMock(ICollection<string> registeredContracts)
        {
            _registeredContracts = registeredContracts;
        }

        public IDependencyInjectionContainer RegisterRequestScoped<TType, TImplementation>()
            where TImplementation : TType 
            => AddTypeFullName<TType>();

        public IDependencyInjectionContainer RegisterRequestScoped<TType>(Func<TType> implementationFactory)
            => AddTypeFullName<TType>();

        public IDependencyInjectionContainer RegisterSingleton<TType, TImplementation>() 
            where TImplementation : TType
            => AddTypeFullName<TType>();
        
        public IDependencyInjectionContainer RegisterSingleton<TType>(Func<TType> implementationFactory) 
            => AddTypeFullName<TType>();

        private IDependencyInjectionContainer AddTypeFullName<TType>()
        {
            _registeredContracts.Add(typeof(TType).FullName);
            return this;
        }
    }
}