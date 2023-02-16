
using JwtWebTokenExample.Manager.Enums;
using JwtWebTokenExample.Models.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtWebTokenExample.Manager.Helpers
{
    public class JwtTokenGenerator
    {
        public string GenerateToken(IConfiguration configuration,JwtAppUser user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.LogicalRef.ToString()), //Userın Id bilgisi girilir string olarak. Zorunlu değil
                new Claim("Username", user.Username),
                new Claim("RoleId",user.RoleId.ToString()),
            };

            JwtSecurityToken securityToken = new JwtSecurityToken(issuer: configuration["Token:Issuer"], claims: claims, audience: configuration["Token:Audience"], notBefore: DateTime.UtcNow, expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["Token:Expire"])), signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(securityToken);
        }
    }
}
