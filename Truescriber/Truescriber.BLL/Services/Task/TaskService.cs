using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Truescriber.BLL.Interfaces;
using Truescriber.BLL.Services.Models.PageModel;
using Truescriber.BLL.Services.Task.Models;
using Truescriber.Common.Helpers;
using Truescriber.DAL.Interfaces;
using Truescriber.DAL.Entities.Tasks;

namespace Truescriber.BLL.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<DAL.Entities.Tasks.Task> _taskRepository;
        public static FormatHelper FormatHelper;

        public TaskService(IRepository<DAL.Entities.Tasks.Task> taskRep)
        {
            _taskRepository = taskRep;
            FormatHelper = new FormatHelper();
        }

        public PagedTaskList CreateTaskList(int page, string userId)
        {
            const int pageSize = 15;
            var tasks = _taskRepository.Find(t => t.UserId == userId);

            var enumerable = tasks as DAL.Entities.Tasks.Task[] ?? tasks.ToArray();     // Form an array of tasks;
            var count = enumerable.Count();                                             // Number of all tasks;
            var taskViewModel = new TaskViewModel[count];                               // Create array of TaskViewModel;

            for (var i = 0; i < count; i++)
            {
                taskViewModel[i] = new TaskViewModel(enumerable[i]);                    //Give each task a converted size;
            }

            var items = taskViewModel.Skip((page - 1) * pageSize).Take(pageSize).ToList(); // Skip the required number of items;
            var pageViewModel = new PagedViewModel(count, page, pageSize);        //Create new Page;

            var viewModel = new PagedTaskList(items, pageViewModel);                        // General ViewModel;
            return  viewModel;
        }

        public CreateTaskViewModel UploadFile(
            string id, 
            CreateTaskViewModel uploadModel, 
            ModelStateDictionary modelState
        ){
            if (!modelState.IsValid)
            {
                modelState.AddModelError("", "Fill in all the fields");
                return uploadModel;
            }

            if (!GetFormatValid(uploadModel.File.ContentType))
            {
                modelState.AddModelError("",GetFormatError());
                return uploadModel;
            }

            CreateDescription(uploadModel.TaskName, uploadModel.File, id);
            return null;
        }

        public void DeleteTask(int id)
        {
            _taskRepository.Delete(id);
        }

        public void EditTask(EditTaskViewModel editModel)
        {
            if(editModel == null)
                throw new ArgumentException("Model cannot be null");
            
            var task = _taskRepository.Get(editModel.TaskId);
            task.ChangeTaskName(editModel.TaskName);
            _taskRepository.Update(task);
            _taskRepository.SaveChange();
        }

        private static bool GetFormatValid(string format)
        {
            var result = FormatHelper.GetFormats().Find((x) => x == format);
            return !string.IsNullOrWhiteSpace(result);
        }

        private static string GetFormatError()
        {
            return "Supported formats:" + FormatHelper.SupportedFormatsMessage();
        }

        private void CreateDescription(string taskName, IFormFile file, string id)
        {
            var task = new DAL.Entities.Tasks.Task(
                DateTime.UtcNow,
                taskName,
                file.FileName,
                file.ContentType,
                file.Length,
                id);

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                task.AddFile(binaryReader.ReadBytes((int)file.Length));
            }
            task.ChangeStatus(TaskStatus.UploadToServer);

            _taskRepository.Create(task);
        }
    }
}
