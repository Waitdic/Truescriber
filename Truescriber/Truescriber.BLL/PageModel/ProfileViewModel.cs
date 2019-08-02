using System.Collections.Generic;
using Truescriber.DAL.Entities;

namespace Truescriber.BLL.PageModel
{
    public class ProfileViewModel
    {
        public IEnumerable<Task> Task { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
