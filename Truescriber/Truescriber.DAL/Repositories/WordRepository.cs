using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.EFContext;
using Truescriber.DAL.Entities;
using Truescriber.DAL.Interfaces;

namespace Truescriber.DAL.Repositories
{
    public class WordRepository : IRepository<Word>
    {
        private readonly TruescriberContext db;

        public WordRepository(TruescriberContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Word>> GetAllAsync()
        {
            return await db.Words.ToListAsync();
        }

        public async Task<Word> Get(int id)
        {
            CheckId(id);
            return await db.Words.FindAsync(id);
        }

        public async Task Create(Word word)
        {
            WordValidation(word);
            await db.Words.AddAsync(word);
            await SaveChangeAsync();
        }

        public async Task<Word> FindAsync(Expression<Func<Word, bool>> predicate)
        {
            return await db.Words.AsQueryable().Where(predicate).FirstAsync();
        }

        public async Task<IEnumerable<Word>> FindAllAsync(Expression<Func<Word, bool>> predicate)
        {
            return await db.Words.AsQueryable().Where(predicate).ToListAsync();
        }

        public void Update(Word word)
        {
            WordValidation(word);
            db.Entry(word).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            CheckId(id);
            var word = await db.Tasks.FindAsync(id);

            if (word == null)
                throw new ArgumentException("Word not found");

            db.Tasks.Remove(word);
            await SaveChangeAsync();
        }

        public async Task SaveChangeAsync()
        {
            await db.SaveChangesAsync();
        }

        private static void CheckId(int id)
        {
            if (id == 0)
                throw new ArgumentException("Id cannot be null");
        }

        private static void WordValidation(Word word)
        {
            if (word == null)
                throw new ArgumentException("Word cannot be null");
        }
    }
}
    

