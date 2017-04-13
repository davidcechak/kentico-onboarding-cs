using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using ItemList.Contracts.DependencyInjection;
using ItemList.DependencyInjection.Builder;
using NSubstitute;
using NUnit.Framework;

namespace ItemList.DependencyInjection.Tests
{
    [TestFixture]
    internal class ContratsImplementationTests
    {
        private ContainerMock _containerMock;
        private IResolverBuilderInitializer _resolverMock;

        [SetUp]
        public void SetUp()
        {
            _containerMock = new ContainerMock();
            _resolverMock = Substitute.For<IResolverBuilder>();
            _resolverMock
                .RegisterDependencies(Arg.Any<Action<IDependencyInjectionContainer>>())
                .Returns(info =>
                {
                    info.Arg<Action<IDependencyInjectionContainer>>().Invoke(_containerMock);
                    return _resolverMock;
                });
            _resolverMock
                .RegisterDependencies(Arg.Any<IEnumerable<Action<IDependencyInjectionContainer>>>())
                .Returns(info =>
                {
                    foreach (var typeRegistrationMethod in info.Arg<IEnumerable<Action<IDependencyInjectionContainer>>>())
                    {
                        typeRegistrationMethod.Invoke(_containerMock);
                    }
                    return _resolverMock;
                });
        }

        [Test]
        public void AllContractsHaveRegisteredImplementation()
        {
            var bootstrapperRegistrations = BootstrapFactory.Bootstrappers.Value;
            var bootstrap = new Bootstrap(bootstrapperRegistrations, _resolverMock);
            ICollection<string> expectedRegisteredContracts = new List<string>();
            Assembly contractsAssembly = typeof(IDependencyInjectionContainer).Assembly;
            foreach (Type type in contractsAssembly.GetTypes())
            {
                if (type.IsInterface)
                {
                    expectedRegisteredContracts.Add(type.FullName);
                }
            }
            expectedRegisteredContracts.Remove(typeof(IDependencyInjectionContainer).FullName);
            expectedRegisteredContracts.Remove(typeof(IBootstrapper).FullName);
            expectedRegisteredContracts.Add(typeof(HttpRequestMessage).FullName);

            bootstrap.RegisterDependencies();
            var actualRegisteredContracts = _containerMock.RegisteredContracts;

            Assert.That(actualRegisteredContracts, Is.EquivalentTo(expectedRegisteredContracts));
        }
    }

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