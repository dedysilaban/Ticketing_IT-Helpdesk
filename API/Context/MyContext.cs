using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Ticket> Cases { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Convertation> Convertations { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StatusCode> StatusCodes { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>()
             .HasMany(r => r.Employees)
             .WithOne(u => u.Role);

            modelBuilder.Entity<Employee>()
            .HasMany(u => u.Convertation)
            .WithOne(cv => cv.Employee);

            //modelBuilder.Entity<User>()
            //.HasMany(cs => cs.Case)
            //.WithOne(u => u.User);

            modelBuilder.Entity<Ticket>()
            .HasMany(cs => cs.Convertation)
            .WithOne(cv => cv.Ticket);

            modelBuilder.Entity<Category>()
            .HasMany(ct => ct.Tickets)
            .WithOne(cs => cs.Category);

            modelBuilder.Entity<History>()
            .HasOne(h => h.Ticket)
            .WithMany(cs => cs.History);

            modelBuilder.Entity<StatusCode>()
            .HasMany(sc => sc.History)
            .WithOne(h => h.StatusCode);

        }
    }
}
