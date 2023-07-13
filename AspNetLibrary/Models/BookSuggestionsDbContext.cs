using Microsoft.EntityFrameworkCore;

namespace AspNetLibrary.Models
{
    public class BookSuggestionsDbContext: DbContext
    {
        public BookSuggestionsDbContext(DbContextOptions<BookSuggestionsDbContext> options) : base(options)
        {

        }

        public DbSet<BookSuggestions> BookSuggestions { get; set; }
    }
}
