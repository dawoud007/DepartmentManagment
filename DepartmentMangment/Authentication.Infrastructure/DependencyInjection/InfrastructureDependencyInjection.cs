using System.Reflection;
using DepartManagment.Application.Interfaces;
using DepartManagment.Infrastructure.Models;
using DepartManagment.Infrastructure.NetworkCalls.EmailSender;
using DepartManagment.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DepartManagment.Infrastructure.DependencyInjection;
public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Jwt>(configuration.GetSection("Jwt"));
        services.Configure<Smtp>(configuration.GetSection("Smtp"));

        services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IEmailSender, EmailSender>();
        services.AddSingleton<IConfirmationEmailSender, ConfirmationEmailSender>();
        services.AddSingleton<IResetPasswordEmailSender, ResetPasswordEmailSender>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        

        return services;

    }
}