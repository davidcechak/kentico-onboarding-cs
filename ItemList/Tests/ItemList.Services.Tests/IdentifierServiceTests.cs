using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemList.Services.Wrappers;
using NUnit.Framework;

namespace ItemList.Services.Tests
{
    [TestFixture]
    internal class IdentifierServiceTests
    {
        private IdentifierService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new IdentifierService();
        }

        [Test]
        public void GetIdentifier_DoesNotReturnEmptyGuid()
        {
            Guid actualId = _service.GetIdentifier();

            Assert.That(actualId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GetIdentifier_CalledTwice_ReturnsDifferentIdentifiers()
        {
            Guid firstId = _service.GetIdentifier();
            Guid secondId = _service.GetIdentifier();

            Assert.That(firstId, Is.Not.EqualTo(secondId));
        }
    }
}
