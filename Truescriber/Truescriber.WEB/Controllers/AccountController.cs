using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Truescriber.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Truescriber.BLL.EditModel;
using Truescriber.BLL.IdentityModels;
using Truescriber.BLL.PageModel;
using Truescriber.DAL.EFContext;
using Truescriber.BLL.UploadModel;
using Truescriber.DAL.Interfaces;
using Task = Truescriber.DAL.Entities.Task;

namespace Truescriber.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TruescriberContext db;
        private IRepository<Task> _taskRepository;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            TruescriberContext context,
            IRepository<Task> taskRepository
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
            _taskRepository = taskRepository;
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

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Login", "Account");
        }


        [Authorize]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginModel { });
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
            model.UserId = user.Id;
            user.GoOnline();
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Profile");
        }


        public IActionResult Profile(int page = 1)
        {
            var tasks = _taskRepository.Find(t => t.UserId == _userManager.GetUserId(User));

            //TaskViewModel - model that converted size
            var enumerable = tasks as Task[] ?? tasks.ToArray();        // Form an array of tasks;
            var count = enumerable.Count();                             // Number of all tasks;
            var taskViewModel = new TaskViewModel[] { };                // Create array of TaskViewModel;
            for (var i = 0; i < count; i++)
            {
                taskViewModel[i] = new TaskViewModel(enumerable[i]);    //Give each task a converted size;
            }

            //Pagination
            const int pageSize = 15;                                                        // Max item numbers to page;
            var items = taskViewModel.Skip((page - 1) * pageSize).Take(pageSize).ToList();  // Skip the required number of items;

            var pageViewModel = new PageViewModel(count, page, pageSize);          //Create new Page;
            var viewModel = new ProfileViewModel(items, pageViewModel);                      // General ViewModel;

            return View(viewModel); 
        }


        [HttpGet]
        public IActionResult Upload()
        {
            return View(new TaskModel{});
        }

        [HttpPost]
        public async Task<IActionResult> Upload(TaskModel taskModel)
        {
            if (!ModelState.IsValid) return View(taskModel);

            // Creating task;
            // Uploading file to database;
            taskModel.UserId = _userManager.GetUserId(User);
            var task = new Task(
                DateTime.UtcNow,
                taskModel.TaskName,
                taskModel.File.FileName,
                taskModel.File.ContentType,
                taskModel.File.Length,
                taskModel.UserId);
            task.StatusUploadServer(); // Changing status when file to upload server db;

            using (var binaryReader = new BinaryReader(taskModel.File.OpenReadStream()))
            {
                task.AddFile(binaryReader.ReadBytes((int)taskModel.File.Length));
            }

            _taskRepository.Create(task);
            await db.SaveChangesAsync();

            return RedirectToAction("Profile", "Account");
        }


        public async Task<IActionResult> Delete(int id)
        {
            _taskRepository.Delete(id);
            await db.SaveChangesAsync();
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _taskRepository.Get(id);
            return View(new EditModel
            {
                TaskName = task.TaskName, //Transfer the available data
                TaskId = task.Id          //
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditModel model)
        {
            var task = _taskRepository.Get(model.TaskId);

            task.ChangeTaskName(model.TaskName);
            task.ChangeUpdateTime() ;

            _taskRepository.Update(task);
            await db.SaveChangesAsync();

            return RedirectToAction("Profile");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.GoOffline();

            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

    }
}