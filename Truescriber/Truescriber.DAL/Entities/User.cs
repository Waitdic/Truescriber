using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Truescriber.DAL.Entities
{
    public class User : IdentityUser
    {
        protected User()
        {
        }

        public User(string userName)
        {
            Email = userName;
            UserName = userName;
            Online = false;
        }

        public string Name { get; protected set; }
        public string Surname { get; protected set; }

        public string Login { get; protected set; }
        public string Password { get; protected set; }

        public bool Online { get; protected set; }

        protected virtual ICollection<Task> Tasks { get; set; }

        public void GoOnline()
        {
            Online = true;
        }

        public void GoOffline()
        {
            Online = false;
        }
    }
}
