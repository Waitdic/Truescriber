using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.Entities;


namespace Truescriber.WEB.Models
{
    public sealed class TruescriberContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }

        public TruescriberContext(DbContextOptions<TruescriberContext> options)
            :base(options)
        {

            Database.EnsureCreated();
        }
    }
}
