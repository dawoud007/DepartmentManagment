using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Departments
{
    public record AddEmployeeToDepartmentCommand(Guid DepartmentId, string EmployeeId) : ICommand<ErrorOr<EmployeeViewModel>>;
}
