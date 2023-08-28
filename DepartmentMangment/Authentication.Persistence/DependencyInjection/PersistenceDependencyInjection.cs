using DepartManagment.Application.Commands.Departments;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.Department;
using DepartManagment.Persistence.Repositories;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DepartManagment.Persistence.DependencyInjection;
public static class PersistenceDependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        /* var connectionString = configuration.GetConnectionString("Default");
         services.AddDbContext<ApplicationDbContext>(options =>
         {
             options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
             .EnableSensitiveDataLogging()
             .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
         });*/


        services.AddDbContext<ApplicationDbContext>(Options =>
        {
            Options.UseSqlServer(configuration.GetConnectionString("Default"));
        });


        services.AddScoped<IDepartmentRepository, DepartmentRepository>(); 
        services.AddScoped<IEmployeeTaskRepository, EmployeeTaskRepository>();

        return services;
    }
}