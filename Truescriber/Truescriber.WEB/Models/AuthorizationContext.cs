using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Truescriber.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Truescriber.WEB.Models
{
    public class AuthorizationContext : IdentityDbContext<User>
    {
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
