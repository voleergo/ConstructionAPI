using Construction.DomainModel;
using Construction.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
        {
            return await _userRoleRepository.GetAllAsync();
        }

        public async Task<UserRole> GetUserRoleByIdAsync(long id)
        {
            return await _userRoleRepository.GetByIdAsync(id);
        }

        public async Task<UserRole> GetUserRoleByNameAsync(string roleName)
        {
            return await _userRoleRepository.GetByRoleNameAsync(roleName);
        }

        public async Task<long> CreateUserRoleAsync(UserRole userRole)
        {
            userRole.CreatedOn = DateTime.Now;
            return await _userRoleRepository.AddAsync(userRole);
        }

        public async Task<bool> UpdateUserRoleAsync(UserRole userRole)
        {
            userRole.ModifiedOn = DateTime.Now;
            return await _userRoleRepository.UpdateAsync(userRole);
        }

        public async Task<bool> DeleteUserRoleAsync(long id)
        {
            return await _userRoleRepository.DeleteAsync(id);
        }
    }
}
