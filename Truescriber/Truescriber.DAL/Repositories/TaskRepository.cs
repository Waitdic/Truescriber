using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.EFContext;
using Truescriber.DAL.Interfaces;
using Task = Truescriber.DAL.Entities.Task;

namespace Truescriber.DAL.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly TruescriberContext db;

        public TaskRepository(TruescriberContext context)
        {
            db = context;
        }
        public IEnumerable<Task> GetAll()
        {
            return db.Tasks;
        }

        public Task Get(int id)
        {
            CheckId(id);
            return db.Tasks.Find(id);
        }

        public async void Create(Task task)
        {
            TaskValidation(task);
            await db.Tasks.AddAsync(task);
            SaveChange();
        }

        public IEnumerable<Task> Find(Func<Task, bool> predicate)
        {
            return db.Tasks.Where(predicate).ToList(); ;
        }

        public void Update(Task task)
        {
            TaskValidation(task);
            db.Entry(task).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            CheckId(id);
            var task = db.Tasks.Find(id);

            if (task == null)
                throw new ArgumentException("Task not found");

            db.Tasks.Remove(task);
            SaveChange();
        }

        public void SaveChange()
        {
            db.SaveChanges();
        }

        public void CreateDescription(string taskName, IFormFile file, string id)
        {
            var task = new Task(
                    DateTime.UtcNow,
                    taskName,
                    file.FileName,
                    file.ContentType,
                    file.Length,
                    id);

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                task.AddFile(binaryReader.ReadBytes((int)file.Length));
            }
            task.ChangeStatus(Status.UploadToServer);

            Create(task);
        }

        private static void CheckId(int id)
        {
            if (id == 0)
                throw new ArgumentException("Id cannot be null");
        }

        private static void TaskValidation(Task task)
        {
            if (task == null)
                throw new ArgumentException("Task cannot be null");
        }
    }
}