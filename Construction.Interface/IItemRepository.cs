using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Task<Item> GetByItemCodeAsync(string itemCode);
        Task<IEnumerable<Item>> GetItemsByTypeAsync(string itemType);
    }
}
