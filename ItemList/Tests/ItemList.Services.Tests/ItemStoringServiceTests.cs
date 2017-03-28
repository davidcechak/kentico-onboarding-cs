using System;
using System.Threading.Tasks;
using ItemList.Contracts.DatabaseLayer;
using ItemList.Contracts.Models;
using ItemList.Contracts.ServiceLayer;
using ItemList.ServiceLayer.Items;
using NSubstitute;
using NUnit.Framework;
using Contracts.Tests.Base.Item;

namespace ItemList.Services.Tests
{
    [TestFixture]
    public class ItemStoringServiceTests
    {
        private IIdentifierService _identifierServiceMock;
        private IItemsRepository _repositoryMock;
        private ItemStoringService _itemStoringService;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = Substitute.For<IItemsRepository>();
            _identifierServiceMock = Substitute.For<IIdentifierService>();

            _itemStoringService = new ItemStoringService(_identifierServiceMock, _repositoryMock);

        }

        [Test]
        public async Task StoreNewItemAsync_ValidItem_ReturnsStoredItemWithNewId()
        {
            var expectedItem = new Item
            {
                Id = new Guid("236E156C-A0A3-4B32-BD92-345672AEE31C"),
                Ueid = "Hello Susan",
                Value = "text4"
            };
            var postedItem = new Item
            {
                Ueid = expectedItem.Ueid,
                Value = expectedItem.Value
            };
            Item itemSentToRepository = null;
            _identifierServiceMock.GetIdentifier().Returns(expectedItem.Id);
            _repositoryMock.CreateAsync(Arg.Do<Item>(item => { itemSentToRepository = item; })).Returns(Task.CompletedTask);


            Item actualItem = await _itemStoringService.StoreNewItemAsync(postedItem);
           
            Assert.That(itemSentToRepository, Is.EqualTo(expectedItem).UsingItemComparer());
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemComparer());
        }
    }
}