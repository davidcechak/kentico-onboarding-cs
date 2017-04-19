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
            _containerMock = new ContainerMock(new HashSet<string>());
            SetUpResolverMock();
        }

        [Test]
        public void AllContractsHaveRegisteredImplementation()
        {
            var bootstrap = new Bootstrap(BootstrapFactory.Bootstrappers.Value, _resolverMock);
            var expectedContracts = GetExpectedContracts();

            bootstrap.RegisterDependencies();
            var actualContracts = _containerMock.RegisteredContracts;

            const string firstSeparator = ",\n\t\t\t\t\t\t\t\t   ";
            const string secondSeparator = ",\n\t\t\t\t\t\t  ";
            Assert.That(
                actualContracts,
                Is.EquivalentTo(expectedContracts),
                $"This should not be registered: [ {string.Join(firstSeparator, actualContracts.Except(expectedContracts))} ],\n\n" +
                $"This is not registered: [ {string.Join(secondSeparator, expectedContracts.Except(actualContracts))} ],\n"
                );
        }

        private List<string> GetExpectedContracts()
        {
            Assembly contractsAssembly = typeof(IDependencyInjectionContainer).Assembly;
            var expectedContracts = contractsAssembly
                .GetTypes()
                .Where(type => type.IsInterface)
                .Select(type => type.FullName)
                .ToList();

            // container should not register itself
            expectedContracts.Remove(typeof(IDependencyInjectionContainer).FullName);

            // IBootstrapper is pattern for Bootstrapper classes, does not have specific implementation
            expectedContracts.Remove(typeof(IBootstrapper).FullName);

            // WebApi should have HttpRequestMessage registered
            expectedContracts.Add(typeof(HttpRequestMessage).FullName);

            return expectedContracts;
        }

        private void SetUpResolverMock()
        {
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
    }
}