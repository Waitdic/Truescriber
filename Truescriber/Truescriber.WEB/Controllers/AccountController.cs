using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Truescriber.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Truescriber.BLL.Interfaces;
using Truescriber.BLL.Services.Task.Models;
using Truescriber.BLL.Services.User.IdentityModels;

namespace Truescriber.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITaskService taskService,
            IUserService userService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            //if (!ModelState.IsValid) return View(model);
            
            //var user = new User(model.Email);
            //var result = await _userManager.CreateAsync(user, model.Password);

            //var userValid = await _userService.Register(user, result, ModelState);

            var userValid = await _userService.Register(model, ModelState);
            if (userValid != true) return View(model);

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
            var result = await _signInManager.PasswordSignInAsync(
                model.Login,
                model.Password,
                true,
                false);

            var test = await _userService.Login(model, result, ModelState);
            if (test == false) return View(model);

            return RedirectToAction("TaskList");
        }

        public IActionResult TaskList(int page = 1)
        {
            var viewModel = _taskService.CreateTaskList(page, _userManager.GetUserId(User));
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
            var result = _taskService.UploadFile(_userManager.GetUserId(User), uploadModel, ModelState);
            if (result != null) return View(uploadModel);

           return RedirectToAction("TaskList", "Account");
        }

        public ActionResult Delete(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction("TaskList");
        }

        [HttpGet]
        public IActionResult Edit(int id, string taskName)
        {
            return View(new EditTaskViewModel
            {
                TaskName = taskName,
                TaskId = id          
            });
        }

        [HttpPost]
        public ActionResult Edit(EditTaskViewModel model)
        {
            _taskService.EditTask(model);
            return RedirectToAction("TaskList");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout(_userManager.GetUserId(User));
            return RedirectToAction("Login", "Account");
        }
    }
}