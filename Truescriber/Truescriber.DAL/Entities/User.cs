using Microsoft.AspNetCore.Identity;


namespace Truescriber.DAL.Entities
{
    public class User : IdentityUser
    {
        public int Id { get; set; } 

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public string Online { get; set; }



    }
}
