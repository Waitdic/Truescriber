using System;
using System.ComponentModel.DataAnnotations.Schema;

public enum StatusValue
{
    UploadToServer,
    Processed,
    Finished
};

namespace Truescriber.DAL.Entities
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
            string userId
        )
        {
            CreateTime = createTime;
            TaskName = taskName;
            FileName = fileName;
            Format = format;
            Size = size;
            UserId = userId;
        }

        public int Id { get; protected set; }

        public DateTime CreateTime { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public DateTime FinishTime { get; protected set; }

        public string Status { get; protected set; }
        public string TaskName { get; protected set; }
        public string FileName { get; protected set; }
        public string Format { get; protected set; }
        public long Size { get; protected set; }
        public byte[] File { get; protected set; }

        public string UserId { get; protected set; }
        [ForeignKey("UserId")] public virtual User User { get; set; }


        public void ChangeCreateTime()
        {
            var createTime = DateTime.UtcNow;
            CreateTime = createTime;
        }

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

        public void ChangeStatus(StatusValue status)
        {
            Status = status.ToString();
        }
    }
}