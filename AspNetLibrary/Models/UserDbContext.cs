using Microsoft.EntityFrameworkCore;

namespace AspNetLibrary.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<Usera> Users { get; set; }
    }
}
