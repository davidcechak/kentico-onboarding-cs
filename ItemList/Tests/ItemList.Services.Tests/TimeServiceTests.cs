using System;
using System.Threading;
using ItemList.Services.Wrappers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ItemList.Services.Tests
{
    [TestFixture]
    internal class TimeServiceTests
    {
        private TimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _timeService = new TimeService();
        }

        [Test]
        public void Now_DoesNotReturnDefaultValue()
        {
            var actualValue = _timeService.Now();

            Assert.That(actualValue, Is.Not.EqualTo(default(DateTime)));
        }

        [Test]
        public void Now_ReturnsHigherTimeOnEachCall()
        {
            int timeToSleep = 100;
            var valueOnFirstCall = _timeService.Now();
            Thread.Sleep(timeToSleep);
            var valueOnSecondCall = _timeService.Now();

            Assert.That(valueOnSecondCall, Is.GreaterThan(valueOnFirstCall)
                .And.Not.EqualTo(valueOnFirstCall).Within(timeToSleep - 1).Milliseconds);
        }
    }
}