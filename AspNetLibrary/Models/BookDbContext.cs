using Microsoft.EntityFrameworkCore;


namespace AspNetLibrary.Models
{
    public class BookDbContext: DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }

        public DbSet<Books> Books { get; set; }
    }
}
