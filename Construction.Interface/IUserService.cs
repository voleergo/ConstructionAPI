using Construction.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(long id);
        Task<User> GetUserByNameAsync(string userName);
        Task<User> ValidateUserAsync(string userName, string password);
        Task<IEnumerable<User>> GetUsersByRoleAsync(long roleId);
        Task<long> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(long id);
    }
}
