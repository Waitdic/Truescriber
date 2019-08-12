using System.Collections.Generic;

namespace Truescriber.BLL.Services.Models.PageModel
{
    public class PagedTaskList
    {
        public IEnumerable<TaskViewModel> TaskViewModels { get; set; }
        public PagedViewModel PagedViewModel { get; set; }

        public PagedTaskList(IEnumerable<TaskViewModel> taskViewModel, PagedViewModel pagedViewModel)
        {
            TaskViewModels = taskViewModel;
            PagedViewModel = pagedViewModel;
        }
    }
}
