using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ItemList.Api.Helpers;
using ItemList.Contracts.Models;

namespace ItemList.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly IItemUrlHelper _itemUrlHelper;

        public ItemsController()
        {
            _itemUrlHelper = new ItemUrlHelper(Url);
        }

        public ItemsController(IItemUrlHelper itemUrlHelper)
        {
            _itemUrlHelper = itemUrlHelper;
        }

        public async Task<IHttpActionResult> Get()
        {
            var items = new[]
            {
                new Item {Id = new Guid("7383243d-9230-4a6c-94ea-122e151208ca"), Value = "text1"},
                new Item {Id = new Guid("83aa9154-2b5f-49b7-b7af-25cab7bf2159"), Value = "text2"}
            };
            return Ok(await Task.FromResult(items));
        }


        public async Task<IHttpActionResult> Get(Guid id)
        {
            var item = await Task.FromResult(new Item { Id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7"), Value = "text3" });
            return Ok(item);
        }


        public async Task<IHttpActionResult> Post(Item item)
        {
            item = await Task.FromResult(new Item { Id = new Guid("5081544A-5584-4449-B0CD-72B2BFF0AF30"), Ueid = "Hello Susan", Value = "text4" });

            var location = _itemUrlHelper.GetUrl(item.Id);

            return Created(location, item);
        }


        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await Task.CompletedTask;
            return StatusCode(HttpStatusCode.NoContent);
        }


        public async Task<IHttpActionResult> Put(Item item)
        {
            await Task.CompletedTask;
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
