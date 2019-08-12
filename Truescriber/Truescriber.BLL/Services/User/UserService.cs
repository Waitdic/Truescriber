using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Truescriber.BLL.Interfaces;
using Truescriber.BLL.Services.User.IdentityModels;
using Truescriber.Common.Helpers;

namespace Truescriber.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<DAL.Entities.User> _userManager;
        private readonly SignInManager<DAL.Entities.User> _signInManager;

        public UserService(
            UserManager<DAL.Entities.User> userManager,
            SignInManager<DAL.Entities.User> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Register(RegisterViewModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;

            var user = new DAL.Entities.User(model.Email);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(string.Empty, error.Description);
                    return false;
                }
            }

            await _signInManager.SignInAsync(user, false);
            return true;
        }

        public async Task<bool> Login(LoginViewModel logModel, SignInResult result, ModelStateDictionary modelState)
        {
            if (!result.Succeeded)
            {
                modelState.AddModelError("", "Incorrect login or password");
                return false;
            }

            var user = await _userManager.FindByNameAsync(logModel.Login);
            user.GoOnline();
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async System.Threading.Tasks.Task Logout(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.GoOffline();
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
        }
    }
}
