using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // =======================
        // DbSets
        // =======================
        public DbSet<Company> Companies { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SignUp> signups { get; set; }

        public DbSet<Tasks> tasks { get; set; }

        public DbSet<AssignTask> AssignTasks { get; set; }

        // =======================
        // Model Configuration
        // =======================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Company -> Unit (Cascade is OK here)
            modelBuilder.Entity<Unit>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Units)
                .HasForeignKey(u => u.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Company -> Employee (NO ACTION / RESTRICT)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unit -> Employee (NO ACTION / RESTRICT)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Unit)
                .WithMany(u => u.Employees)
                .HasForeignKey(e => e.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique EmployeeId
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmployeeId)
                .IsUnique();
        }
    }
}
