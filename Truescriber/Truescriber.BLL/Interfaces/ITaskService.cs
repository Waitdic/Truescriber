using Microsoft.AspNetCore.Mvc.ModelBinding;
using Truescriber.BLL.Services.Models.PageModel;
using Truescriber.BLL.Services.Task.Models;

namespace Truescriber.BLL.Interfaces
{
    public interface ITaskService
    {
        PagedTaskList CreateTaskList(int page, string id);

        UploadViewModel UploadFile(
            string id, 
            UploadViewModel item, 
            ModelStateDictionary modelState);

        void EditTask(EditTaskViewModel item);
        void DeleteTask(int id);
    }
}
