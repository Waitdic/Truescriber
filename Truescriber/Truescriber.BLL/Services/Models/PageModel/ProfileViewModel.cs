using System.Collections.Generic;

namespace Truescriber.BLL.Services.Models.PageModel
{
    // TODO: Rename to PagedTaskList
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
