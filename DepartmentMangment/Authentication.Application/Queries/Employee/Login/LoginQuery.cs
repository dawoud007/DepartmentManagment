

using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using ErrorOr;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DepartManagment.Application.Queries.Login;
public record LoginQuery(
    string UserName,
    string Password
) : IQuery<ErrorOr<Results>>;