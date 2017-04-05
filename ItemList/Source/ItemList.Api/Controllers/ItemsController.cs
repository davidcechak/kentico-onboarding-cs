using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ItemList.Contracts.Api;
using ItemList.Contracts.Database;
using ItemList.Contracts.Models;
using ItemList.Contracts.Services;

namespace ItemList.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly IItemUrlHelper _itemUrlHelper;
        private readonly IItemsRepository _repository;
        private readonly IItemStoringService _itemStoringService;

        public ItemsController(IItemUrlHelper itemUrlHelper, IItemsRepository repository, IItemStoringService itemStoringService)
        {
            _itemUrlHelper = itemUrlHelper;
            _repository = repository;
            _itemStoringService = itemStoringService;
        }

        public async Task<IHttpActionResult> GetAsync() 
            => Ok(await _repository.GetAllAsync());


        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("You have passed in invalid id.");
            }
            return Ok(await _repository.GetAsync(id));
        }


        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("You have passed in invalid item.");
            }
            var newItem = await _itemStoringService.StoreNewItemAsync(item);

            var location = _itemUrlHelper.GetUrl(newItem.Id);

            return Created(location, newItem);
        }


        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }


        public async Task<IHttpActionResult> PutAsync(Item item)
        {
            await _repository.UpdateAsync(item);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
