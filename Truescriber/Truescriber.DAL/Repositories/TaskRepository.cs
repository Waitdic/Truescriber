using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.EFContext;
using Truescriber.DAL.Entities;
using Truescriber.DAL.Interfaces;

namespace Truescriber.DAL.Repositories
{
    public class TaskRepository :IRepository<Task>
    {
        private TruescriberContext db;

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
            return db.Tasks.Find(id);
        }

        public void Create(Task task)
        {
            db.Tasks.Add(task);
            //db.SaveChangesAsync();
        }

        public void Update(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var task = db.Tasks.Find(id);
            if (task == null) return;

            var file = task.File;
            if (file != null)
                db.Files.Remove(file);

            db.Tasks.Remove(task);
        }
    }
}
