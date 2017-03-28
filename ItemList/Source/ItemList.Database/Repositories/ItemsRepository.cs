using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemList.Contracts.DatabaseLayer;
using ItemList.Contracts.Models;

namespace ItemList.DatabaseLayer.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            IEnumerable<Item> items = new List<Item>()
            {
                new Item {Id = new Guid("7383243d-9230-4a6c-94ea-122e151208ca"), Value = "text1"},
                new Item {Id = new Guid("83aa9154-2b5f-49b7-b7af-25cab7bf2159"), Value = "text2"}
            };
            return await Task.FromResult(items);
        }

        public async Task<Item> GetAsync(Guid id)
        {
            return await Task.FromResult(new Item { Id = new Guid("331c43f5-11af-43a4-83d1-7d949ae5a8d7"), Value = "text3" });
        }

        public async Task CreateAsync(Item item)
        {
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Item item)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            await Task.CompletedTask;
        }
    }
}