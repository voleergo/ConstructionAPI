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

        protected override Supplier MapFromReader(IDataReader dataReader)
        {
            return new Supplier
            {
                ID_Supplier = Convert.ToInt64(dataReader["ID_Supplier"]),
                SupplierCode = Convert.ToString(dataReader["SupplierCode"]) ?? string.Empty,
                SupplierName = Convert.ToString(dataReader["SupplierName"]) ?? string.Empty,
                CreatedOn = Convert.ToDateTime(dataReader["CreatedOn"]),
                CreatedBy = dataReader["CreatedBy"] == DBNull.Value ? null : Convert.ToInt64(dataReader["CreatedBy"]),
                ModifiedOn = dataReader["ModifiedOn"] == DBNull.Value ? null : Convert.ToDateTime(dataReader["ModifiedOn"]),
                ModifiedBy = dataReader["ModifiedBy"] == DBNull.Value ? null : Convert.ToInt64(dataReader["ModifiedBy"])
            };
        }
    }
}
