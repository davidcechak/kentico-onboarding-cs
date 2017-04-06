using System.Threading.Tasks;
using ItemList.Contracts.Models;

namespace ItemList.Contracts.Services
{
    public interface IItemStoringService
    {
        Task<Item> StoreNewItemAsync(Item item);
    }
}