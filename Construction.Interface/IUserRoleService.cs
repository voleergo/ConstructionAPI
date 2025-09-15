using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
        Task<UserRole> GetUserRoleByIdAsync(long id);
        Task<UserRole> GetUserRoleByNameAsync(string roleName);
        Task<long> CreateUserRoleAsync(UserRole userRole);
        Task<bool> UpdateUserRoleAsync(UserRole userRole);
        Task<bool> DeleteUserRoleAsync(long id);
    }
}
