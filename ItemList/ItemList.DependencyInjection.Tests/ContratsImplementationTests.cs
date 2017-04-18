using System;
using System.Collections.Generic;
using System.Linq;
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
            var bootstrap = new Bootstrap(BootstrapFactory.Bootstrappers.Value, _resolverMock);
            Assembly contractsAssembly = typeof(IDependencyInjectionContainer).Assembly;
            var expectedRegisteredContracts = contractsAssembly
                .GetTypes()
                .Where(type => type.IsInterface)
                .Select(type => type.FullName)
                .ToList();
            // container should not register itself
            expectedRegisteredContracts.Remove(typeof(IDependencyInjectionContainer).FullName);
            // IBootstrapper is pattern for Bootstrapper classes, does not have specific implementation
            expectedRegisteredContracts.Remove(typeof(IBootstrapper).FullName);
            // WebApi should have HttpRequestMessage registered
            expectedRegisteredContracts.Add(typeof(HttpRequestMessage).FullName);

            bootstrap.RegisterDependencies();
            var actualRegisteredContracts = _containerMock.RegisteredContracts;

            const string firstSeparator = ",\n\t\t\t\t\t\t\t\t   ";
            const string secondSeparator = ",\n\t\t\t\t\t\t  ";
            Assert.That(
                actualRegisteredContracts,
                Is.EquivalentTo(expectedRegisteredContracts),
                $"This should not be registered: [ {string.Join(firstSeparator, actualRegisteredContracts.Except(expectedRegisteredContracts))} ],\n\n" +
                $"This is not registered: [ {string.Join(secondSeparator, expectedRegisteredContracts.Except(actualRegisteredContracts))} ],\n"
                );
        }
    }
}