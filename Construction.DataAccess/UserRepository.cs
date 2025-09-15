using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected override string TableName => "Users";
        protected override string IdColumn => "ID_Users";

        public UserRepository(DatabaseConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await GetMultipleStoredProcAsync("usp_User_GetAll");
        }

        public override async Task<User?> GetByIdAsync(long id)
        {
            return await GetSingleStoredProcAsync("usp_User_GetById", new { ID_Users = id });
        }

        public override async Task<long> AddAsync(User user)
        {
            return await ExecuteInsertStoredProcAsync("usp_User_Insert", new
            {
                user.UserName,
                user.UserPassword,
                user.MobileNumber,
                user.Email,
                user.FK_UserRoles,
                user.UserStatus,
                user.CreatedOn,
                user.CreatedBy,
                user.ModifiedOn,
                user.ModifiedBy
            });
        }

        public override async Task<bool> UpdateAsync(User user)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_User_Update", new
            {
                user.ID_Users,
                user.UserName,
                user.UserPassword,
                user.MobileNumber,
                user.Email,
                user.FK_UserRoles,
                user.UserStatus,
                user.ModifiedOn,
                user.ModifiedBy
            });
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_User_Delete", new { ID_Users = id });
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await GetSingleStoredProcAsync("usp_User_GetByUserName", new { UserName = userName });
        }

        public async Task<User?> ValidateUserAsync(string userName, string password)
        {
            return await GetSingleStoredProcAsync("usp_User_ValidateUser", new { UserName = userName, Password = password });
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(long roleId)
        {
            return await GetMultipleStoredProcAsync("usp_User_GetByRole", new { FK_UserRoles = roleId });
        }

        protected override User MapFromReader(IDataReader reader)
        {
            return new User
            {
                ID_Users = reader.GetInt64(reader.GetOrdinal("ID_Users")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                UserPassword = reader.GetString(reader.GetOrdinal("UserPassword")),
                MobileNumber = GetNullableString(reader, "MobileNumber"),
                Email = GetNullableString(reader, "Email"),
                FK_UserRoles = GetNullableLong(reader, "FK_UserRoles"),
                UserStatus = GetNullableString(reader, "UserStatus"),
                CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                CreatedBy = GetNullableLong(reader, "CreatedBy"),
                ModifiedOn = GetNullableDateTime(reader, "ModifiedOn"),
                ModifiedBy = GetNullableLong(reader, "ModifiedBy")
            };
        }
    }
}
