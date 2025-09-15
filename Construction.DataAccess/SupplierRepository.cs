using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        protected override string TableName => "Suppliers";
        protected override string IdColumn => "ID_Supplier";

        public SupplierRepository(DatabaseConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public override async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await GetMultipleStoredProcAsync("usp_Supplier_GetAll");
        }

        public override async Task<Supplier?> GetByIdAsync(long id)
        {
            return await GetSingleStoredProcAsync("usp_Supplier_GetById", new { ID_Supplier = id });
        }

        public override async Task<long> AddAsync(Supplier supplier)
        {
            return await ExecuteInsertStoredProcAsync("usp_Supplier_Insert", new
            {
                supplier.SupplierCode,
                supplier.SupplierName,
                supplier.CreatedOn,
                supplier.CreatedBy,
                supplier.ModifiedOn,
                supplier.ModifiedBy
            });
        }

        public override async Task<bool> UpdateAsync(Supplier supplier)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Supplier_Update", new
            {
                supplier.ID_Supplier,
                supplier.SupplierCode,
                supplier.SupplierName,
                supplier.ModifiedOn,
                supplier.ModifiedBy
            });
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Supplier_Delete", new { ID_Supplier = id });
        }

        public async Task<Supplier?> GetBySupplierCodeAsync(string supplierCode)
        {
            return await GetSingleStoredProcAsync("usp_Supplier_GetByCode", new { SupplierCode = supplierCode });
        }

        public async Task<IEnumerable<Supplier>> SearchByNameAsync(string supplierName)
        {
            return await GetMultipleStoredProcAsync("usp_Supplier_SearchByName", new { SupplierName = supplierName });
        }

        protected override Supplier MapFromReader(IDataReader reader)
        {
            return new Supplier
            {
                ID_Supplier = reader.GetInt64(reader.GetOrdinal("ID_Supplier")),
                SupplierCode = reader.GetString(reader.GetOrdinal("SupplierCode")),
                SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                CreatedBy = GetNullableLong(reader, "CreatedBy"),
                ModifiedOn = GetNullableDateTime(reader, "ModifiedOn"),
                ModifiedBy = GetNullableLong(reader, "ModifiedBy")
            };
        }
    }
}
