﻿using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ItemList.Contracts.Api;
using ItemList.Contracts.DatabaseLayer;
using ItemList.Contracts.Models;
using ItemList.Contracts.ServiceLayer;

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
            item.Id = _identifierService.GetIdentifier();
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
