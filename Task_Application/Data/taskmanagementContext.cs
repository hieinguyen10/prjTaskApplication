using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Task_Application.Models;
using Task = Task_Application.Models.Task;

namespace Task_Application.Data
{
    public partial class taskmanagementContext : DbContext
    {
        public taskmanagementContext()
        {
        }

        public taskmanagementContext(DbContextOptions<taskmanagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-TYLER; Initial catalog=taskmanagement; Trusted_Connection=True; TrustServerCertificate=True");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasOne(d => d.AssignedNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.Assigned)
                    .HasConstraintName("FK_Tasks_Employees");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
