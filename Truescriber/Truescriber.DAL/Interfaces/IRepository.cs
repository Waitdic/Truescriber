﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Truescriber.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> Get(int id);
        Task<T> FindAsync(Expression<Func<Entities.Tasks.Task, bool>> predicate);
        Task<IEnumerable<Entities.Tasks.Task>> FindAllAsync(Expression<Func<Entities.Tasks.Task, bool>> predicate);
        Task Create(T item);
        void Update(T item);
        Task DeleteAsync(int id);
        Task SaveChangeAsync();
    }
}