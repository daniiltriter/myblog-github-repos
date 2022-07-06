using Microsoft.EntityFrameworkCore;

namespace MyBlog.Models
{
    public class ApplicationContex : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationContex(DbContextOptions<ApplicationContex> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
