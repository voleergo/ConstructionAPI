using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DomainModel.User;

namespace Construction.Interface
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToekn(UsersModel model);
        string GenerateToken();
    }
}
