using System;
using System.Collections.Generic;
using System.Text;

namespace Truescriber.DAL.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string LoadFileUrl {get;set;}
        public string UploadFileUrl { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
