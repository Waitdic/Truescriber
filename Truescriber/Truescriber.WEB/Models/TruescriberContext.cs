using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.Entities;

namespace Truescriber.WEB.Models
{
    public class TruescriberContext : IdentityDbContext<User>
    {
        public TruescriberContext(DbContextOptions<TruescriberContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
