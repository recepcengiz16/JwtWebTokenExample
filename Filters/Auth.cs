using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace JwtWebTokenExample.Filters
{
    public class Auth : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "RoleId") ==null)                  
            {               
                context.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}
