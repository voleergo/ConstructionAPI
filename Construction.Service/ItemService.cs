using Construction.DomainModel;
using Construction.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Service
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _itemRepository.GetAllAsync();
        }

        public async Task<Item> GetItemByIdAsync(long id)
        {
            return await _itemRepository.GetByIdAsync(id);
        }

        public async Task<Item> GetItemByCodeAsync(string itemCode)
        {
            return await _itemRepository.GetByItemCodeAsync(itemCode);
        }

        public async Task<IEnumerable<Item>> GetItemsByTypeAsync(string itemType)
        {
            return await _itemRepository.GetItemsByTypeAsync(itemType);
        }

        public async Task<long> CreateItemAsync(Item item)
        {
            item.CreatedOn = DateTime.Now;
            return await _itemRepository.AddAsync(item);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            item.UpdatedOn = DateTime.Now;
            return await _itemRepository.UpdateAsync(item);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            return await _itemRepository.DeleteAsync(id);
        }
    }
}
