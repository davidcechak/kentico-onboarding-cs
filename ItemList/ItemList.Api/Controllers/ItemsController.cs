using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using ItemList.Api.Models;

namespace ItemList.Api.Controllers
{
    public class ItemsController : ApiController
    {
        public IEnumerable<Item> Get()
        {
            yield return new Item { Id = new Guid("7383243d-9230-4a6c-94ea-122e151208ca"), Value = "text1" };
            yield return new Item { Id = new Guid("83aa9154-2b5f-49b7-b7af-25cab7bf2159"), Value = "text2" };
        }


        public IHttpActionResult Get(Guid id)
        {
            return Ok(new Item { Id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7"), Value = "text3" });
        }


        public IHttpActionResult Post(Item item)
        {
            var location = Url.Link(null, new
            {
                Controller = nameof(ItemsController).Replace("Controller", String.Empty),
                Action = "Get",
                Id = "331c43f5-11af-43a4-83d1-7d949ae5a8d7"
            });

            return Created(location, new Item { Id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7"), Value = item.Value });
        }
    }
}
