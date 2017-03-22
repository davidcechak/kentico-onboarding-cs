using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ItemList.Api.Helpers;
using ItemList.Contracts.DatabaseLayer;
using ItemList.Contracts.Models;

namespace ItemList.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly IItemUrlHelper _itemUrlHelper;
        private IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _itemUrlHelper = new ItemUrlHelper(Url);
            _repository = repository;
        }

        public ItemsController(IItemUrlHelper itemUrlHelper, IItemsRepository repository)
        {
            _itemUrlHelper = itemUrlHelper;
            _repository = repository;
        }

        public async Task<IHttpActionResult> Get()
        {
            var items = await _repository.GetAll();
            return Ok(items);
        }


        public async Task<IHttpActionResult> Get(Guid id)
        {
            var item = await _repository.Get(id);
            return Ok(item);
        }


        public async Task<IHttpActionResult> Post(Item item)
        {
            item.Id = new Guid("5081544A-5584-4449-B0CD-72B2BFF0AF30");
            await _repository.Create(item);

            var location = _itemUrlHelper.GetUrl(item.Id);

            return Created(location, item);
        }


        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }


        public async Task<IHttpActionResult> Put(Item item)
        {
            await _repository.Update(item);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
