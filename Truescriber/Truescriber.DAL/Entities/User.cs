﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Truescriber.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        ICollection<File> File { get; set; }
    }
}
