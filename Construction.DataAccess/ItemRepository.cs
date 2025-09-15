using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        protected override string TableName => "Items";
        protected override string IdColumn => "ID_Item";

        public ItemRepository(DatabaseConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public override async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await GetMultipleStoredProcAsync("usp_Item_GetAll");
        }

        public override async Task<Item> GetByIdAsync(long id)
        {
            return await GetSingleStoredProcAsync("usp_Item_GetById", new { ID_Item = id });
        }

        public override async Task<long> AddAsync(Item item)
        {
            return await ExecuteInsertStoredProcAsync("usp_Item_Insert", new
            {
                item.ItemCode,
                item.ItemName,
                item.ItemType,
                item.CreatedBy,
                item.CreatedOn,
                item.UpdatedBy,
                item.UpdatedOn
            });
        }

        public override async Task<bool> UpdateAsync(Item item)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Item_Update", new
            {
                item.ID_Item,
                item.ItemCode,
                item.ItemName,
                item.ItemType,
                item.UpdatedBy,
                item.UpdatedOn
            });
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Item_Delete", new { ID_Item = id });
        }

        public async Task<Item> GetByItemCodeAsync(string itemCode)
        {
            return await GetSingleStoredProcAsync("usp_Item_GetByCode", new { ItemCode = itemCode });
        }

        public async Task<IEnumerable<Item>> GetItemsByTypeAsync(string itemType)
        {
            return await GetMultipleStoredProcAsync("usp_Item_GetByType", new { ItemType = itemType });
        }

        protected override Item MapFromReader(IDataReader reader)
        {
            return new Item
            {
                ID_Item = reader.GetInt64("ID_Item"),
                ItemCode = reader.GetString("ItemCode"),
                ItemName = reader.GetString("ItemName"),
                ItemType = GetNullableString(reader, "ItemType"),
                CreatedBy = GetNullableLong(reader, "CreatedBy"),
                CreatedOn = reader.GetDateTime("CreatedOn"),
                UpdatedBy = GetNullableLong(reader, "UpdatedBy"),
                UpdatedOn = GetNullableDateTime(reader, "UpdatedOn")
            };
        }
    }
}
