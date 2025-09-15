using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        protected override string TableName => "Customers";
        protected override string IdColumn => "ID_Customer";

        public CustomerRepository(DatabaseConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public override async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await GetMultipleStoredProcAsync("usp_Customer_GetAll");
        }

        public override async Task<Customer?> GetByIdAsync(long id)
        {
            return await GetSingleStoredProcAsync("usp_Customer_GetById", new { ID_Customer = id });
        }

        public override async Task<long> AddAsync(Customer customer)
        {
            return await ExecuteInsertStoredProcAsync("usp_Customer_Insert", new
            {
                customer.CustomerCode,
                customer.CustomerName,
                customer.CreatedOn,
                customer.CreatedBy,
                customer.ModifiedOn,
                customer.ModifiedBy
            });
        }

        public override async Task<bool> UpdateAsync(Customer customer)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Customer_Update", new
            {
                customer.ID_Customer,
                customer.CustomerCode,
                customer.CustomerName,
                customer.ModifiedOn,
                customer.ModifiedBy
            });
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Customer_Delete", new { ID_Customer = id });
        }

        public async Task<Customer?> GetByCustomerCodeAsync(string customerCode)
        {
            return await GetSingleStoredProcAsync("usp_Customer_GetByCode", new { CustomerCode = customerCode });
        }

        public async Task<IEnumerable<Customer>> SearchByNameAsync(string customerName)
        {
            return await GetMultipleStoredProcAsync("usp_Customer_SearchByName", new { CustomerName = customerName });
        }

        protected override Customer MapFromReader(IDataReader reader)
        {
            return new Customer
            {
                ID_Customer = reader.GetInt64(reader.GetOrdinal("ID_Customer")),
                CustomerCode = reader.GetString(reader.GetOrdinal("CustomerCode")),
                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),               
                CreatedBy = GetNullableLong(reader, "CreatedBy"),
                ModifiedOn = GetNullableDateTime(reader, "ModifiedOn"),
                ModifiedBy = GetNullableLong(reader, "ModifiedBy")
            };
        }
    }
}
