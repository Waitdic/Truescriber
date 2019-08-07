
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Truescriber.BLL.IdentityModels;
using Truescriber.BLL.Interfaces;
using Truescriber.DAL.Entities;
using Task = System.Threading.Tasks.Task;

namespace Truescriber.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async void Register(User user)
        {
            await _signInManager.SignInAsync(user, false);
        }

        public async Task Login(LoginViewModel logModel)
        {
            var user = await _userManager.FindByNameAsync(logModel.Login);
            logModel.UserId = user.Id;
            user.GoOnline();
            await _userManager.UpdateAsync(user);
        }

        public async Task Logout(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.GoOffline();
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
        }
    }
}
