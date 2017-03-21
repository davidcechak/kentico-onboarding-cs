using System;
using System.Collections.Generic;
using ItemList.Api.Controllers;
using ItemList.Api.Models;
using NUnit.Framework;
using System.Web.Http;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ItemList.Api.Helpers;
using NSubstitute;

namespace ItemList.Api.Tests
{
    [TestFixture]
    public class ItemsControllerTests
    {
        private ItemsController _itemsController;

        [SetUp]
        public void SetUp()
        {
            var itemUrlHelperMock = Substitute.For<IItemUrlHelper>();
            itemUrlHelperMock.GetUrl(Arg.Any<Guid>()).Returns(info => info.Arg<Guid>().ToString());

            _itemsController = new ItemsController(itemUrlHelperMock)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public async Task Get_WithoutParameters_ReturnsAllItems()
        {
            var expectedItems = new List<Item> {
                new Item { Id = new Guid("7383243d-9230-4a6c-94ea-122e151208ca"), Value = "text1" },
                new Item { Id = new Guid("83aa9154-2b5f-49b7-b7af-25cab7bf2159"), Value = "text2" }
            };

            var result = await _itemsController.Get();
            var response = await result.ExecuteAsync(CancellationToken.None);
            IEnumerable<Item> actualItems;
            response.TryGetContentValue(out actualItems);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItems, Is.EqualTo(expectedItems).AsCollection.UsingItemComparer());
        }

        [Test]
        public async Task Get_ExistingId_ReturnsDummyItem()
        {
            var id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7");
            var expectedItem = new Item { Id = id, Value = "text3" };

            var result = await _itemsController.Get(id);
            var response = await result.ExecuteAsync(CancellationToken.None);
            Item actualItem;
            response.TryGetContentValue(out actualItem);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemComparer());
        }

        [Test]
        public async Task Delete_ExistingId_ReturnsNoContent()
        {
            var id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7");

            var result = await _itemsController.Delete(id);
            var response = await result.ExecuteAsync(CancellationToken.None);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task Put_ExitingId_ReturnsNoContent()
        {
            var puttedItem = new Item { Id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7"), Value = "text3" };

            var result = await _itemsController.Put(puttedItem);
            var response = await result.ExecuteAsync(CancellationToken.None);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task Post_ValidItem_ReturnsDummyItemAndUrl()
        {
            const string expectedId = "5081544A-5584-4449-B0CD-72B2BFF0AF30";
            const string ueid = "Hello Susan";
            const string value = "text4";
            var expectedItem = new Item { Id = new Guid(expectedId), Ueid = ueid, Value = value };
            var postedItem = new Item { Ueid = ueid, Value = value };

            var result = await _itemsController.Post(postedItem);
            var response = await result.ExecuteAsync(CancellationToken.None);
            Item actualItem;
            response.TryGetContentValue(out actualItem);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location.ToString(), Does.EndWith(expectedId).IgnoreCase);
            Assert.That(actualItem, Is.EqualTo(expectedItem).UsingItemComparer());
        }
    }
}
