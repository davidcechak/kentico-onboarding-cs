using System.Threading.Tasks;
using ItemList.Contracts.Api;
using ItemList.Contracts.DatabaseLayer;
using ItemList.Contracts.Models;
using ItemList.Contracts.ServiceLayer;

namespace ItemList.ServiceLayer.Items
{
    internal class ItemStoringService
    {
        private readonly IIdentifierService _identifierService;
        private readonly IItemsRepository _itemRepository;

        public ItemStoringService(IIdentifierService identifierService, IItemsRepository itemRepository)
        {
            _identifierService = identifierService;
            _itemRepository = itemRepository;
        }

        public async Task<Item> StoreNewItemAsync(Item item)
        {
            Item newItem = CreateItemWithNewId(item);
            await _itemRepository.Create(newItem);

            return newItem;
        }

        private Item CreateItemWithNewId(Item item) => new Item
        {
            Id = _identifierService.GetIdentifier(),
            Ueid = item.Ueid,
            Value = item.Value
        };
    }
}