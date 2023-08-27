using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartManagment.Domain.Entities.ApplicationUser;

using Microsoft.EntityFrameworkCore;
using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using Microsoft.AspNetCore.Identity;
using DepartManagment.Domain.Entities;
using System.Security.Cryptography;

namespace DepartManagment.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }

        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
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


            var DepartmentId = Guid.NewGuid();
            modelBuilder.Entity<Department>().HasData(
      new Department { Id = DepartmentId, Name = "IT Department" }
         );

          

            modelBuilder.Entity<EmployeeTask>().HasData(
                new EmployeeTask { Id = Guid.NewGuid(), Title = "Complete Project X", Description = "Finish the project by the end of the month" }
            );





            var hasher = new PasswordHasher<Employee>();
            var seedApplicationUser = new Employee
            {
                Name = "Leqaa",
                Email = "Leqaa.Technical@gmail.com",
                Gender = Gender.Female,
                EmailConfirmed = true,
                NormalizedEmail = "LEQAA.TECHNICAL@GMAIL.COM",
                UserName = "Leqaa",
                Role= Role.Admin,
                DepartmentId = DepartmentId,
                NormalizedUserName = "LEQAA",
                PasswordHash = hasher.HashPassword(null!, "P@ssw0rd123")
            };
            modelBuilder.Entity<Employee>().HasData(seedApplicationUser);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
