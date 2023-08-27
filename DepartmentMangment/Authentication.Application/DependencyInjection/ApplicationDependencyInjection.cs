using System.Reflection;
using DepartManagment.Application.Commands.RegisterUser;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DepartManagment.Application.DependencyInjection;
public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(RegisterUserCommand).Assembly);
        return services;
    }
}