using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.Entities;
using Truescriber.DAL.Entities.Tasks;

namespace Truescriber.DAL.EFContext
{
    public class TruescriberContext : IdentityDbContext<User>
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Word> Words { get; set; }

        public TruescriberContext(DbContextOptions<TruescriberContext> options)
            :base(options)
        {
        }
    }
}
