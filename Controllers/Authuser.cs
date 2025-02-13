using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ResumeManager.Models;
using ResumeManager.Service;
using System.Security.Claims;

namespace ResumeManager.Controllers
{
    public class Authuser : Controller
    {
        private readonly ApiUserService _apiService;
        public Authuser()
        {
            _apiService = new ApiUserService();
        }
       

        public IActionResult Login()
        {
            LoginRequestDTO obj = new LoginRequestDTO();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO obj)
        {
            LoginResponseDTO objResponse =  new LoginResponseDTO();
            objResponse = await _apiService.AuthenticateUser(obj);

            if (objResponse != null && objResponse.Token.ToString() != "")
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                identity.AddClaim(new Claim(ClaimTypes.Name,objResponse.UserDetails.Name));

                identity.AddClaim(new Claim(ClaimTypes.Role,objResponse.UserDetails.Role));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);


                HttpContext.Session.SetString("APIToken", objResponse.Token.ToString());
                return RedirectToAction("Index","Home");
            }
            else
            {
                HttpContext.Session.SetString("APIToken", "");
            }

            return View(objResponse);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("APIToken", "");

            return RedirectToAction("Login","Authuser");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
