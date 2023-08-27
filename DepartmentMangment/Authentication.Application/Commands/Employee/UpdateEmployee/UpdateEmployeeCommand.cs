using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Employees.UpdateEmployee
{
    public record UpdateEmployeeCommand(
      string EmployeeId,
   EmployeeCreateUpdateModel EmployeeUpdateModel
) : ICommand<ErrorOr<Results>>;

}
