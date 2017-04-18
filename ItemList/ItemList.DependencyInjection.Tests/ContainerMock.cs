using System;
using System.Collections.Generic;
using ItemList.Contracts.DependencyInjection;

namespace ItemList.DependencyInjection.Tests
{
    internal class ContainerMock : IDependencyInjectionContainer
    {
        private readonly ICollection<string> _registeredContracts = new List<string>();

        public IEnumerable<string> RegisteredContracts => _registeredContracts;

        public void RegisterRequestScoped<TType, TImplementation>()
            where TImplementation : TType 
            => AddTypeFullName<TType>();

        public void RegisterRequestScoped<TType>(Func<TType> implementationFactory)
            => AddTypeFullName<TType>();

        public void RegisterSingleton<TType, TImplementation>() 
            where TImplementation : TType
            => AddTypeFullName<TType>();

        private void AddTypeFullName<TType>() => _registeredContracts.Add(typeof(TType).FullName);

        public void RegisterSingleton<TType>(Func<TType> implementationFactory) => AddTypeFullName<TType>();
    }
}