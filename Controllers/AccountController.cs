using Microsoft.AspNetCore.Mvc;
using SmallBizManager.Models;
using SmallBizManager.Services;
using Microsoft.AspNetCore.Authorization;

using SmallBizManager.Models.Auth;
using SmallBizManager.Models;  
using SmallBizManager.Services; 

namespace SmallBizManager.Controllers
{

    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var loginRequest = new LoginRequest
            {
                Username = model.Email, 
                Password = model.Password
            };

            var result = await _authService.LoginAsync(loginRequest);
            if (result)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Invalid login Please Try Again");
            return View(model);
        }


        public IActionResult Register()
        {
          
            ViewBag.Roles = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(Enum.GetValues(typeof(UserRole)));


            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var registerRequest = new RegisterRequest
            {
                Username = model.Email, 
                Password = model.Password,
                Role = model.Role
            };

            var result = await _authService.RegisterAsync(registerRequest);
            if (result)
                return RedirectToAction("Login");

            ModelState.AddModelError("", "Registration failed");
            return View(model);
        }

        public IActionResult Logout()
        {
            _authService.Logout();
            return RedirectToAction("Login");
        } 
    }
}
