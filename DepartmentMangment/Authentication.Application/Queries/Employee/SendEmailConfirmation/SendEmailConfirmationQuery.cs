using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using ErrorOr;

namespace DepartManagment.Application.Queries.SendEmailConfirmation;
public record SendEmailConfirmationQuery(
    string Email,
    string ConfirmationLink
) : IQuery<ErrorOr<Results>>;