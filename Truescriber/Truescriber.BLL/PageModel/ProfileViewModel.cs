using System.Collections.Generic;

namespace Truescriber.BLL.PageModel
{
    public class ProfileViewModel
    {
        public IEnumerable<TaskViewModel> TaskViewModels { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public ProfileViewModel(IEnumerable<TaskViewModel> taskViewModel, PageViewModel pageViewModel)
        {
            TaskViewModels = taskViewModel;
            PageViewModel = pageViewModel;
        }
    }
}
