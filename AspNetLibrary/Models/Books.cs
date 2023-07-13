using System.ComponentModel.DataAnnotations;

namespace AspNetLibrary.Models
{
    public class Books
    {

        [Key]
        public int BookID { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public int ReleaseDate { get; set; }

        public int Pages { get; set; }

        public string Author { get; set; }

        public string Link { get; set; }
    }
}
