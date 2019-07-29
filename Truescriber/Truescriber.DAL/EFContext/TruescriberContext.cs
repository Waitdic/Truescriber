using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.Entities;

namespace Truescriber.DAL.EFContext
{
    public class TruescriberContext : IdentityDbContext<User>
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<File> Files { get; set; }

        public TruescriberContext(DbContextOptions<TruescriberContext> options)
            :base(options)
        {
           // Database.EnsureCreated();
        }
    }
}
