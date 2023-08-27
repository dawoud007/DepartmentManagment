using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using ErrorOr;
using MediatR;

namespace DepartManagment.Application.Commands.RegisterUser;
public record RegisterUserCommand(
    string UserName,
    string Password,
    string Email,
    Gender Gender,
    string Name,
    string ConfirmationLink,
   Role Role
) : ICommand<ErrorOr<Results>>;