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

        protected override User MapFromReader(IDataReader dataReader)
        {
            return new User
            {
                ID_Users = Convert.ToInt64(dataReader["ID_Users"]),
                UserName = Convert.ToString(dataReader["UserName"]) ?? string.Empty,
                UserPassword = Convert.ToString(dataReader["UserPassword"]) ?? string.Empty,
                MobileNumber = dataReader["MobileNumber"] == DBNull.Value ? null : Convert.ToString(dataReader["MobileNumber"]),
                Email = dataReader["Email"] == DBNull.Value ? null : Convert.ToString(dataReader["Email"]),
                FK_UserRoles = dataReader["FK_UserRoles"] == DBNull.Value ? null : Convert.ToInt64(dataReader["FK_UserRoles"]),
                UserStatus = dataReader["UserStatus"] == DBNull.Value ? null : Convert.ToString(dataReader["UserStatus"]),
                CreatedOn = Convert.ToDateTime(dataReader["CreatedOn"]),
                CreatedBy = dataReader["CreatedBy"] == DBNull.Value ? null : Convert.ToInt64(dataReader["CreatedBy"]),
                ModifiedOn = dataReader["ModifiedOn"] == DBNull.Value ? null : Convert.ToDateTime(dataReader["ModifiedOn"]),
                ModifiedBy = dataReader["ModifiedBy"] == DBNull.Value ? null : Convert.ToInt64(dataReader["ModifiedBy"])
            };
        }
    }
}
