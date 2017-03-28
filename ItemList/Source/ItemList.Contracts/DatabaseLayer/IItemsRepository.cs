using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemList.Contracts.Models;

namespace ItemList.Contracts.DatabaseLayer
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetAll();

        Task<Item> Get(Guid id);

        Task Create(Item item);

        Task Update(Item item);

        Task Delete(Guid id);
    }
}