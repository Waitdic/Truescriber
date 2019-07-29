using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Truescriber.DAL.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
        }

        private string Name { get; set; }
        private string Surname { get; set; }

        private string Login { get; set; }
        private string Password { get; set; }

        public bool Online { get; set; }

        private ICollection<Task> Tasks { get; set; }
    }
}
