using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(long id);
        Task<Item> GetItemByCodeAsync(string itemCode);
        Task<IEnumerable<Item>> GetItemsByTypeAsync(string itemType);
        Task<long> CreateItemAsync(Item item);
        Task<bool> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(long id);
    }
}
