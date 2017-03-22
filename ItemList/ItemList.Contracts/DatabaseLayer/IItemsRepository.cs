using System.Collections.Generic;
using System.Threading.Tasks;
using ItemList.Contracts.Models;

namespace ItemList.Contracts.DatabaseLayer
{
    public interface ItemsRepository
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item> Get();
        Task<bool> Create();
        Task<bool> Update();
        Task<bool> Delete();
    }
}