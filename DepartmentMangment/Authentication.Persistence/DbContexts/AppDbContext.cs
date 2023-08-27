/*using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using DepartManagment.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartManagment.Domain.Entities;

namespace DbContexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }

        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=localhost;database=LeqaaDepartManagment;Uid=root;pwd=1216";
                optionsBuilder.UseMySql(connectionString,
                                  ServerVersion.AutoDetect(connectionString))
                                  .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            }
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasData(
       new Department { Id = Guid.NewGuid(), Name = "IT Department" }
   );

            modelBuilder.Entity<EmployeeProfile>().HasData(
                new EmployeeProfile { Id = Guid.NewGuid(), ContactNumber = "123456789", DateOfBirth = new DateTime(1990, 1, 1) }
            );

            modelBuilder.Entity<EmployeeTask>().HasData(
                new EmployeeTask { Id = Guid.NewGuid(), Title = "Complete Project X", Description = "Finish the project by the end of the month" }
            );

        }
    }
}
*/