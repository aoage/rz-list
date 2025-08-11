using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data
{
    public class RzListDbContext(DbContextOptions<RzListDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Book> Books { get; set; }
        public override DbSet<User> Users { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
    }
}