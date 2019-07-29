using System;

namespace Truescriber.DAL.Entities
{
    public class Task
    {
        protected Task()
        {
        }

        public int Id { get; private set; }

        private DateTime StartTime { get; set; }
        private DateTime UpdateTime { get; set; }

        private string Status { get; set; }

        private int? UserId { get; set; }
        public virtual User User { get; set; }

        public File File { get; private set; }

        public Task(DateTime startTime, DateTime updateTime, string status, string fileName, string size, string format, string length, string link)
        {
            StartTime = startTime;
            UpdateTime = updateTime;
            Status = status;
            File = new File(fileName, size, format, length, link);
        }

        public void ChangeStatus(string status)
        {
            Status = status;
        }

        public void ChangeUpdateTime(DateTime updateTime)
        {
            UpdateTime = updateTime;
        }
    }
}
