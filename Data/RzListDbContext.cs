using Microsoft.EntityFrameworkCore;
using RzList.Models;

namespace RzList.Data
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