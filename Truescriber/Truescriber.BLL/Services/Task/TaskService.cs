﻿using System;
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
using Truescriber.BLL.Clients;
using Truescriber.BLL.Clients.SpeechToTextModels;
using Truescriber.BLL.Services.Models.ShowResultModel;
using Truescriber.DAL.Entities;

namespace Truescriber.BLL.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<DAL.Entities.Tasks.Task> _taskRepository;
        private readonly IRepository<Word> _wordRepository;
        public static FormatHelper FormatHelper;
        public static SpeechToTextClient SpeechToTextClient;

        public TaskService(
            IRepository<DAL.Entities.Tasks.Task> taskRep,
            IRepository<Word> wordRepository
        ){
            _taskRepository = taskRep;
            _wordRepository = wordRepository;
            FormatHelper = new FormatHelper();
            SpeechToTextClient = new SpeechToTextClient();
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

            await CreateDescription(
                uploadModel.TaskName, 
                uploadModel.File, id, 
                uploadModel.DurationMoreMinute
                );
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

        public async System.Threading.Tasks.Task StartProcessing(int id)
        {
            SpeechToTextViewModel result;
            var task = await _taskRepository.Get(id);

            if (!task.DurationMoreMinute)
            {
                task.SetStartTime();
                task.ChangeStatus(TaskStatus.Processed);
                _taskRepository.Update(task);
                await _taskRepository.SaveChangeAsync();

                result = await SpeechToTextClient.SyncRecognize(task.File);
                task.SetFinishTime();
            }
            else
            {
                task.SetStartTime();
                task.ChangeStatus(TaskStatus.Processed);
                _taskRepository.Update(task);
                await _taskRepository.SaveChangeAsync();

                result = await SpeechToTextClient.AsyncRecognize(task.File);
                task.SetFinishTime();
            }

            var i = 0;
            foreach (var item in result.WordInfo)
            {
                var word = new Word(
                    item.Word.ToString(),
                    item.StartTime,
                    item.EndTime,
                    i,
                    task.Id);
                i++;
                await _wordRepository.Create(word);
                await _wordRepository.SaveChangeAsync();
            }

            task.ChangeStatus(TaskStatus.Finished);
            _taskRepository.Update(task);
            await _taskRepository.SaveChangeAsync();
        }

        public async Task<ShowResultViewModel[]> ShowResult(int id)
        {
            var words = await _wordRepository.FindAllAsync(x=>x.TaskId == id);

            return words
                .Where(w => w.TaskId == id)
                .OrderBy(x => x.Index)
                .Select(x => new ShowResultViewModel
                {
                    StartTime = x.StartTime,
                    FinishTime = x.FinishTime,
                    Value = x.Value
                }).ToArray();
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

        private async System.Threading.Tasks.Task CreateDescription(
            string taskName, 
            IFormFile file, 
            string id, 
            bool durationMoreMinute)
        {
            var task = new DAL.Entities.Tasks.Task(
                DateTime.UtcNow,
                taskName,
                file.FileName,
                file.ContentType,
                file.Length,
                id,
                durationMoreMinute
                );

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                task.AddFile(binaryReader.ReadBytes((int)file.Length));
            }
            task.ChangeStatus(TaskStatus.UploadToServer);

            await _taskRepository.Create(task);
        }
    }
}
