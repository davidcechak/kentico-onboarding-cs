using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemList.Contracts.Models;

namespace ItemList.Contracts.DatabaseLayer
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();

        Task<Item> GetAsync(Guid id);

        Task CreateAsync(Item item);

        Task UpdateAsync(Item item);

        Task DeleteAsync(Guid id);
    }
}