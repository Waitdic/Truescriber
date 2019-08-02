using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Truescriber.DAL.Entities
{
    public class Task
    {
        protected Task()
        {
        }

        public Task(
            DateTime startTime,
            DateTime updateTime,
            string taskName,
            string fileName,
            string format,
            long size,
            string userId
        )
        {
            ValidationFormat(format);

            StartTime = startTime;
            UpdateTime = updateTime;
            TaskName = taskName;
            FileName = fileName;
            Format = format;
            //Length = length;
            Size = size;
            UserId = userId;
        }

        public int Id { get; protected set; }

        public DateTime StartTime { get; protected set; }
        public DateTime UpdateTime { get; protected set; }

        public string Status { get; protected set; }
        public string TaskName { get; protected set; }
        public string FileName { get; protected set; }
        public string Format { get; protected set; }
        public string Length { get; protected set; }
        public long Size { get; protected set; }
        public byte[] File { get; protected set; }

        public string UserId { get; protected set; }
        [ForeignKey("UserId")] public virtual User User { get; set; }


        public void ChangeStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be null");

            Status = status;
        }

        public void ChangeUpdateTime()
        {
            var updateTime = DateTime.UtcNow;
            if (updateTime < StartTime)
                throw new ArgumentException("Time can not be earlier");

            UpdateTime = updateTime;
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

        public void StatusUploadServer()
        {
            Status = "Uploaded to server";
        }

        public void ValidationFormat(string format)
        {
            var audioFormats = new List<string>()
            {
                "audio/flac",
                "audio/raw",
                "audio/wav",
                "audio/mp3",
                "audio/arm-wb",
                "audio/ogg",
            };
            string form = audioFormats.Find((x) => x == format);

            if (string.IsNullOrWhiteSpace(form))
                throw new ArgumentException(
                    "Wrong format. Server support: audio/flac, audio/raw, audio/wav, audio/mp3, audio/arm-wb, audio/ogg");
        }
    }
}