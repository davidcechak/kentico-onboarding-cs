using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ItemList.Api.Models;

namespace ItemList.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private static List<Item> Items;

        public IHttpActionResult GetItem(string id)
        {
            var Item = Items.Find((item) => item.Id == id);

            if (Item != null) ? Ok(Item) : NotFound();
        }
    }
}
