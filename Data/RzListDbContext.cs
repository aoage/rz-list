using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data
{
    public class RzListDbContext : DbContext
    {
        public RzListDbContext(DbContextOptions<RzListDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
    }
}