using System.Threading.Tasks;
using Truescriber.BLL.Services.User.IdentityModels;
using Truescriber.DAL.Entities;
using Task = System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Truescriber.BLL.Interfaces
{
    public interface IUserService
    {
        // Task<bool> Register(User user, IdentityResult result, ModelStateDictionary modelState);
        Task<bool> Register(RegisterViewModel model, ModelStateDictionary modelState);
        Task<bool> Login(LoginViewModel item, SignInResult result, ModelStateDictionary modelState);
        Task Logout(string id);
    }
}
