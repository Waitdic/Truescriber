using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Truescriber.BLL.Services.Models.PageModel;
using Truescriber.BLL.Services.Task.Models;

namespace Truescriber.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<PagedTaskList> CreateTaskList(int page, string id);

        Task<CreateTaskViewModel> UploadFile(
            string id, 
            CreateTaskViewModel item, 
            ModelStateDictionary modelState);

        Task EditTask(EditTaskViewModel item);
        Task DeleteTask(int id);

        string StartProcessing(int id);
    }
}
