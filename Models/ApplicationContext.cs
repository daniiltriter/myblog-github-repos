﻿using Microsoft.EntityFrameworkCore;
using MyBlog.Areas.Chat.Models;

namespace MyBlog.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "daniiltriter@gmail.com";
            string adminPassword = "5335";

            Role adminRole = new Role { Id = 1, Name = adminRoleName};
            Role userRole = new Role { Id = 2, Name = userRoleName};

            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleInfoKey = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] {adminRole,userRole});
            modelBuilder.Entity<User>().HasData(new User[] {adminUser});

            base.OnModelCreating(modelBuilder);
        }

    }
}
