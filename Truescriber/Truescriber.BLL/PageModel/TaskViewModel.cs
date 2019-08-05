using System;
using System.Globalization;
using Truescriber.DAL.Entities;

namespace Truescriber.BLL.PageModel
{
    public class TaskViewModel
    {
        public Task Task { get; set; }
        public string Size { get; set; }

        public TaskViewModel(Task task)
        {
            Task = task;
            Size = Convert(task.Size);
        }

        private string Convert(long taskSize)
        {
            double size = taskSize;
            string[] format = { "B", "KB", "MB", "GB" };
            for(var i = 0; i < format.Length; i++)
            {
                if (size / 1024 == 0 || i == 2)
                {
                    size = Math.Round(size, 1);
                    return size.ToString(CultureInfo.InvariantCulture) + " " + format[i];
                }

                size /= 1024;
            }
            return null;
        }
    }
}
