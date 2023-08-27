using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Department;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DepartManagment.Application.Commands.Departments
{
    public record DeployDepartmentCommand(
    string Name,
    string MangerId

) : ICommand<ErrorOr<DepartmentViewModel>>;
}
