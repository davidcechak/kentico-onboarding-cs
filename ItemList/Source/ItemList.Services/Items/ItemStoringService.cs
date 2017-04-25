using System;
using System.Threading.Tasks;
using ItemList.Contracts.Database;
using ItemList.Contracts.Models;
using ItemList.Contracts.Services;

namespace ItemList.Services.Items
{
    internal class ItemStoringService : IItemStoringService
    {
        private readonly IIdentifierService _identifierService;
        private readonly IItemsRepository _itemRepository;
        private readonly ITimeService _timeService;

        public ItemStoringService(IIdentifierService identifierService, IItemsRepository itemRepository, ITimeService timeService)
        {
            _identifierService = identifierService;
            _itemRepository = itemRepository;
            _timeService = timeService;
        }

        public async Task<Item> StoreNewItemAsync(Item item)
        {
            Item newItem = CreateItemWithNewId(item, _timeService.Now());
            await _itemRepository.CreateAsync(newItem);

            return newItem;
        }

        private Item CreateItemWithNewId(Item item, DateTime creationTime) => new Item
        {
            Id = _identifierService.GetIdentifier(),
            Ueid = item.Ueid,
            Value = item.Value,
            Created = creationTime,
            LastUpdated = creationTime
        };
    }
}