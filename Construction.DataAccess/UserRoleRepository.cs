using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        protected override string TableName => "UserRoles";
        protected override string IdColumn => "ID_UserRole";

        public UserRoleRepository(DatabaseConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public override async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await GetMultipleStoredProcAsync("usp_UserRole_GetAll");
        }

        public override async Task<UserRole> GetByIdAsync(long id)
        {
            return await GetSingleStoredProcAsync("usp_UserRole_GetById", new { ID_UserRole = id });
        }

        public override async Task<long> AddAsync(UserRole userRole)
        {
            return await ExecuteInsertStoredProcAsync("usp_UserRole_Insert", new
            {
                userRole.RoleName,
                userRole.CreatedOn,
                userRole.CreatedBy,
                userRole.ModifiedOn,
                userRole.ModifiedBy
            });
        }

        public override async Task<bool> UpdateAsync(UserRole userRole)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_UserRole_Update", new
            {
                userRole.ID_UserRole,
                userRole.RoleName,
                userRole.ModifiedOn,
                userRole.ModifiedBy
            });
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_UserRole_Delete", new { ID_UserRole = id });
        }

        public async Task<UserRole> GetByRoleNameAsync(string roleName)
        {
            return await GetSingleStoredProcAsync("usp_UserRole_GetByName", new { RoleName = roleName });
        }

        protected override UserRole MapFromReader(IDataReader reader)
        {
            return new UserRole
            {
                ID_UserRole = reader.GetInt64("ID_UserRole"),
                RoleName = reader.GetString("RoleName"),
                CreatedOn = reader.GetDateTime("CreatedOn"),
                CreatedBy = GetNullableLong(reader, "CreatedBy"),
                ModifiedOn = GetNullableDateTime(reader, "ModifiedOn"),
                ModifiedBy = GetNullableLong(reader, "ModifiedBy")
            };
        }
    }
}
