using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<Supplier> GetBySupplierCodeAsync(string supplierCode);
        Task<IEnumerable<Supplier>> SearchByNameAsync(string supplierName);
    }
}
