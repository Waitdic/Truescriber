using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Truescriber.DAL.EFContext;
using Truescriber.DAL.Interfaces;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;
using Task = Truescriber.DAL.Entities.Tasks.Task;

namespace Truescriber.DAL.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly TruescriberContext db;

        public TaskRepository(TruescriberContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Task>> GetAllAsync()
        {
            return await db.Tasks.ToListAsync();
        }

        public async Task<Task> Get(int id)
        {
            CheckId(id);
            return await db.Tasks.FindAsync(id);
        }

        public async System.Threading.Tasks.Task Create(Task task)
        {
            TaskValidation(task);
            await db.Tasks.AddAsync(task);
            await SaveChangeAsync();
        }

        public async Task<Task> FindAsync(Expression<Func<Task, bool>> predicate)
        {
            return await db.Tasks.AsQueryable().Where(predicate).FirstAsync();
        }

        public async Task<IEnumerable<Task>> FindAllAsync(Expression<Func<Task,bool>> predicate)
        {
            return await db.Tasks.AsQueryable().Where(predicate).ToListAsync();
        }

        public void Update(Task task)
        {
            TaskValidation(task);
            db.Entry(task).State = EntityState.Modified;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            CheckId(id);
            var task = await db.Tasks.FindAsync(id);

            if (task == null)
                throw new ArgumentException("Task not found");

            db.Tasks.Remove(task);
            await SaveChangeAsync();
        }

        public async System.Threading.Tasks.Task SaveChangeAsync()
        {
            await db.SaveChangesAsync();
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