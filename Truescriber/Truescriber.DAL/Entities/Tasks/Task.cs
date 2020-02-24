using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Truescriber.DAL.Entities.Tasks
{
    public class Task
    {
        protected Task()
        {
        }

        public Task(
            DateTime createTime,
            string taskName,
            string fileName,
            string format,
            long size,
            string userId,
            bool durationMoreMinute
        )
        {
            CreateTime = createTime;
            TaskName = taskName;
            FileName = fileName;
            Format = format;
            Size = size;
            UserId = userId;
            DurationMoreMinute = durationMoreMinute;
        }

        public int Id { get; protected set; }

        public DateTime CreateTime { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public DateTime FinishTime { get; protected set; }

        public TaskStatus Status { get; protected set; }
        public string TaskName { get; protected set; }
        public string FileName { get; protected set; }
        public string Format { get; protected set; }
        public long Size { get; protected set; }
        public byte[] File { get; protected set; }
        public bool DurationMoreMinute { get; protected set; }

        public ICollection<Word> Words { get; set; }

        public string UserId { get; protected set; }
        [ForeignKey("UserId")] public virtual User User { get; set; }

        public void ChangeTaskName(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
                throw new ArgumentException("Name cannot be null");

            TaskName = taskName;
        }

        public void AddFile(byte[] file)
        {
            File = file;
        }

        public void ChangeStatus(TaskStatus status)
        {
            var dif = Status - status;
            if (dif < -1 || dif > 0)
                throw new ArgumentException("The process cannot be skipped and cannot be reversed");

            Status = status;
        }

        public void SetStartTime()
        {
            StartTime = DateTime.UtcNow;
        }

        public void SetFinishTime()
        {
            FinishTime = DateTime.UtcNow;
        }
    }
}