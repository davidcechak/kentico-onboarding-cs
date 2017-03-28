using System.Threading.Tasks;
using ItemList.Contracts.Database;
using ItemList.Contracts.Models;
using ItemList.Contracts.Services;

namespace ItemList.Services.Items
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
            await _itemRepository.CreateAsync(newItem);

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