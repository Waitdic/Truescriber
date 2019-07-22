using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.Entities;

namespace Truescriber.WEB.Models
{
    public class TruescriberContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public TruescriberContext(DbContextOptions<TruescriberContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
