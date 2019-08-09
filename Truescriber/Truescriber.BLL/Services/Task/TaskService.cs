using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Truescriber.BLL.Interfaces;
using Truescriber.BLL.Services.Models.PageModel;
using Truescriber.BLL.Services.Task.Models;
using Truescriber.Common.Helpers;
using Truescriber.DAL.Interfaces;
using TaskStatus = Truescriber.DAL.Entities.Tasks.TaskStatus;

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

        public async Task<PagedTaskList> CreateTaskList(int page, string userId)
        {
            const int pageSize = 15;
            var tasks = await _taskRepository.FindAllAsync(t => t.UserId == userId);
            
            var enumerable = tasks as DAL.Entities.Tasks.Task[] ?? tasks.ToArray();     
            var count = enumerable.Count();                                             
            var taskViewModel = new TaskViewModel[count];                               

            for (var i = 0; i < count; i++)
            {
                taskViewModel[i] = new TaskViewModel(enumerable[i]);                    
            }

            var items = taskViewModel.Skip((page - 1) * pageSize).Take(pageSize).ToList(); 
            var pageViewModel = new PagedViewModel(count, page, pageSize);        

            var viewModel = new PagedTaskList(items, pageViewModel);                        
            return viewModel;
        }

        public async Task<CreateTaskViewModel> UploadFile(
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

            await CreateDescription(uploadModel.TaskName, uploadModel.File, id);
            return null;
        }

        public async System.Threading.Tasks.Task DeleteTask(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async System.Threading.Tasks.Task EditTask(EditTaskViewModel editModel)
        {
            if(editModel == null)
                throw new ArgumentException("Model cannot be null");
            
            var task = await _taskRepository.Get(editModel.TaskId);
            task.ChangeTaskName(editModel.TaskName);
            _taskRepository.Update(task);
            await _taskRepository.SaveChangeAsync();
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

        private async System.Threading.Tasks.Task CreateDescription(string taskName, IFormFile file, string id)
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

            await _taskRepository.Create(task);
        }
    }
}
