using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Truescriber.BLL.EditModel;
using Truescriber.BLL.Helpers;
using Truescriber.BLL.Interfaces;
using Truescriber.BLL.PageModel;
using Truescriber.BLL.UploadModel;
using Truescriber.DAL.Interfaces;
using Task = Truescriber.DAL.Entities.Task;

namespace Truescriber.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Task> _taskRepository;
        public FormatHelper FormatHelper;
        public TaskService(IRepository<Task> taskRep)
        {
            _taskRepository = taskRep;
            FormatHelper = new FormatHelper();
        }

        public ProfileViewModel CreateProfile(int page, string userId)
        {
            const int pageSize = 15;
            var tasks = _taskRepository.Find(t => t.UserId == userId);

            var enumerable = tasks as Task[] ?? tasks.ToArray();        // Form an array of tasks;
            var count = enumerable.Count();                             // Number of all tasks;
            var taskViewModel = new TaskViewModel[count];               // Create array of TaskViewModel;

            for (var i = 0; i < count; i++)
            {
                taskViewModel[i] = new TaskViewModel(enumerable[i]);    //Give each task a converted size;
            }

            var items = taskViewModel.Skip((page - 1) * pageSize).Take(pageSize).ToList();  // Skip the required number of items;
            var pageViewModel = new PageViewModel(count, page, pageSize);          //Create new Page;

            var viewModel = new ProfileViewModel(items, pageViewModel);                      // General ViewModel;
            return  viewModel;
        }

        public UploadViewModel UploadFile(
            string id, 
            UploadViewModel uploadModel, 
            ModelStateDictionary modelState
        ){
            if (!modelState.IsValid)
            {
                modelState.AddModelError("", "Введены не все данные!");
                return uploadModel;
            }

            if (!GetFormatValid(uploadModel.File.ContentType))
            {
                modelState.AddModelError("",GetFormatError());
                return uploadModel;
            }

            _taskRepository.CreateDescription(uploadModel.TaskName, uploadModel.File, id);
            return null;
        }

        public void DeleteTask(int id)
        {
            _taskRepository.Delete(id);
        }

        public void EditTask(EditViewModel editModel)
        {
            if(editModel == null)
                throw new ArgumentException("Model cannot be null");
            
            var task = _taskRepository.Get(editModel.TaskId);
            task.ChangeTaskName(editModel.TaskName);
            _taskRepository.Update(task);
            _taskRepository.SaveChange();
        }

        public bool GetFormatValid(string format)
        {
            var result = FormatHelper.GetFormat().Find((x) => x == format);
            return !string.IsNullOrWhiteSpace(result);
        }

        public string GetFormatError()
        {
            return "Supported formats:" + FormatHelper.GetErrorMessage();
        }

    }
}
