using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(long id);
        Task<Customer> GetCustomerByCodeAsync(string customerCode);
        Task<IEnumerable<Customer>> SearchCustomersByNameAsync(string customerName);
        Task<long> CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(long id);
    }
}
