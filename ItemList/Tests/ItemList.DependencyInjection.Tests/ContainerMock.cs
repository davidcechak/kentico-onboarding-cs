using System;
using System.Collections.Generic;
using ItemList.Contracts.DependencyInjection;

namespace ItemList.DependencyInjection.Tests
{
    internal class ContainerMock : IDependencyInjectionContainer
    {
        private readonly ICollection<string> _registeredContracts;

        public IEnumerable<string> RegisteredContracts => _registeredContracts;

        public ContainerMock(ICollection<string> registeredContracts)
        {
            _registeredContracts = registeredContracts;
        }

        public void RegisterRequestScoped<TType, TImplementation>()
            where TImplementation : TType 
            => AddTypeFullName<TType>();

        public void RegisterRequestScoped<TType>(Func<TType> implementationFactory)
            => AddTypeFullName<TType>();

        public void RegisterSingleton<TType, TImplementation>() 
            where TImplementation : TType
            => AddTypeFullName<TType>();
        
        public void RegisterSingleton<TType>(Func<TType> implementationFactory) => AddTypeFullName<TType>();

        private void AddTypeFullName<TType>() => _registeredContracts.Add(typeof(TType).FullName);
    }
}