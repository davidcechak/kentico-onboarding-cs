using System.Collections.Generic;
using System.Threading.Tasks;
using ItemList.Contracts.Models;

namespace ItemList.Contracts.DatabaseLayer
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item> Get();
        Task Create();
        Task Update();
        Task Delete();
    }
}