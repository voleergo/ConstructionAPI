using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Construction.Common;
using Construction.DomainModel.User;
using Construction.Interface;

namespace Construction.Service
{
    public class TokenService: ITokenService
    {
        public JwtSecurityToken GenerateToekn(UsersModel model)
        {
            // GetUserRoles
            IList<string> userRoles = new List<string>();
            userRoles.Add("Administrator:");
            userRoles.Add("User:");

            var authClaims = new List<Claim>
            {
                    new Claim(JwtRegisteredClaimNames.Name, model.UserName??""),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sid, Cryptography.AESEncryption(Convert.ToString(model.ID_Users))),
                    new Claim(JwtRegisteredClaimNames.Email, Convert.ToString(model.Email??""))
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecurityParams.jwtSecret));
            var token = new JwtSecurityToken(
                issuer: JwtSecurityParams.jwtValidIssuer,
                audience: JwtSecurityParams.jwtValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        public string GenerateToken()
        {
            // GetUserRoles
            //IList<string> userRoles = new List<string>();
            //userRoles.Add("Administrator:");
            //userRoles.Add("User:");

            //var authClaims = new List<Claim>
            //{
            //        new Claim(JwtRegisteredClaimNames.Name, "TMCHREG"??""),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //        new Claim(JwtRegisteredClaimNames.Sid, Cryptography.AESEncryption(Convert.ToString("123"))),
            //        //new Claim(JwtRegisteredClaimNames.Email, Convert.ToString(model.Email??""))
            //};
            //foreach (var userRole in userRoles)
            //{
            //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //}
            var tokenHandler = new JwtSecurityTokenHandler();

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecurityParams.jwtSecret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = JwtSecurityParams.jwtValidIssuer,
                Audience = JwtSecurityParams.jwtValidAudience,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            };
            var tokens = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(tokens);
            return tokenString;
        }

    }
}
