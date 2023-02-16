using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace JwtWebTokenExample.Models.ViewModel
{
    public class SigninViewModel
    {
        public ClaimsIdentity ClaimsIdentity_ { get; set; }
        public AuthenticationProperties AuthenticationProperties_ { get; set; }
    }
}
