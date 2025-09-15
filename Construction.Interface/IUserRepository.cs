using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserNameAsync(string userName);
        Task<User> ValidateUserAsync(string userName, string password);
        Task<IEnumerable<User>> GetUsersByRoleAsync(long roleId);
    }
}
