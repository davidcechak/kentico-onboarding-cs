using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Contracts.Tests.Base;
using ItemList.Api.Controllers;
using ItemList.Contracts.Api;
using ItemList.Contracts.Database;
using ItemList.Contracts.Models;
using ItemList.Contracts.Services;
using NSubstitute;
using NUnit.Framework;

namespace ItemList.Api.Tests
{
    [TestFixture]
    public class ItemsControllerTests
    {
        private ItemsController _itemsController;
        private IItemsRepository _repositoryMock;
        private IItemStoringService _itemStoringServiceMock;

        [SetUp]
        public void SetUp()
        {
            var itemUrlHelperMock = Substitute.For<IItemUrlHelper>();
            itemUrlHelperMock.GetUrl(Arg.Any<Guid>()).Returns(info => info.Arg<Guid>().ToString());

            _repositoryMock = Substitute.For<IItemsRepository>();
            _itemStoringServiceMock = Substitute.For<IItemStoringService>();

            _itemsController = new ItemsController(itemUrlHelperMock, _repositoryMock, _itemStoringServiceMock)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public async Task GetAsync_WithoutParameters_ReturnsAllItems()
        {
            IEnumerable<Item> expectedItems = new List<Item> {
                new Item { Id = new Guid("7383243d-9230-4a6c-94ea-122e151208ca"), Value = "text1" },
                new Item { Id = new Guid("83aa9154-2b5f-49b7-b7af-25cab7bf2159"), Value = "text2" }
            };
            _repositoryMock.GetAllAsync().Returns(Task.FromResult(expectedItems));

            var result = await _itemsController.GetAsync();
            var response = await result.ExecuteAsync(CancellationToken.None);
            IEnumerable<Item> actualItems;
            response.TryGetContentValue(out actualItems);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItems, Is.EqualTo(expectedItems).AsCollection.UsingItemComparer());
        }

        [Test]
        public async Task GetAsync_ExistingId_ReturnsDummyItem()
        {
            var id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7");
            var expectedItem = new Item { Id = id, Value = "text3" };
            _repositoryMock.GetAsync(Arg.Is(id)).Returns(Task.FromResult(expectedItem));

            var result = await _itemsController.GetAsync(id);
            var response = await result.ExecuteAsync(CancellationToken.None);
            Item actualItem;
            response.TryGetContentValue(out actualItem);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemComparer());
        }

        [Test]
        public async Task DeleteAsync_ExistingId_ReturnsNoContent()
        {
            var id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7");

            var result = await _itemsController.DeleteAsync(id);
            var response = await result.ExecuteAsync(CancellationToken.None);
            await _repositoryMock.Received().DeleteAsync(id);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task PutAsync_ExitingId_ReturnsNoContent()
        {
            var itemToPut = new Item { Id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7"), Value = "text3" };
            var itemSentToRepository = new Item();
            _repositoryMock.UpdateAsync(Arg.Do<Item>(item => { itemSentToRepository = item; })).Returns(Task.CompletedTask);

            var result = await _itemsController.PutAsync(itemToPut);
            var response = await result.ExecuteAsync(CancellationToken.None);

            Assert.That(itemSentToRepository, Is.EqualTo(itemToPut).UsingItemComparer());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task PostAsync_ValidItem_ReturnsDummyItemAndUrl()
        {
            var expectedItem = new Item
            {
                Id = new Guid("5081544A-5584-4449-B0CD-72B2BFF0AF30"),
                Ueid = "Hello Susan",
                Value = "text4"
            };
            var postedItem = new Item { Ueid = expectedItem.Ueid, Value = expectedItem.Value };
            Item itemSentToService = null;
            _itemStoringServiceMock.StoreNewItemAsync(Arg.Do<Item>(item => { itemSentToService = item; })).Returns(expectedItem);

            var result = await _itemsController.PostAsync(postedItem);
            var response = await result.ExecuteAsync(CancellationToken.None);
            Item actualItem;
            response.TryGetContentValue(out actualItem);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location.ToString(), Does.EndWith(expectedItem.Id.ToString()).IgnoreCase);
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemComparer());
            Assert.That(itemSentToService, Is.EqualTo(postedItem).UsingItemComparer());
        }
    }
}
