using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemList.Contracts.Api;
using ItemList.Contracts.Database;
using ItemList.Contracts.Models;
using MongoDB.Driver;

namespace ItemList.Database.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly IMongoCollection<Item> _collection;

        public ItemsRepository(IDatabaseConfiguration databaseConfiguration)
        {
            var client = new MongoClient(databaseConfiguration.GetConnectionString());
            var database = client.GetDatabase("itemlist");
            _collection = database.GetCollection<Item>("Items");
        }
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            var result = await _collection.FindAsync(FilterDefinition<Item>.Empty);
            return result.ToEnumerable();
        }

        public async Task<Item> GetAsync(Guid id)
        {
            var result = await _collection.FindAsync(item => item.Id == id);
            return result.FirstOrDefault();
        }

        public async Task CreateAsync(Item item)
        {
            await _collection.InsertOneAsync(item);
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