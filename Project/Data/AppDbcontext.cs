using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
namespace Project.Data
{
    public class AppDbContext  : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
        public DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Units)
                .HasForeignKey(u => u.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public DbSet<SignUp> signups { get; set; }
    }
    }
