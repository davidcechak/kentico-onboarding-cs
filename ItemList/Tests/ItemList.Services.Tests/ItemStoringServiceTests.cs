﻿using System;
using System.Threading.Tasks;
using Contracts.Tests.Base;
using ItemList.Contracts.Database;
using ItemList.Contracts.Models;
using ItemList.Contracts.Services;
using ItemList.Services.Items;
using NSubstitute;
using NUnit.Framework;

namespace ItemList.Services.Tests
{
    [TestFixture]
    public class ItemStoringServiceTests
    {
        private IIdentifierService _identifierServiceMock;
        private IItemsRepository _repositoryMock;
        private ITimeService _timeService;
        private ItemStoringService _itemStoringService;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = Substitute.For<IItemsRepository>();
            _identifierServiceMock = Substitute.For<IIdentifierService>();
            _timeService = Substitute.For<ITimeService>();

            _itemStoringService = new ItemStoringService(_identifierServiceMock, _repositoryMock, _timeService);
        }

        [Test]
        public async Task StoreNewItemAsync_ValidItem_ReturnsStoredItemWithNewId()
        {
            DateTime now = new DateTime(year: 1944, month: 6, day: 6);
            var expectedItem = new Item
            {
                Id = new Guid("236E156C-A0A3-4B32-BD92-345672AEE31C"),
                Ueid = "Hello Susan",
                Value = "text4",
                Created = now,
                LastUpdated = now
            };
            var postedItem = new Item
            {
                Ueid = expectedItem.Ueid,
                Value = expectedItem.Value
            };
            Item itemSentToRepository = null;
            _identifierServiceMock.GetIdentifier().Returns(expectedItem.Id);
            _repositoryMock.CreateAsync(Arg.Do<Item>(item => { itemSentToRepository = item; })).Returns(Task.CompletedTask);
            _timeService.Now().Returns(now);

            Item actualItem = await _itemStoringService.StoreNewItemAsync(postedItem);
           
            Assert.That(itemSentToRepository, Is.EqualTo(expectedItem).UsingItemComparer());
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemComparer());
        }
    }
}