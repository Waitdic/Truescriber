using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.EFContext;
using Truescriber.DAL.Entities;
using Truescriber.DAL.Interfaces;


namespace Truescriber.DAL.Repositories
{
    public class FileRepository : IRepository<File>
    {
        private TruescriberContext db;

        public FileRepository(TruescriberContext context)
        {
            db = context;
        }
        public IEnumerable<File> GetAll()
        {
            return db.Files;
        }

        public File Get(int id)
        {
            return db.Files.Find(id);
        }

        public void Create(File file)
        {
            db.Files.Add(file);
            db.SaveChangesAsync();
        }

        public void Update(File file)
        {
            db.Entry(file).State = EntityState.Modified;
            db.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var file = db.Files.Find(id);
            if (file != null)
                db.Remove(file);

            db.SaveChangesAsync();
        }
    }
}
