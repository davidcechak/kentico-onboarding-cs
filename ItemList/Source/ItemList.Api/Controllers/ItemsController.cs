using System;
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
        private readonly IIdentifierService _identifierService;

        public ItemsController(IItemUrlHelper itemUrlHelper, IItemsRepository repository, IIdentifierService identifierService)
        {
            _itemUrlHelper = itemUrlHelper;
            _repository = repository;
            _identifierService = identifierService;
        }

        public async Task<IHttpActionResult> GetAsync() 
            => Ok(await _repository.GetAllAsync());


        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await _repository.GetAsync(id));


        public async Task<IHttpActionResult> PostAsync(Item item)
        {
            item.Id = _identifierService.GetIdentifier();
            await _repository.CreateAsync(item);

            var location = _itemUrlHelper.GetUrl(item.Id);

            return Created(location, item);
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
