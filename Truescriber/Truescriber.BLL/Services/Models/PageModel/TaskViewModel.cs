using Truescriber.Common.Helpers;

namespace Truescriber.BLL.Services.Models.PageModel
{
    public class TaskViewModel
    {
        public DAL.Entities.Task Task { get; set; }
        
        public string FileSize { get; set; } //Size + suffix

        public TaskViewModel(DAL.Entities.Task task)
        {
            Task = task;
            FileSize = TaskHelper.BytesToSizeString(task.Size);
        }
    }
}
