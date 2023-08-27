
using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using ErrorOr;
using MediatR;

namespace DepartManagment.Application.Commands.ConfirmEmail;
public record ConfirmEmailCommand(
    string Email,
    string Token
) : ICommand<ErrorOr<Results>>;