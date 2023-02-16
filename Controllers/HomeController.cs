using JwtWebTokenExample.Filters;
using JwtWebTokenExample.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JwtWebTokenExample.Models.Entity;
using JwtWebTokenExample.Manager.Services;

namespace JwtWebTokenExample.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager _userManager;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, UserManager userManager)
        {
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
        }

        [Auth]
        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(JwtAppUser user)
        {
            //Burada veri tabanından kontrol yapılır. Varsa sign in yapılır.
         
            var signinModel = await _userManager.GetSigninModel(_configuration, user);

            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(signinModel.ClaimsIdentity_), signinModel.AuthenticationProperties_);
            return RedirectToAction("Index");
        }


        // [Authorize(Roles ="Admin")] //Buraya yetkisi varsa girsin mesela.
        [Auth]
        public IActionResult Privacy()
        {
            return View();
        }   

        [Auth]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }


        [Auth]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [Auth]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}