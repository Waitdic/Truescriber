﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Truescriber.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);

        void Create(T item);
        void Update(T item);
        void Delete(int id);

        void CreateDescription(string name, IFormFile file, string id);
        void SaveChange();
    }
}