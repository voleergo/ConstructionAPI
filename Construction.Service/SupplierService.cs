using Construction.DomainModel;
using Construction.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(long id)
        {
            return await _supplierRepository.GetByIdAsync(id);
        }

        public async Task<Supplier> GetSupplierByCodeAsync(string supplierCode)
        {
            return await _supplierRepository.GetBySupplierCodeAsync(supplierCode);
        }

        public async Task<IEnumerable<Supplier>> SearchSuppliersByNameAsync(string supplierName)
        {
            return await _supplierRepository.SearchByNameAsync(supplierName);
        }

        public async Task<long> CreateSupplierAsync(Supplier supplier)
        {
            supplier.CreatedOn = DateTime.Now;
            return await _supplierRepository.AddAsync(supplier);
        }

        public async Task<bool> UpdateSupplierAsync(Supplier supplier)
        {
            supplier.ModifiedOn = DateTime.Now;
            return await _supplierRepository.UpdateAsync(supplier);
        }

        public async Task<bool> DeleteSupplierAsync(long id)
        {
            return await _supplierRepository.DeleteAsync(id);
        }
    }
}
