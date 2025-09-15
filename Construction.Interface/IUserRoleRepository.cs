using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        Task<UserRole> GetByRoleNameAsync(string roleName);
    }
}
