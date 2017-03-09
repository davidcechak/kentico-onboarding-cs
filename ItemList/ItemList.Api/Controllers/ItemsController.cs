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
        private static List<Item> Items;

        public IHttpActionResult GetItem(int id)
        {
            var Item = Items.Find((item) => item.Id == id);

            if (Item == null)
            {
                return NotFound();
            }
            return Ok(Item);
        }


        public IHttpActionResult GetAllItems()
        {
            return Ok(Items);
        }


        public IHttpActionResult AddItem(Item item)
        {
            if (ModelState.IsValid)
            {
                Items.Add(item);
                return Ok();
            }
            return NotFound();
        }


    }
}
