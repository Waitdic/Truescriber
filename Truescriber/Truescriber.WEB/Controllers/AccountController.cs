using System.Net;
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

            var userLogin = await _userService.Login(model, result, ModelState);
            if (userLogin == false) return View(model);

            return RedirectToAction("TaskList");
        }

        public async Task<IActionResult> TaskList(int page = 1)
        {
            var viewModel = await _taskService.CreateTaskList(page, _userManager.GetUserId(User));
            return View(viewModel); 
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            return View(new CreateTaskViewModel{});
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(CreateTaskViewModel uploadModel)
        {
            var result = await _taskService.UploadFile(_userManager.GetUserId(User), uploadModel, ModelState);
            if (result != null) return View(uploadModel);

           return RedirectToAction("TaskList", "Account");
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _taskService.DeleteTask(id);
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
        public async Task<ActionResult> Edit(EditTaskViewModel model)
        {
            await _taskService.EditTask(model);
            return RedirectToAction("TaskList");
        }


        //[HttpPost]
        public IActionResult StartProcessing(Result result)
        {
            result.TextResult = _taskService.StartProcessing("E:/загрузки/Record_flac_48Hz.flac");
            return View(result);
            //return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout(_userManager.GetUserId(User));
            return RedirectToAction("Login", "Account");
        }
    }
}