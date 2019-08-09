using Truescriber.Common.Helpers;

namespace Truescriber.BLL.Services.Models.PageModel
{
    public class TaskViewModel
    {
        public DAL.Entities.Tasks.Task Task { get; set; }
        
        public string FileSize { get; set; } 

        public TaskViewModel(DAL.Entities.Tasks.Task task)
        {
            Task = task;
            FileSize = TaskHelper.BytesToSizeString(task.Size);
        }
    }
}
