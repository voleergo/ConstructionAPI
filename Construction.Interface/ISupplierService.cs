using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier> GetSupplierByIdAsync(long id);
        Task<Supplier> GetSupplierByCodeAsync(string supplierCode);
        Task<IEnumerable<Supplier>> SearchSuppliersByNameAsync(string supplierName);
        Task<long> CreateSupplierAsync(Supplier supplier);
        Task<bool> UpdateSupplierAsync(Supplier supplier);
        Task<bool> DeleteSupplierAsync(long id);
    }
}
