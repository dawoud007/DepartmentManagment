using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using ErrorOr;

namespace DepartManagment.Application.Queries.SendEmailResetPassword;
public record SendEmailResetPasswordQuery(
    string Email
) : IQuery<ErrorOr<Results>>;