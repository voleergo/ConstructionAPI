using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetByCustomerCodeAsync(string customerCode);
        Task<IEnumerable<Customer>> SearchByNameAsync(string customerName);
    }
}
