using Construction.DomainModel;
using Construction.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(long id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> GetCustomerByCodeAsync(string customerCode)
        {
            return await _customerRepository.GetByCustomerCodeAsync(customerCode);
        }

        public async Task<IEnumerable<Customer>> SearchCustomersByNameAsync(string customerName)
        {
            return await _customerRepository.SearchByNameAsync(customerName);
        }

        public async Task<long> CreateCustomerAsync(Customer customer)
        {
            // Set audit fields
            customer.CreatedOn = DateTime.Now;
            // customer.CreatedBy should be set by the calling method based on current user context

            return await _customerRepository.AddAsync(customer);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            // Set audit fields
            customer.ModifiedOn = DateTime.Now;
            // customer.ModifiedBy should be set by the calling method based on current user context

            return await _customerRepository.UpdateAsync(customer);
        }

        public async Task<bool> DeleteCustomerAsync(long id)
        {
            return await _customerRepository.DeleteAsync(id);
        }
    }
}
