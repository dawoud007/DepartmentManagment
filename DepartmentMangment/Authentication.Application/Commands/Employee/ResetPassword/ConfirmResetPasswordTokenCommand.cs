using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using ErrorOr;

namespace DepartManagment.Application.Commands.ConfirmResetPasswordToken;
public record ConfirmResetPasswordTokenCommand(
   string CurrentPassword,
        string NewPassword,
        string Email
) : ICommand<ErrorOr<string>>;