using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Truescriber.DAL.Entities
{
    public class File
    {
        protected File()
        {
        }

        [Key]
        [ForeignKey("Task")]
        public int Id { get; private set; }

        private string FileName { get; set; }
        private string Size { get; set; }
        private string Format { get; set; }
        private string Length { get; set; }
        private string Link { get; set; }

        private Task Task { get; set; }

        public File(string fileName, string size, string format, string length, string link)
        {
            FileName = fileName;
            Size = size;
            Format = format;
            Length = length;
            Link = link;
        }

        public void ChangeFileName(string fileName)
        {
            FileName = fileName;
        }
    }
}
