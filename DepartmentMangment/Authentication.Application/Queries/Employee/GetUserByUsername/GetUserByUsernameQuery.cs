using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using ErrorOr;

namespace DepartManagment.Application.Queries.GetUserByUsername;
public record GetUserByUsername(
    string UserName
) : IQuery<ErrorOr<EmployeeViewModel>>;


