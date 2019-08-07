using Truescriber.BLL.IdentityModels;
using Truescriber.DAL.Entities;
using Task = System.Threading.Tasks.Task;

namespace Truescriber.BLL.Interfaces
{
    public interface IUserService
    {
        void Register(User user);
        Task Login(LoginViewModel item);
        Task Logout(string id);
    }
}
