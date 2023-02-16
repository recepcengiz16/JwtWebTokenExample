using JwtWebTokenExample.Manager.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtWebTokenExample.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using JwtWebTokenExample.Models.ViewModel;

namespace JwtWebTokenExample.Manager.Services
{
    public class UserManager
    {
        public async Task<SigninViewModel> GetSigninModel(IConfiguration configuration,JwtAppUser user)
        {
            JwtSecurityTokenHandler handler = new ();
            var stringToken = new JwtTokenGenerator().GenerateToken(configuration, user);
            var token = handler.ReadJwtToken(stringToken);

            var claimsIdentity = new ClaimsIdentity(
                   token.Claims, JwtBearerDefaults.AuthenticationScheme);

            var authProps = new AuthenticationProperties()
            {
                IsPersistent = true,//client bu tokenı hatırlasın
            };

            return new SigninViewModel() { AuthenticationProperties_= authProps,ClaimsIdentity_=claimsIdentity };
            
        }
    }
}
