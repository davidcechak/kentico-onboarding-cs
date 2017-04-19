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
        private const string CollectionName = "Items";

        public ItemsRepository(IDatabaseConfiguration databaseConfiguration)
        {
            var databaseName = MongoUrl.Create(databaseConfiguration.DefaultConnectionString).DatabaseName;
            var client = new MongoClient(databaseConfiguration.DefaultConnectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Item>(CollectionName);
        }
        public async Task<IEnumerable<Item>> GetAllAsync() 
            => (await _collection.FindAsync(FilterDefinition<Item>.Empty)).ToEnumerable();

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