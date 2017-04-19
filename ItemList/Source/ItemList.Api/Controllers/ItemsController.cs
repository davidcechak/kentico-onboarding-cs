using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
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

        private void ValidateItem(Item item, ModelStateDictionary modelState)
        {
            if (item == null)
            {
                modelState.AddModelError(nameof(item), "Item is not correct.");
                return;
            }
            if (item.Id != Guid.Empty)
            {
                modelState.AddModelError(nameof(item.Id), "Id of item will be overwritten so should be empty.");
            }
            if (string.IsNullOrEmpty(item.Ueid))
            {
                modelState.AddModelError(nameof(item.Ueid), "Ueid should not be empty.");
            }
            if (string.IsNullOrEmpty(item.Value))
            {
                modelState.AddModelError(nameof(item.Value), "Value should not be empty.");
            }
            if (item.Value != null && item.Value.Length > 200)
            {
                modelState.AddModelError(nameof(item.Value), "Value cannot exceed 200 characters.");
            }
        }

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
                return NotFound();
            }
            var result = await _repository.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            ValidateItem(item, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newItem = await _itemStoringService.StoreNewItemAsync(item);

            var location = _itemUrlHelper.GetUrl(newItem.Id);

            return Created(location, newItem);
        }

        // dummy implementation
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // dummy implementation
        public async Task<IHttpActionResult> PutAsync(Item item)
        {
            await _repository.UpdateAsync(item);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
