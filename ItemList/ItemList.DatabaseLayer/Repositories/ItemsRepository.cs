using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemList.Contracts.DatabaseLayer;
using ItemList.Contracts.Models;

namespace ItemList.DatabaseLayer.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        public Task<IEnumerable<Item>> GetAll()
        {
            IEnumerable<Item> items = new List<Item>()
            {
                new Item {Id = new Guid("7383243d-9230-4a6c-94ea-122e151208ca"), Value = "text1"},
                new Item {Id = new Guid("83aa9154-2b5f-49b7-b7af-25cab7bf2159"), Value = "text2"}
            };
            return Task.FromResult(items);
        }

        public Task<Item> Get()
        {
            return Task.FromResult(new Item { Id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7"), Value = "text3" });
        }

        public Task Create()
        {
            return Task.CompletedTask;
        }

        public Task Update()
        {
            return Task.CompletedTask;
        }

        public Task Delete()
        {
            return Task.CompletedTask;
        }
    }
}