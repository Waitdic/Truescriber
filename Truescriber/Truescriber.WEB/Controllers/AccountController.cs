using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Truescriber.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Truescriber.BLL.EditModel;
using Truescriber.BLL.IdentityModels;
using Truescriber.BLL.Interfaces;
using Truescriber.BLL.UploadModel;
using Truescriber.DAL.Interfaces;
using Task = Truescriber.DAL.Entities.Task;

namespace Truescriber.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepository<Task> _taskRepository;
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IRepository<Task> taskRepository,
            ITaskService taskService,
            IUserService userService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _taskRepository = taskRepository;
            _taskService = taskService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User(model.Email);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return View(model);
                }
            }

            _userService.Register(user);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel { });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Введены не все данные!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Login,
                model.Password,
                true,
                false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Login);
            model.UserId = user.Id;
            user.GoOnline();
            await _userManager.UpdateAsync(user);

            //_userService.Login(model);
            return RedirectToAction("Profile");
        }

        public IActionResult Profile(int page = 1)
        {
            var viewModel = _taskService.CreateProfile(page, _userManager.GetUserId(User));
            return View(viewModel); 
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View(new UploadViewModel{});
        }

        [HttpPost]
        public ActionResult Upload(UploadViewModel uploadModel)
        {
           var modelState = ModelState;
           var result = _taskService.UploadFile(_userManager.GetUserId(User), uploadModel, modelState);

           if (result != null)
               return View(uploadModel);

           return RedirectToAction("Profile", "Account");
        }

        public ActionResult Delete(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _taskRepository.Get(id);
            return View(new EditViewModel
            {
                TaskName = task.TaskName,
                TaskId = task.Id          
            });
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            _taskService.EditTask(model);
            return RedirectToAction("Profile");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout(_userManager.GetUserId(User));
            return RedirectToAction("Login", "Account");
        }
    }
}