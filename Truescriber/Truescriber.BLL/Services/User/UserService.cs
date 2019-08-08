using Microsoft.AspNetCore.Identity;
using Truescriber.BLL.Interfaces;
using Truescriber.BLL.Services.User.IdentityModels;

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

        public async void Register(DAL.Entities.User user)
        {
            await _signInManager.SignInAsync(user, false);
        }

        public async System.Threading.Tasks.Task Login(LoginViewModel logModel)
        {
            var user = await _userManager.FindByNameAsync(logModel.Login);
            user.GoOnline();
            await _userManager.UpdateAsync(user);
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
