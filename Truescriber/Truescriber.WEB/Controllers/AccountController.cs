using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Truescriber.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Truescriber.BLL.IdentityModels;

namespace Truescriber.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager
        ){
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new User {Email = model.Email, UserName = model.Email, Online = false};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return View(model);
                }
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Login", "Account");
        }



        [Authorize]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginModel {});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, true, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                return View(model);
            }


            var user = await _userManager.FindByNameAsync(model.Login);
            user.Online = true;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Profile", "Account");
            }


        public IActionResult Profile()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.Online = false;
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}