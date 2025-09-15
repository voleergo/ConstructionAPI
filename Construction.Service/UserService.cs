using Construction.DomainModel;
using Construction.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Construction.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            return await _userRepository.GetByUserNameAsync(userName);
        }

        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            // Hash the password for comparison
            string hashedPassword = HashPassword(password);
            return await _userRepository.ValidateUserAsync(userName, hashedPassword);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(long roleId)
        {
            return await _userRepository.GetUsersByRoleAsync(roleId);
        }

        public async Task<long> CreateUserAsync(User user)
        {
            // Hash the password before storing
            user.UserPassword = HashPassword(user.UserPassword);
            user.CreatedOn = DateTime.Now;
            
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            user.ModifiedOn = DateTime.Now;
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
